using AprajitaRetails.Mobil.Converters;
using AprajitaRetails.Mobile.Behaviors;
using AprajitaRetails.Mobile.DataModels.Payroll;
using AprajitaRetails.Mobile.RemoteServices;
using AprajitaRetails.Shared.Models.Auth;
using AprajitaRetails.Shared.ViewModels;
using Microsoft.Maui.Controls;
using Syncfusion.Maui.DataForm;
using System.ComponentModel.DataAnnotations;
//using static Android.Icu.Text.CaseMap;

namespace AprajitaRetails.Mobile.Pages.EntryPages.Payroll
{


    public class AttVM
    {
        public AttendanceEntry AttendanceEntry { get; set; }

        public AttVM()
        {
            this.AttendanceEntry = new AttendanceEntry();

        }
    }
    [INotifyPropertyChanged]
    public partial class AttendanceEntry // : ObservableValidator
    {

        public AttendanceEntry()
        {


            this.AttendanceId = string.Empty;
            this.EntryTime = DateTime.Now.ToShortTimeString();
            this.Status = AttUnit.SundayHoliday;
            this.Remarks = string.Empty;
            this.StoreId = "ARJ";
            this.EmployeeId = "ARD-2016-SM-1";
            this.OnDate = DateTime.Now;
        }

        [Required(ErrorMessage = "Please select Store")]
        //[ObservableProperty]
        //private string _storeId;//{ get; set; }
        public string StoreId { get; set; }

        [ReadOnly(true)]
        [Editable(false)]
        //[ObservableProperty]
        [Display(AutoGenerateField = false)]
        //private string _attendanceId;//{ get; set; }

        public string AttendanceId { get; set; }

        //[Display(Name = "Employee")]
        [Required(ErrorMessage = "Please select Employee")]
        //[ObservableProperty]
        //private string _employeeId;// { get; set; }
        public string EmployeeId { get; set; }

