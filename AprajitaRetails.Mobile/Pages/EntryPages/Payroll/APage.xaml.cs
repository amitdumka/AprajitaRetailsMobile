using AprajitaRetails.Mobil.Converters;
using AprajitaRetails.Mobile.RemoteServices;
using AprajitaRetails.Shared.ViewModels;
using Syncfusion.Maui.DataForm;
using System.ComponentModel.DataAnnotations;

namespace AprajitaRetails.Mobile.Pages.EntryPages.Payroll
{
    public partial class APage : ContentPage
    {
        public APage()
        {
            InitializeComponent();
        }
    }

    public partial class AttendanceDFViewModel
    {
        public Attendance Attendance { get; set; }
        public AttendanceDFViewModel()
        {
            Attendance= new Attendance();
        }
    }
    public class BasicData
    {
        public static List<SelectOption> Stores { get; set; }
        public static List<SelectOption> Employees { get; set; }
        public static async void LoadDataSources()
        {
            if (Stores == null || !Stores.Any())
                Stores = await RestService.GetStoreListAsync();
            if (Employees == null || !Employees.Any())
                Employees = await RestService.GetEmployeeListAsync(CurrentSession.StoreCode);

        }
    }
    //[INotifyPropertyChanged]
    public partial class Attendance
    {
        public Attendance()
        {
            this.AttendanceId = string.Empty;
            this.EntryTime = DateTime.Now.ToShortTimeString();
            this.Status = AttUnit.SundayHoliday;
            this.Remarks = string.Empty;
            this.StoreId = "ARD";
            this.EmployeeId = "ARD002";
            this.OnDate = DateTime.Now;
        }

        [Required(ErrorMessage = "Please select Store")]
        public string StoreId { get; set; }

        [ReadOnly(true)]
        [Editable(false)]
        [Display(AutoGenerateField = false)]
       
        public string AttendanceId { get; set; }

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

    }

