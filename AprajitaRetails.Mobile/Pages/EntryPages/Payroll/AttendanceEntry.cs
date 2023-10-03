﻿using AprajitaRetails.Mobile.Behaviors;
using AprajitaRetails.Mobile.RemoteServices;
using Syncfusion.Maui.DataForm;
using System.ComponentModel.DataAnnotations;

namespace AprajitaRetails.Mobile.Pages.EntryPages.Payroll
{
    internal class AttendanceEntry
    {
        public AttendanceEntry() { 
        
            this.AttendanceId=string.Empty;
            this.EntryTime=DateTime.Now.ToShortTimeString();
            this.Status = AttUnit.Absent;
            this.Remarks=string.Empty;
            this.StoreId=CurrentSession.StoreCode;
            this.EmployeeId=string.Empty;
            this.OnDate = DateTime.Now;           
        }    

        [ReadOnly(true)]
        [Editable(false)]
        [Display(AutoGenerateField = false)]
        public string AttendanceId { get; set; }

        // [Display(Name = "Employee")]
        [Required(ErrorMessage = "Please select Employee")]
        public string EmployeeId { get; set; }

        [Display(GroupName = "Date Time")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please select Date")]
        public DateTime OnDate { get; set; }

        [Display(GroupName = "Date Time")]
        [DataFormDateRange(MinimumDate = "17/02/2016", ErrorMessage = "Attendance cannot be beyond 16/Feb/2016, date is invalid")]
        [DataType(DataType.Time)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Entry Time should not be empty")]
        public string? EntryTime { get; set; }

        [Required(ErrorMessage = "Please select Attendance status")]
        public AttUnit Status { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Remarks is requried")]
        public string? Remarks { get; set; }

        [Display(Name = "Tailor")]
        public bool IsTailoring { get; set; }

        //[Display(Name = "Store")]
        public string StoreId { get; set; }
    }
    internal class AttendanceBehvior:BaseEntryBehavior<AttendanceEntry>
    {
        protected override void DoPrimary()
        {

            if (dataForm != null)
            {
                this.dataForm.Commit();
                AttendanceEntry data = (AttendanceEntry)this.dataForm.DataObject;
                Notify.NotifyVLong(data.EmployeeId);
            }
        }

        protected override void DoSecondory()
        {
            throw new NotImplementedException();
        }

        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            this.dataForm = bindable.Content.FindByName<SfDataForm>("AttendanceEntryForm");

            if (this.dataForm != null)
            {
                this.dataForm.RegisterEditor(nameof(AttendanceEntry.EmployeeId), DataFormEditorType.ComboBox);
                this.dataForm.RegisterEditor(nameof(AttendanceEntry.StoreId), DataFormEditorType.ComboBox);
                this.dataForm.RegisterEditor("IsTailoring", DataFormEditorType.Switch);

                this.dataForm.GenerateDataFormItem += this.OnGenerateDataFormItem;
                
            }

            this.primaryButton = bindable.Content.FindByName<Button>("Save");
            if (this.primaryButton != null)
            {
                this.primaryButton.Clicked += OnPrimaryButtonClicked;
            }
        }
        private async void OnGenerateDataFormItem(object sender, GenerateDataFormItemEventArgs e)
        {
            if (e.DataFormItem != null && e.DataFormItem.FieldName == "StoreId" && e.DataFormItem is DataFormComboBoxItem comboBoxItem)
            {

                comboBoxItem.DisplayMemberPath = "Value";
                comboBoxItem.SelectedValuePath = "ID";
                var result = await RestService.GetStoreListAsync();
                comboBoxItem.ItemsSource = result;
            }
            if (e.DataFormItem != null && e.DataFormItem.FieldName == "EmployeeId" && e.DataFormItem is DataFormComboBoxItem cbEmp)
            {

                cbEmp.DisplayMemberPath = "Value";
                cbEmp.SelectedValuePath = "ID";
                var result = await RestService.GetEmployeeListAsync(CurrentSession.StoreCode);
                cbEmp.ItemsSource = result;
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
}
