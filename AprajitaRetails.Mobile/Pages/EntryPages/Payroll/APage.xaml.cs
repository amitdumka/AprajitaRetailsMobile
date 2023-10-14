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

            cbx.SelectedValuePath = "ID";
            cbx.DisplayMemberPath = "Value";
            // dataForm.BindingContext = viewModel;
            (dataForm.DataObject as Attendance).PropertyChanged += MainPage_PropertyChanged;
            viewModel.Attendance.PropertyChanged += MainPage_PropertyChanged;
            
        }
        private void MainPage_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)

        {

            if ( !string.IsNullOrEmpty(e.PropertyName))

            {

                dataForm.UpdateEditor(e.PropertyName);

            }

        }
        private void PrimaryButton_Clicked(object sender, EventArgs e)
        {

            //viewModel.Attendance = new Attendance
            //{
            //    AttendanceId = string.Empty,
            //    EntryTime = DateTime.Now.ToShortTimeString(),
            //    Status = AttUnit.SundayHoliday,
            //    Remarks = string.Empty,
            //    StoreId = "JCK",
            //    EmployeeId = "ARD-2016-SM-1",
            //    OnDate = DateTime.Now.AddDays(-11)
            //};

            //viewModel.Attendance.AttendanceId = "1";
            //viewModel.Attendance.EntryTime = "1";
            viewModel.Attendance.StoreId     = "ARD";
            viewModel.Attendance.EmployeeId = "ARD-2016-SM-4";

            //dataForm.DataObject = viewModel.Attendance;
        }

        private void SecondaryButton_Clicked(object sender, EventArgs e)
        {
            cbx.SelectedValuePath = "ID";
            cbx.DisplayMemberPath = "Value";
            cbx.ItemsSource = viewModel.Employees;
        }
    }

    [ObservableRecipient]
    public partial class AttendanceDFViewModel : ObservableObject, INotifyPropertyChanged
    {
        [ObservableProperty]
        private List<SelectOption> stores;

        [ObservableProperty]
        private List<SelectOption> employees;

        [ObservableProperty]
        private Attendance attendance;//{ get; set; }

        //public Attendance Attendance { get; set; }
        public AttendanceDFViewModel()
        {
            LoadDataSources();
            Attendance = new Attendance();
        }

        private async void LoadDataSources()
        {
            // if (Stores == null || !Stores.Any())
           // var x = RestService.GetEmployeeListAsync(CurrentSession.StoreCode);
            //var y = RestService.GetStoreListAsync();

            Stores = await GetStoreListAsync();
            // if (Employees == null || !Employees.Any())
            Employees = await GetEmployeeListAsync(CurrentSession.StoreCode);
        }
        private async Task<List<SelectOption>> GetStoreListAsync()
        {
            var stores = "[{\"ID\":\"ARD\",\"Value\":\"Aprajita Retails, #: Dumka\"},{\"ID\":\"ARJ\",\"Value\":\"Aprajita Retails, Jamshedpur, #: Jamshedpur\"},{\"ID\":\"JKD\",\"Value\":\"Aprajita Retails(Jockey EBO), #: Dumka\"}]";

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
           // storeDetails = JsonSerializer.Deserialize<List<SelectOption>>(stores);
            return storeDetails;
        }
        private async Task<List<SelectOption>> GetEmployeeListAsync(string sc)
        {
            var emp = "[{\"ID\":\"ARD-2023-HK\",\"Value\":\"Keli Devi\"},{\"ID\":\"ARD-2016-SM-1\",\"Value\":\"Alok Kumar\"},{\"ID\":\"ARD-2020-ACC-11\",\"Value\":\"Geetanjali Kumari Verma\"}]";

            List<SelectOption> storeDetails = new List<SelectOption>();
            storeDetails.Add(new SelectOption()
            {
                Value =
            "Alok",
                ID = "ARD-2016-SM-1"
            });
            storeDetails.Add(new SelectOption()
            {
                Value =
            " Amit Thakur",
                ID = "ARD-2016-SM-2"
            });
            storeDetails.Add(new SelectOption()
            {
                Value =
            "Mukesh",
                ID = "ARD-2016-SM-4"
            });
            storeDetails.Add(new SelectOption()
            {
                Value =
            "Geetanjali",
                ID = "ARD-2016-SM-3"
            });
            await Task.Delay(1000);
          // storeDetails = JsonSerializer.Deserialize<List<SelectOption>>(emp);
            return storeDetails;
        }
    }

    [INotifyPropertyChanged]
    public partial class Attendance 
    {
        public Attendance()
        {
            this.AttendanceId = string.Empty;
            this.EntryTime = DateTime.Now.ToShortTimeString();
            this.Status = AttUnit.SundayHoliday;
            this.Remarks = string.Empty;
            this.StoreId = "ARD";
            this.EmployeeId = "ARD-2016-SM-2";
            this.OnDate = DateTime.Now;
        }

       // [Required(ErrorMessage = "Please select Store")]
        [ObservableProperty]
        private string storeId;
        //public string StoreId { get; set; }


        [ReadOnly(true)]
        [Editable(false)]
        [Display(AutoGenerateField = false)]
        [ObservableProperty]
        private string attendanceId;
        //public string AttendanceId { get; set; }

        //[Required(ErrorMessage = "Please select Employee")]
        [ObservableProperty]
        private string employeeId;

        //public event PropertyChangedEventHandler PropertyChanged;

        //public string EmployeeId { get; set; }

        [Display(GroupName = "Date Time", Name = "Date")]
        //[DataFormDateRange(MinimumDate = "17/02/2016", ErrorMessage = "Attendance cannot be beyond 16/Feb/2016, date is invalid")]
        //[DataType(DataType.Date)]
        //[Required(ErrorMessage = "Please select Date")]
        [ObservableProperty]
        private DateTime onDate;
        //public DateTime OnDate { get; set; }

        [Display(GroupName = "Date Time")]
        //[DataType(DataType.Time)]
       // [Required(AllowEmptyStrings = false, ErrorMessage = "Entry Time should not be empty")]
        //[DataFormValueConverter(typeof(StringToTimeConverter))]
        [ObservableProperty]
        private string entryTime;
        //public string EntryTime { get; set; }

        [Display(Name = "Attndance")]
        //[Required(ErrorMessage = "Please select Attendance status")]
        [ObservableProperty]
        private AttUnit status;
        //public AttUnit Status { get; set; }

       // [Required(AllowEmptyStrings = false, ErrorMessage = "Remarks is requried")]
        [ObservableProperty]
        private string? remarks;
        //public string? Remarks { get; set; }

        [Display(Name = "Tailor")]
        [ObservableProperty]
        private bool isTailoring;
        //public bool IsTailoring { get; set; }
    }

    public class AttendanceFormBehvour : Behavior<SfDataForm>
    {
        private Attendance Attendance { get; set; }
        private SfDataForm DataForm { get; set; }
        private AttendanceDFViewModel viewModel;

        protected override async void OnAttachedTo(SfDataForm dataForm)
        {
            base.OnAttachedTo(dataForm);
            // dataForm = bindable.Content.FindByName<SfDataForm>("dataForm");

            if (dataForm != null)
            {
                DataForm = dataForm;
                dataForm.ColumnCount = 2;
                // dataForm.ItemsSourceProvider = new ItemSourceProvider2();

                dataForm.RegisterEditor(nameof(Attendance.EmployeeId), DataFormEditorType.ComboBox);
                dataForm.RegisterEditor(nameof(Attendance.StoreId), DataFormEditorType.ComboBox);
                dataForm.RegisterEditor("IsTailoring", DataFormEditorType.Switch);
                //dataForm.GenerateDataFormItem += this.OnGenerateDataFormItem;
                viewModel = DataForm.BindingContext as AttendanceDFViewModel;
                dataForm.Commit();
                dataForm.GenerateDataFormItem += OnGenerateDataFormItem;
               // (dataForm.DataObject as Attendance).PropertyChanged += OnDataObjectPropertyChanged;
               
                //await WorkArroundForComboBoxLoad();
            }
        }

        private async Task WorkArroundForComboBoxLoad()
        {
            Attendance = new Attendance
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
                viewModel.Attendance = Attendance;
                DataForm.DataObject = Attendance;
            }
            else
            {
                await Task.Delay(10000);
                viewModel.Attendance = Attendance;
                DataForm.DataObject = Attendance;
            }
        }

        private void OnGenerateDataFormItem(object sender, GenerateDataFormItemEventArgs e)
        {
            if (e.DataFormItem != null && (e.DataFormItem.FieldName == "StoreId" || e.DataFormItem.FieldName == "Store") && e.DataFormItem is DataFormComboBoxItem comboBoxItem)
            {
                // e.DataFormItem.LabelText = "Store";
                comboBoxItem.DisplayMemberPath = "Value";
                comboBoxItem.SelectedValuePath = "ID";
                comboBoxItem.IsEditable = true;

                var viewModel = DataForm.BindingContext as AttendanceDFViewModel;
                comboBoxItem.BindingContext = viewModel;
                comboBoxItem.SetBinding(DataFormComboBoxItem.ItemsSourceProperty, nameof(viewModel.Stores));
            }

            if (e.DataFormItem != null && (e.DataFormItem.FieldName == "EmployeeId" || e.DataFormItem.FieldName == "Employee") && e.DataFormItem is DataFormComboBoxItem cbEmp)
            {
                // e.DataFormItem.LabelText = "Employee";
                cbEmp.DisplayMemberPath = "Value";
                cbEmp.SelectedValuePath = "ID";
                cbEmp.IsEditable = true;

                var viewModel = DataForm.BindingContext as AttendanceDFViewModel;
                cbEmp.BindingContext = viewModel;

                cbEmp.SetBinding(DataFormComboBoxItem.ItemsSourceProperty, nameof(viewModel.Employees));

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
        private void OnDataObjectPropertyChanged(object sender,PropertyChangedEventArgs e)

        {

            if (DataForm!=null&& !string.IsNullOrEmpty(e.PropertyName))

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
    }

    public class ItemSourceProvider2 : IDataFormSourceProvider
    {
        public ItemSourceProvider2()
        {
            LoadDataSources();
        }


string storejson="[{id:ARD, value:dumka}, {id:ARJ, value:Jamshedpur}]";

string empjson="[{id:ARD004, value:alok},{id:ARD001,value:mukesh}]";

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