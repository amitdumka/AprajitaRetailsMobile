using AprajitaRetails.Mobil.Converters;
using AprajitaRetails.Mobile.Behaviors;
using AprajitaRetails.Mobile.DataModels.Payroll;
using AprajitaRetails.Mobile.RemoteServices;
using AprajitaRetails.Shared.Models.Auth;
using Syncfusion.Maui.DataForm;
using System.ComponentModel.DataAnnotations;
//using static Android.Icu.Text.CaseMap;

namespace AprajitaRetails.Mobile.Pages.EntryPages.Payroll
{
    [INotifyPropertyChanged]
    internal partial class AttendanceEntry
    {
        public AttendanceEntry()
        {
            this.AttendanceId = string.Empty;
            this.EntryTime = DateTime.Now.ToShortTimeString();
            this.Status = AttUnit.StoreClosed;
            this.Remarks = string.Empty;
            this.StoreId = CurrentSession.StoreCode;
            this.EmployeeId = string.Empty;
            this.OnDate = DateTime.Now;
        }

       [Required(ErrorMessage = "Please select Store")]
        //[ObservableProperty]
        public string StoreId { get; set; }

        [ReadOnly(true)]
        [Editable(false)]
        [Display(AutoGenerateField = false)]
        public string AttendanceId { get; set; }

        //[Display(Name = "Employee")]
        [Required(ErrorMessage = "Please select Employee")]
        public string EmployeeId { get; set; }

        [Display(GroupName = "Date Time", Name = "Date")]
        [DataFormDateRange(MinimumDate = "17/02/2016", ErrorMessage = "Attendance cannot be beyond 16/Feb/2016, date is invalid")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please select Date")]
        public DateTime OnDate { get; set; }

        [Display(GroupName = "Date Time")]
        [DataType(DataType.Time)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Entry Time should not be empty")]
        [DataFormValueConverter(typeof(StringToTimeConverter))]
        public string EntryTime { get; set; }

        [Display(Name = "Attndance")]
        [Required(ErrorMessage = "Please select Attendance status")]
        public AttUnit Status { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Remarks is requried")]
        public string? Remarks { get; set; }

        [Display(Name = "Tailor")]
        public bool IsTailoring { get; set; }

        //[Display(Name = "Store")]
    }

    internal class AttendanceBehvior : BaseEntryBehavior<AttendanceEntry, AttendanceDataModel>
    {
        AttendanceEntry entity;

        private async Task<bool> FetchAttendance()
        {
           
            if (dataModel == null)
            {
                dataModel = new AttendanceDataModel();
                dataModel.Mode = DBType.API;
                dataModel.StoreCode = CurrentSession.StoreCode;
                dataModel.Connect();
            }
            var list = (await dataModel.GetByStoreDTO(CurrentSession.StoreCode)).FirstOrDefault();
            if (list == null) return false;
            entity = new AttendanceEntry {
            AttendanceId=list.AttendanceId, EmployeeId=list.EmployeeId, EntryTime=entity.EntryTime, IsTailoring=list.IsTailoring, 
             OnDate=entity.OnDate, Remarks = list.Remarks, Status = list.Status, StoreId=list.StoreId
            };
            return true;
        }
        
        protected override async void DoPrimary()
        {
            if (dataForm != null)
            {
                this.dataForm.Commit();
                AttendanceEntry data = (AttendanceEntry)this.dataForm.DataObject;
                if (dataModel == null)
                {
                    dataModel = new AttendanceDataModel();
                    dataModel.Mode = DBType.API;
                    dataModel.StoreCode = CurrentSession.StoreCode;
                    dataModel.Connect();
                }
                Attendance attd = new Attendance
                {
                    IsReadOnly = false,
                    IsTailoring = data.IsTailoring,
                    MarkedDeleted = false,
                    OnDate = data.OnDate,
                    EmployeeId = data.EmployeeId,
                    AttendanceId = String.Empty,
                    EntryStatus = EntryStatus.Added,
                    EntryTime = data.EntryTime, Remarks=data.Remarks, Status=data.Status, StoreId=data.StoreId, UserId=CurrentSession.UserName
                };
                
                attd = await dataModel.SaveAsync(attd, true);
                if (attd != null && string.IsNullOrEmpty(attd.AttendanceId))
                {
                    Notify.NotifyVShort("Attendance is saved!");
                    this.primaryButton.IsEnabled=false;
                }
                else 
                    Notify.NotifyVLong("Error Occured while saving");
            }
        }

        protected override void DoSecondory()
        {
            throw new NotImplementedException();
        }

        protected override async void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            this.dataForm = bindable.Content.FindByName<SfDataForm>("dataForm");

            if (this.dataForm != null)
            {
                this.dataForm.ColumnCount = 2;
                entity = new AttendanceEntry();
                dataForm.DataObject = entity;// new AttendanceEntry();
                dataForm.CommitMode = DataFormCommitMode.PropertyChanged;
                
               
                this.dataForm.RegisterEditor(nameof(AttendanceEntry.EmployeeId), DataFormEditorType.ComboBox);
                this.dataForm.RegisterEditor(nameof(AttendanceEntry.StoreId), DataFormEditorType.ComboBox);
                this.dataForm.RegisterEditor("IsTailoring", DataFormEditorType.Switch);
                this.dataForm.GenerateDataFormItem += this.OnGenerateDataFormItem;
               
                //((AttendanceEntry)dataForm.DataObject).StoreId = "ARD";
                //dataForm.UpdateEditor("StoreId");

                //dataForm.Commit();

                //if ((await this.FetchAttendance())==true)
                //{
                //    dataForm.DataObject = entity;
                //    dataForm.Commit(); 
                    
                //}
                
            }

            this.primaryButton = bindable.Content.FindByName<Button>("PrimaryButton");

            if (this.primaryButton != null)
            {
                // this.primaryButton.Text = "Save";
                this.primaryButton.Clicked += OnPrimaryButtonClicked;
            }
        }

        private async void OnGenerateDataFormItem(object sender, GenerateDataFormItemEventArgs e)
        {
            if (e.DataFormItem != null && (e.DataFormItem.FieldName == "StoreId" || e.DataFormItem.FieldName == "Store") && e.DataFormItem is DataFormComboBoxItem comboBoxItem)
            {
                e.DataFormItem.LabelText = "Store";
                comboBoxItem.DisplayMemberPath = "Value";
                comboBoxItem.SelectedValuePath = "ID";
                comboBoxItem.IsEditable = true;
                var result = await RestService.GetStoreListAsync();
                comboBoxItem.ItemsSource = result.ToList();
                e.DataFormItem.BindingContext = result;
                
                
            }

            if (e.DataFormItem != null && (e.DataFormItem.FieldName == "EmployeeId" || e.DataFormItem.FieldName == "Employee") && e.DataFormItem is DataFormComboBoxItem cbEmp)
            {
                e.DataFormItem.LabelText = "Employee";
                cbEmp.DisplayMemberPath = "Value";
                cbEmp.SelectedValuePath = "ID";
                cbEmp.IsEditable = true;
                var result = await RestService.GetEmployeeListAsync(CurrentSession.StoreCode);
                cbEmp.ItemsSource = result.ToList(); ;
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