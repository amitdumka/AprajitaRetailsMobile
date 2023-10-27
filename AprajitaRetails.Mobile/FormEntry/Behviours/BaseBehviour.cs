using AprajitaRetails.Mobile.FormEntry.Models;
using AprajitaRetails.Mobile.FormEntry.ViewModels;
using Syncfusion.Maui.DataForm;

namespace AprajitaRetails.Mobile.FormEntry.Behviours
{
    
    public class BaseEntryBehavior<T,VM> : Behavior<SfDataForm>,INotifyPropertyChanged
    {
        protected SfDataForm DataForm { get; set; }
        protected VM viewModel;

        protected override void OnDetachingFrom(SfDataForm dataForm)
        {
            base.OnDetachingFrom(dataForm);

            if (dataForm != null)
            {
                dataForm.GenerateDataFormItem -= this.OnGenerateDataFormItem;
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
                //comboBoxItem.IsEditable = true;
                // comboBoxItem.FieldName = "cbStoreId";
                // comboBoxItem.PlaceholderText = "Select Store";

                var viewModel = DataForm.BindingContext as BaseEntryViewModel<T>;
                comboBoxItem.BindingContext = viewModel;
                comboBoxItem.SetBinding(DataFormComboBoxItem.ItemsSourceProperty, nameof(viewModel.Stores), BindingMode.TwoWay);
            }

            if (e.DataFormItem != null && (e.DataFormItem.FieldName == "EmployeeId" || e.DataFormItem.FieldName == "Employee") && e.DataFormItem is DataFormComboBoxItem cbEmp)
            {
                 e.DataFormItem.LabelText = "Employee";
                cbEmp.DisplayMemberPath = "Value";
                cbEmp.SelectedValuePath = "ID";
                // cbEmp.IsEditable = true;

                var viewModel = DataForm.BindingContext as  BaseEntryViewModel<T>; ;
                cbEmp.BindingContext = viewModel;
                cbEmp.SetBinding(DataFormComboBoxItem.ItemsSourceProperty, nameof(viewModel.Employees), BindingMode.TwoWay);

                //Notify.NotifyVShort(viewModel.Employees.First().ID);
            }

            if (e.DataFormItem != null)
            {
                if (e.DataFormItem.FieldName == "IsTailoring")
                {
                    e.DataFormItem.IsVisible = false;
                }
            }
        }
    }
    public partial class AttendanceEntryFormBehavior : BaseEntryBehavior<AttendanceEM,AttendanceEntryViewModel>
    {

        protected override async void OnAttachedTo(SfDataForm dataForm)
        {
            base.OnAttachedTo(dataForm);
            // dataForm = bindable.Content.FindByName<SfDataForm>("dataForm");

            if (dataForm != null)
            {
                DataForm = dataForm;
                dataForm.ColumnCount = 2;
             
                dataForm.RegisterEditor(nameof(Attendance.EmployeeId), DataFormEditorType.ComboBox);
                dataForm.RegisterEditor(nameof(Attendance.StoreId), DataFormEditorType.ComboBox);
                dataForm.RegisterEditor("IsTailoring", DataFormEditorType.Switch);
                
                viewModel = DataForm.BindingContext as AttendanceEntryViewModel;
                dataForm.Commit();
                dataForm.GenerateDataFormItem += OnGenerateDataFormItem;
                

                
            }
        }

        private async Task WorkArroundForComboBoxLoad()
        {
          var  Attendance = new AttendanceEM
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

        protected  override void OnGenerateDataFormItem(object sender, GenerateDataFormItemEventArgs e)
        {
            base.OnGenerateDataFormItem (sender, e);


        }

        private void OnDataObjectPropertyChanged(object sender, PropertyChangedEventArgs e)

        {
            if (DataForm != null && !string.IsNullOrEmpty(e.PropertyName))

            {
                DataForm.UpdateEditor(e.PropertyName);
            }
        }

        protected override void OnDetachingFrom(SfDataForm dataForm)
        {
            base.OnDetachingFrom(dataForm);

            if (dataForm != null)
            {
                dataForm.GenerateDataFormItem -= this.OnGenerateDataFormItem;
                // (dataForm.DataObject as Attendance).PropertyChanged -= OnDataObjectPropertyChanged;
            }
        }

        [RelayCommand]
        protected void Save() { 
            DataForm.Commit();
            Notify.NotifyVShort((DataForm.DataObject as AttendanceEM).EntryTime);
           
        }
    }

    public class EmployeeEntryFormBehavior: BaseEntryBehavior<EmployeeEM, EmployeeEntryViewModel>
    {

    }
}