    public class AttendanceFormBehvour : Behavior<SfDataForm>
    {
        Attendance Attendance { get; set; }
        SfDataForm DataForm { get; set; }
        protected override async void OnAttachedTo(SfDataForm dataForm)
        {
            base.OnAttachedTo(dataForm);
           // dataForm = bindable.Content.FindByName<SfDataForm>("dataForm");

            if (dataForm != null)
            {
                DataForm = dataForm;
                dataForm.ColumnCount = 2;
                dataForm.ItemsSourceProvider = new ItemSourceProvider2();
                dataForm.RegisterEditor(nameof(Attendance.EmployeeId), DataFormEditorType.ComboBox);
                dataForm.RegisterEditor(nameof(Attendance.StoreId), DataFormEditorType.ComboBox);
                dataForm.RegisterEditor("IsTailoring", DataFormEditorType.Switch);
                //dataForm.GenerateDataFormItem += this.OnGenerateDataFormItem;
                WorkArroundForComboBoxLoad();
                dataForm.Commit();

                dataForm.GenerateDataFormItem += OnGenerateDataFormItem;


            }
           
            

        }
        private async void WorkArroundForComboBoxLoad()
        {
            Attendance = new Attendance
            {
                AttendanceId = "11",

                EntryTime = DateTime.Now.ToShortTimeString(),
                Status = AttUnit.SundayHoliday,
                Remarks = "10:30 PM",
                StoreId = "ARD",
                EmployeeId = "ARD004",
                OnDate = DateTime.Now.AddDays(5),


            };
            if (BasicData.Stores != null && BasicData.Employees != null && BasicData.Stores.Any() && BasicData.Employees.Any())
            {
                DataForm.DataObject = Attendance;
            }
            else
            {
                await Task.Delay(10000);
           
                DataForm.DataObject = Attendance;

            }
        }
        private async void OnGenerateDataFormItem(object sender, GenerateDataFormItemEventArgs e)
        {
            if (e.DataFormItem != null && (e.DataFormItem.FieldName == "StoreId" || e.DataFormItem.FieldName == "Store") && e.DataFormItem is DataFormComboBoxItem comboBoxItem)
            {
               // e.DataFormItem.LabelText = "Store";
                comboBoxItem.DisplayMemberPath = "Value";
                comboBoxItem.SelectedValuePath = "ID";
                comboBoxItem.IsEditable = true;
               

            }

            if (e.DataFormItem != null && (e.DataFormItem.FieldName == "EmployeeId" || e.DataFormItem.FieldName == "Employee") && e.DataFormItem is DataFormComboBoxItem cbEmp)
            {
               // e.DataFormItem.LabelText = "Employee";
                cbEmp.DisplayMemberPath = "Value";
                cbEmp.SelectedValuePath = "ID";
                cbEmp.IsEditable = true;
               
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

    public class ItemSourceProvider : IDataFormSourceProvider
    {

        public ItemSourceProvider()
        {

            BasicData.LoadDataSources();
        }

        public object GetSource(string sourceName)
        {
            if (sourceName == "Store" || sourceName == "Stores" || sourceName == "StoreId")
            {

                if (BasicData.Stores == null) BasicData.Stores = new List<SelectOption>();
                return BasicData.Stores;
            }
            if (sourceName == "Employee" || sourceName == "Employees" || sourceName == "EmployeeId")
            {

                if (BasicData.Employees == null) BasicData.Employees = new List<SelectOption>();
                return BasicData.Employees;
            }

            return new List<string>();
        }
    }
    public class ItemSourceProvider2 : IDataFormSourceProvider
    {
        public ItemSourceProvider2()
        {
            LoadDataSources();
        }

        public List<SelectOption> Stores { get; set; }
        public List<SelectOption> Employees { get; set; }

        private async void LoadDataSources()
        {
            Stores = await GetStoresListAsync();
            Employees = await GetEmployeeListAsync();

        }


        private async Task<List<SelectOption>> GetStoresListAsync()
        {

            List<SelectOption> storeDetails = new List<SelectOption>();
            storeDetails.Add(new SelectOption()
            {
                Value =
            "Aprajita Retail Dumka",
                ID = "ARD"
            });
            storeDetails.Add(new SelectOption()
            {
                Value =
            " Retail Jamshedpur",
                ID = "ARJ"
            });
            storeDetails.Add(new SelectOption()
            {
                Value =
            "Jockey Dumka",
                ID = "JCK"
            });
            storeDetails.Add(new SelectOption()
            {
                Value =
            "Personal Dumka",
                ID = "ARO"
            });
            await Task.Delay(1000);
            return storeDetails;

        }

        private async Task<List<SelectOption>> GetEmployeeListAsync()
        {

            List<SelectOption> storeDetails = new List<SelectOption>();
            storeDetails.Add(new SelectOption()
            {
                Value =
            "Alok",
                ID = "ARD004"
            });
            storeDetails.Add(new SelectOption()
            {
                Value =
            " Amit Thakur",
                ID = "ARD003"
            });
            storeDetails.Add(new SelectOption()
            {
                Value =
            "Mukesh",
                ID = "ARD002"
            });
            storeDetails.Add(new SelectOption()
            {
                Value =
            "Geetanjali",
                ID = "ARD001"
            });
            await Task.Delay(1000);
            return storeDetails;

        }

        public object GetSource(string sourceName)
        {
            if (sourceName == "Store" || sourceName == "Stores" ||
            sourceName == "StoreId")
            {

                //   NTOE: THIS ALWAYS NULL
                if (Stores == null)
                {
                    Stores = new List<SelectOption>();
                    return Stores;
                }
                else
                    return Stores;



                //return new List<SelectOption>();
            }
            if (sourceName == "Employee" || sourceName == "Employees" ||
                sourceName == "EmployeeId")
            {
                // NTOE: THIS ALWAYS NULL
                if (Employees == null)
                {
                    Employees = new List<SelectOption>();
                    return Employees;
                }
                else return Employees;

            }
            return new List<SelectOption>();

        }

    }
}
