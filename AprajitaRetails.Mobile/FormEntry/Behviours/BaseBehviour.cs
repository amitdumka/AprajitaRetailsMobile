
using AprajitaRetails.Mobile.DataModels.Payroll;
using AprajitaRetails.Mobile.FormEntry.Models;
using AprajitaRetails.Mobile.FormEntry.ViewModels;
using AprajitaRetails.Mobile.FormEntry.Views;

using Syncfusion.Maui.DataForm;

namespace AprajitaRetails.Mobile.FormEntry.Behviours
{

    public abstract partial class BaseEntryBehavior<T, VM> : Behavior<ContentPage>, INotifyPropertyChanged
    {
        protected SfDataForm DataForm { get; set; }
        protected VM viewModel;
        protected Button primaryButton, secondaryButton, backButton, cancleButton;
        protected INavigation Navigation;

        protected override void OnDetachingFrom(ContentPage bindable)
        {

            base.OnDetachingFrom(bindable);

            if (DataForm != null)
            {
                DataForm.GenerateDataFormItem -= this.OnGenerateDataFormItem;
                // (dataForm.DataObject as Attendance).PropertyChanged -= OnDataObjectPropertyChanged;
            }
        }
        protected virtual void OnGenerateDataFormItem(object sender, GenerateDataFormItemEventArgs e)
        {
            if (e.DataFormItem != null && (e.DataFormItem.FieldName == "StoreId" || e.DataFormItem.FieldName == "Store") && e.DataFormItem is DataFormComboBoxItem comboBoxItem)
            {
                e.DataFormItem.LabelText = "Store";
                comboBoxItem.DisplayMemberPath = "Value";
                comboBoxItem.SelectedValuePath = "ID";


                var viewModel = DataForm.BindingContext as BaseEntryViewModel<T>;
                comboBoxItem.BindingContext = viewModel;
                comboBoxItem.SetBinding(DataFormComboBoxItem.ItemsSourceProperty, nameof(viewModel.Stores), BindingMode.TwoWay);
            }

            if (e.DataFormItem != null && (e.DataFormItem.FieldName == "EmployeeId" || e.DataFormItem.FieldName == "Employee") && e.DataFormItem is DataFormComboBoxItem cbEmp)
            {
                e.DataFormItem.LabelText = "Employee";
                cbEmp.DisplayMemberPath = "Value";
                cbEmp.SelectedValuePath = "ID";


                var viewModel = DataForm.BindingContext as BaseEntryViewModel<T>; ;
                cbEmp.BindingContext = viewModel;
                cbEmp.SetBinding(DataFormComboBoxItem.ItemsSourceProperty, nameof(viewModel.Employees), BindingMode.TwoWay);


            }

            if (e.DataFormItem != null)
            {
                if (e.DataFormItem.FieldName == "IsTailoring")
                {
                    e.DataFormItem.IsVisible = false;
                }
            }
        }

