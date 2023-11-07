using AprajitaRetails.Mobile.DataModels.Payroll;
using AprajitaRetails.Mobile.FormEntry.Models;
using AprajitaRetails.Mobile.FormEntry.ViewModels;
using AprajitaRetails.Mobile.FormEntry.Views;

using Syncfusion.Maui.DataForm;

namespace AprajitaRetails.Mobile.FormEntry.Behviours
{
    public partial class AttendanceEntryFormBehavior : BaseEntryBehavior<AttendanceEM, AttendanceEntryViewModel>
    {
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            var ev = ((AttendanceEntryPage)bindable).entryView;
            //var ev =bindable.Content.FindByName<ContentView>("entryView");

            var dataForm = ev.FindByName<SfDataForm>("dataForm");

            if (dataForm != null)
            {
                dataForm.ColumnCount = 2;
                DataForm = dataForm;
                dataForm.RegisterEditor(nameof(Attendance.EmployeeId), DataFormEditorType.ComboBox);
                dataForm.RegisterEditor(nameof(Attendance.StoreId), DataFormEditorType.ComboBox);
                dataForm.RegisterEditor("IsTailoring", DataFormEditorType.Switch);

                viewModel = DataForm.BindingContext as AttendanceEntryViewModel;
                dataForm.Commit();
                dataForm.GenerateDataFormItem += OnGenerateDataFormItem;

                this.primaryButton = ev.FindByName<Button>("PrimaryButton");
                if (this.primaryButton != null)
                {
                    this.primaryButton.Clicked += OnPrimaryButtonClicked;
                }
                backButton = ev.FindByName<Button>("BackButton");
                if (this.backButton != null)
                {
                    this.backButton.Clicked += OnBackButtonClicked;
                }
                cancleButton = ev.FindByName<Button>("CancleButton");
                if (this.cancleButton != null)
                {
                    this.cancleButton.Clicked += OnCancleButtonClicked;
                }
            }
        }

        protected override void OnCancleButtonClicked(object sender, EventArgs e)
        {
            this.DataForm.DataObject = viewModel.Entity = new AttendanceEM();
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);

            if (DataForm != null)
            {
                DataForm.GenerateDataFormItem -= this.OnGenerateDataFormItem;
                // (dataForm.DataObject as Attendance).PropertyChanged -= OnDataObjectPropertyChanged;
            }
        }

        protected override void OnGenerateDataFormItem(object sender, GenerateDataFormItemEventArgs e)
        {
            base.OnGenerateDataFormItem(sender, e);
        }

        protected override async void OnPrimaryButtonClicked(object? sender, EventArgs e)
        {
            if (this.DataForm != null)
            {
                this.DataForm.Commit();
                if (this.DataForm.Validate())
                {
                    Notify.NotifyShort($" Please Wait while Saving new Attendance...");
                    AttendanceDataModel dataModel = new AttendanceDataModel();
                     
                    var att = this.DataForm.DataObject as AttendanceEM;
                    var result = await dataModel.SaveAsync(new Attendance
                    {
                        AttendanceId = "",
                        EmployeeId = att.EmployeeId,
                        EntryStatus = EntryStatus.Added,
                        EntryTime = att.EntryTime,
                        IsReadOnly = false,
                        IsTailoring = false,
                        MarkedDeleted = false,
                        OnDate = att.OnDate,
                        Remarks = att.Remarks,
                        StoreId = att.StoreId,
                        UserId = CurrentSession.UserName,
                        Status = att.Status
                    });

                    if (result != null)
                    {
                        Notify.NotifyShort($" Attendance is added Successful with Employee Id {result.EmployeeId}");
                        DataForm.DataObject = viewModel.Entity = new AttendanceEM();
                    }
                }
                else
                {
                    Notify.NotifyLong((DataForm.DataObject as AttendanceEM).EmployeeId + " Please enter the required details");
                }
            };
        }

        protected override void OnSecondaryButtonClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void Save()
        {
            DataForm.Commit();
            Notify.NotifyVShort((DataForm.DataObject as AttendanceEM).EntryTime);
        }

        private void OnDataObjectPropertyChanged(object sender, PropertyChangedEventArgs e)

        {
            if (DataForm != null && !string.IsNullOrEmpty(e.PropertyName))

            {
                DataForm.UpdateEditor(e.PropertyName);
            }
        }

        private async Task WorkArroundForComboBoxLoad()
        {
            var Attendance = new AttendanceEM
            {
                AttendanceId = "11",

                EntryTime = DateTime.Now.ToShortTimeString(),
                Status = AttUnit.SundayHoliday,
                Remarks = "10:30 PM",
                StoreId = "ARD",
                EmployeeId = "ARD-2016-SM-3",
                OnDate = DateTime.Now.AddDays(5),
            };
            if (viewModel.Stores != null && viewModel.Employees != null && viewModel.Stores.Any() && viewModel.Employees.Any())
            {
                viewModel.Entity = Attendance;
                DataForm.DataObject = Attendance;
            }
            else
            {
                await Task.Delay(10000);
                viewModel.Entity = Attendance;
                DataForm.DataObject = Attendance;
            }
        }
    }
}