        [Display(GroupName = "Date Time", Name = "Date")]
        [DataFormDateRange(MinimumDate = "17/02/2016", ErrorMessage = "Attendance cannot be beyond 16/Feb/2016, date is invalid")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please select Date")]
        //[ObservableProperty]
        //private DateTime _onDate;//{ get; set; }
        public DateTime OnDate { get; set; }

        [Display(GroupName = "Date Time")]
        [DataType(DataType.Time)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Entry Time should not be empty")]
        [DataFormValueConverter(typeof(StringToTimeConverter))]
        //[ObservableProperty]
        public string EntryTime { get; set; }
        //private string _entryTime;// { get; set; }

        [Display(Name = "Attndance")]
        [Required(ErrorMessage = "Please select Attendance status")]
        //[ObservableProperty]
        //private AttUnit _status;// { get; set; }
        public AttUnit Status { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Remarks is requried")]
        //[ObservableProperty]
        public string? Remarks { get; set; }
        //private string? _remarks;//{ get; set; }

        [Display(Name = "Tailor")]
        //[ObservableProperty]
        //private bool _isTailoring;// { get; set; }
        public bool IsTailoring { get; set; }


    }
    public class BasicData
    {
        public static List<SelectOption> Stores { get; set; }
        public static List<SelectOption> Employees { get; set; }
        public static async void LoadDataSources()
        {
            if (Stores == null)
                Stores = await RestService.GetStoreListAsync();
            if (Employees == null)
                Employees = await RestService.GetEmployeeListAsync(CurrentSession.StoreCode);

        }
    }

    public class ItemSourceProvider : IDataFormSourceProvider
    {


        public object GetSource(string sourceName)
        {
            if (sourceName == "Store" || sourceName == "Stores" || sourceName == "StoreId")
            {
                List<SelectOption> departmentDetails = new List<SelectOption>();
                departmentDetails.Add(new SelectOption() { Value = "Aprajita Retail Dumka", ID = "ARD" });
                departmentDetails.Add(new SelectOption() { Value = "Aprajita Retail Jamshedpur", ID = "ARJ" });
                departmentDetails.Add(new SelectOption() { Value = "Jockey Dumka", ID = "JCK" });
                departmentDetails.Add(new SelectOption() { Value = "Personal Dumka", ID = "ARO" });
                //if (Stores == null) Stores = new List<SelectOption>();
                return departmentDetails;
            }
            if (sourceName == "Employee" || sourceName == "Employees" || sourceName == "EmployeeId")
            {
                //List<SelectOption> departmentDetails = new List<SelectOption>();
                //departmentDetails.Add(new SelectOption() { Value = "Aprajita Retail Dumka", ID = "ARD" });
                //departmentDetails.Add(new SelectOption() { Value = "Aprajita Retail Jamshedpur", ID = "ARJ" });
                //departmentDetails.Add(new SelectOption() { Value = "Jockey Dumka", ID = "JCK" });
                //departmentDetails.Add(new SelectOption() { Value = "Personal Dumka", ID = "ARO" });
                //if (Employees == null) Employees = new List<SelectOption>();
                return BasicData.Employees;
            }

            return new List<string>();
        }
    }
    internal class AttendanceBehvior : BaseEntryBehavior<AttendanceEntry, AttendanceDataModel>
    {

        public AttendanceEntry Entity { get; set; }
        public AttendanceBehvior()
        {
            FetchAttendance();
        }
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
            
            Entity = new AttendanceEntry
            {
                AttendanceId = list.AttendanceId,
                EmployeeId = list.EmployeeId,
                EntryTime = list.EntryTime,
                IsTailoring = list.IsTailoring,
                OnDate = list.OnDate.Date,
                Remarks = list.Remarks+"TESTME",
                Status = list.Status,
                StoreId = list.StoreId
            };
            return true;
        }

        protected override async void DoPrimary()
        {
            if ((await this.FetchAttendance()) == true)
            {
                
                //dataForm.SetBinding(SfDataForm.DataObjectProperty, "entity");
                //dataForm.BindingContext = this;
                dataForm.UpdateEditor("EmployeeId");
                dataForm.UpdateEditor("StoreId");

                //dataForm.Commit();

            }
            //if (dataForm != null)
            //{
            //    this.dataForm.Commit();
            //    AttendanceEntry data = (AttendanceEntry)this.dataForm.DataObject;
            //    if (dataModel == null)
            //    {
            //        dataModel = new AttendanceDataModel();
            //        dataModel.Mode = DBType.API;
            //        dataModel.StoreCode = CurrentSession.StoreCode;
            //        dataModel.Connect();
            //    }
            //    Attendance attd = new Attendance
            //    {
            //        IsReadOnly = false,
            //        IsTailoring = data.IsTailoring,
            //        MarkedDeleted = false,
            //        OnDate = data.OnDate,
            //        EmployeeId = data.EmployeeId,
            //        AttendanceId = String.Empty,
            //        EntryStatus = EntryStatus.Added,
            //        EntryTime = data.EntryTime,
            //        Remarks = data.Remarks,
            //        Status = data.Status,
            //        StoreId = data.StoreId,
            //        UserId = CurrentSession.UserName
            //    };

            //    attd = await dataModel.SaveAsync(attd, true);
            //    if (attd != null && string.IsNullOrEmpty(attd.AttendanceId))
            //    {
            //        Notify.NotifyVShort("Attendance is saved!");
            //        dataForm.DataObject = new AttendanceEntry();
            //        // this.primaryButton.IsEnabled=false;
            //    }
            //    else
            //        Notify.NotifyVLong("Error Occured while saving");
            //}
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

                //entity = new AttendanceEntry();
                //dataForm.DataObject = Entity;// new AttendanceEntry();
                //dataForm.CommitMode = DataFormCommitMode.PropertyChanged;


                this.dataForm.RegisterEditor(nameof(AttendanceEntry.EmployeeId), DataFormEditorType.ComboBox);
                this.dataForm.RegisterEditor(nameof(AttendanceEntry.StoreId), DataFormEditorType.ComboBox);
                this.dataForm.RegisterEditor("IsTailoring", DataFormEditorType.Switch);
                this.dataForm.ItemsSourceProvider = new ItemSourceProvider();
                this.dataForm.GenerateDataFormItem += this.OnGenerateDataFormItem;

                //if ((await this.FetchAttendance()) == true)
                //{
                //    dataForm.DataObject = entity;
                //   // dataForm.UpdateEditor("EmployeeId");
                //    //dataForm.UpdateEditor("StoreId");

                //    //dataForm.Commit();

                //}
                // ((AttendanceEntry)dataForm.DataObject).StoreId = "ARD";
                // dataForm.UpdateEditor("StoreId");

                // dataForm.Commit();
               // dataForm.SetBinding(SfDataForm.DataObjectProperty, "Entity");
               // dataForm.BindingContext = this;


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
                //var result = await RestService.GetStoreListAsync();
                //comboBoxItem.ItemsSource = result.ToList();
                // e.DataFormItem.BindingContext = result;


            }

            if (e.DataFormItem != null && (e.DataFormItem.FieldName == "EmployeeId" || e.DataFormItem.FieldName == "Employee") && e.DataFormItem is DataFormComboBoxItem cbEmp)
            {
                e.DataFormItem.LabelText = "Employee";
                cbEmp.DisplayMemberPath = "Value";
                cbEmp.SelectedValuePath = "ID";
                cbEmp.IsEditable = true;
                // var result = await RestService.GetEmployeeListAsync(CurrentSession.StoreCode);
                // cbEmp.ItemsSource = result.ToList(); ;
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