        protected abstract void OnPrimaryButtonClicked(object? sender, EventArgs e);
        protected abstract void OnSecondaryButtonClicked(object? sender, EventArgs e);
        protected virtual async void OnBackButtonClicked(object? sender, EventArgs e){
            await Navigation.PopAsync();
        }
        protected abstract void OnCancleButtonClicked(object? sender, EventArgs e);
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            Navigation = bindable.Navigation;
        }



    }
    public partial class AttendanceEntryFormBehavior : BaseEntryBehavior<AttendanceEM, AttendanceEntryViewModel>
    {

        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            var ev = ((AttendanceEntryPage)bindable).entryView;
            //var ev =bindable.Content.FindByName<ContentView>("entryView");

            var dataForm = ev.FindByName<SfDataForm>("dataForm"); ;//.Content.FindByName<SfDataForm>("dataForm");

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

            }
        }
        protected override async void OnBackButtonClicked(object? sender, EventArgs e)
        {
            // await Navigation.PopAsync();
        }
        protected override async void OnPrimaryButtonClicked(object? sender, EventArgs e)
        {
            if (this.DataForm != null)
            {
                if (this.DataForm.Validate())
                {
                    Notify.NotifyShort((DataForm.DataObject as AttendanceEM).StoreId + " Attendance is save Successful")
                       ;
                    DataForm.DataObject = new AttendanceEM
                    {
                        EntryTime = DateTime.Now.ToShortTimeString(),
                        OnDate = DateTime.Now,
                        Status = AttUnit.Absent,
                        Remarks = ""
                    ,
                        StoreId = "ARJ"

                    };
                }
                else
                {
                    Notify.NotifyLong((DataForm.DataObject as AttendanceEM).EmployeeId + " Please enter the required details");
                }
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

        protected override void OnGenerateDataFormItem(object sender, GenerateDataFormItemEventArgs e)
        {
            base.OnGenerateDataFormItem(sender, e);


        }

        private void OnDataObjectPropertyChanged(object sender, PropertyChangedEventArgs e)

        {
            if (DataForm != null && !string.IsNullOrEmpty(e.PropertyName))

            {
                DataForm.UpdateEditor(e.PropertyName);
            }
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


        protected void Save()
        {
            DataForm.Commit();
            Notify.NotifyVShort((DataForm.DataObject as AttendanceEM).EntryTime);

        }

        protected override void OnSecondaryButtonClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }



        protected override void OnCancleButtonClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    public class EmployeeEntryFormBehavior : BaseEntryBehavior<EmployeeEM, EmployeeEntryViewModel>
    {
        
        

        protected override void OnCancleButtonClicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            this.DataForm.DataObject = viewModel.Entity=new EmployeeEM();
            
        }

        protected override async void OnPrimaryButtonClicked(object sender, EventArgs e)
        {
            if (this.DataForm != null)
            {
                this.DataForm.Commit();
                if (this.DataForm.Validate())
                {


                    Notify.NotifyShort($" Please Wait while Saving new Employee...");
                    EmployeeDataModel dataModel = new EmployeeDataModel();
                    //dataModel.Connect();
                    var emp = this.DataForm.DataObject as EmployeeEM;
                    var result = await dataModel.SaveAsync(new Employee
                    {
                        AddressLine = emp.Address,
                        DOB = emp.BirthDate,
                        FirstName = emp.FirstName,
                        LastName = emp.LastName,
                        Gender = emp.Gender,
                        IsTailors = false,
                        IsWorking = emp.IsWorking,
                        JoiningDate = emp.JoiningDate,
                        LeavingDate = DateTime.Now.AddYears(-10),
                        MarkedDeleted = false,
                        State = emp.State,
                        StoreId = emp.StoreId,
                        StreetName = emp.Address,
                        Title = emp.Title,
                        ZipCode = emp.ZipCode,
                        Category = emp.Category,
                        City = emp.City,
                        Country = emp.Country, EmployeeId="", 
                    });

                    if (result != null)
                    {

                        Notify.NotifyShort($" Employee is added Successful with Employee Id {result.EmployeeId}");
                        DataForm.DataObject =viewModel.Entity= new EmployeeEM();
                    }
                }
                else
                {
                    Notify.NotifyLong((DataForm.DataObject as EmployeeEM).StoreId + " Please enter the required details");
                }
            };
        }

        protected override void OnSecondaryButtonClicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
        protected override void OnGenerateDataFormItem(object sender, GenerateDataFormItemEventArgs e)
        {
            base.OnGenerateDataFormItem(sender, e);

            if (e.DataFormItem != null && (e.DataFormItem.FieldName == "Title" || e.DataFormItem.FieldName == "Title") && e.DataFormItem is DataFormComboBoxItem cbTitles)
            {
                e.DataFormItem.LabelText = "Tile";
                var tiles = new List<string> { "Mr", "Mrs.", "Ms", "Master" };
                cbTitles.ItemsSource = tiles;
                cbTitles.MaxDropDownHeight = 100;

            }
            if (e.DataFormItem != null && e.DataFormItem.FieldName == "IsWorking")
            {
                e.DataFormItem.LabelText = "Working";
            }

            if (e.DataFormItem != null && (e.DataFormItem.FieldName == "IsActive" || e.DataFormItem.FieldName == "HasErrors"))
            {
                e.DataFormItem.IsVisible = false;

            }
            if (e.DataFormGroupItem != null)
            {
                if (e.DataFormGroupItem.Name == "Name")
                {
                    e.DataFormGroupItem.ColumnCount = 4;
                }
                else if (e.DataFormGroupItem.Name == "Address")
                {
                    e.DataFormGroupItem.ColumnCount = 4;
                }
                else if (e.DataFormGroupItem.Name == "Contact")
                {
                    e.DataFormGroupItem.ColumnCount = 3;
                }
            }


        }
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
           // Navigation = bindable.Navigation;
            var ev = ((EmployeeEntryPage)bindable).entryView;

            viewModel = bindable.BindingContext as EmployeeEntryViewModel;

            var dataForm = ev.FindByName<SfDataForm>("dataForm");

            if (dataForm != null)
            {

                dataForm.ColumnCount = 1;
                DataForm = dataForm;
                dataForm.RegisterEditor(nameof(EmployeeEM.StoreId), DataFormEditorType.ComboBox);
                dataForm.RegisterEditor(nameof(EmployeeEM.Title), DataFormEditorType.ComboBox);
                dataForm.RegisterEditor("IsWorking", DataFormEditorType.Switch);

                viewModel = DataForm.BindingContext as EmployeeEntryViewModel;
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

            }
        }
    }
}
