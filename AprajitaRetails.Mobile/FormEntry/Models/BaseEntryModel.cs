using AprajitaRetails.Mobil.Converters;
using Syncfusion.Maui.DataForm;
using System.ComponentModel.DataAnnotations;

namespace AprajitaRetails.Mobile.FormEntry.Models
{
    [ObservableRecipient]
    public partial class BaseEntryModel : ObservableValidator, INotifyPropertyChanged
    {
    }

    public partial class EmployeeEM : BaseEntryModel
    {
        [ObservableProperty]
        [Required(ErrorMessage = "Please select Store")]
        private string storeId;

        [ObservableProperty]
        [ReadOnly(true)]
        [Editable(false)]
        [Display(AutoGenerateField = false)]
        private string employeeId;

        [ObservableProperty]
        private string? title;

        [ObservableProperty]
        [Required(ErrorMessage = "Please type First Name, it cant be empty ")]
        private string firstName;

        [ObservableProperty]
        [Required(ErrorMessage = "Please type Last Name, it cant be empty ")]
        private string lastName;

        [ObservableProperty]
        [Required(ErrorMessage = "Please type Mobile Number, it cant be empty ")]
        private string mobileNumber;

        [ObservableProperty]
        private string? phoneNumber;

        [ObservableProperty]
        [Required(ErrorMessage = "Please type City Name, it cant be empty ")]
        private string? city;

        [ObservableProperty]
        private string? country;

        [ObservableProperty]
        private string zipCode;

        [ObservableProperty]
        private string? state;

        [ObservableProperty]
        private string emailId;

        [ObservableProperty]
        private string? address;

        [ObservableProperty]
        [Required(ErrorMessage = "Please select date, it cant be empty ")]
        [DataType(DataType.Date)]
        private DateTime? birthDate;

        [ObservableProperty]
        private Gender gender;

        [ObservableProperty]
        [Required(ErrorMessage = "Please select Employee Type ")]
        private EmpType category;

        //[ObservableProperty]
        [DataFormDateRange(MinimumDate = "17/02/2016", ErrorMessage = "Attendance cannot be beyond 16/Feb/2016, date is invalid")]
        [Required(ErrorMessage = "Please select date, it cant be empty ")]
        [DataType(DataType.Date)]
        public DateTime JoiningDate { get; set; }

        [ObservableProperty]
        private bool isWorking;
    }

    public partial class AttendanceEM 
    {
        public AttendanceEM()
        {
            this.AttendanceId = string.Empty;
            this.EntryTime = DateTime.Now.ToShortTimeString();
            this.Status = AttUnit.SundayHoliday;
            this.Remarks = string.Empty;
            this.StoreId = "ARD";
            this.EmployeeId = "ARD-2016-SM-1";
            this.OnDate = DateTime.Now;
        }

        [Required(ErrorMessage = "Please select Store")]
        //[ObservableProperty]
        public string StoreId{get;set;}

        //public string StoreId { get{get;set;} set{get;set;} }

        [ReadOnly(true)]
        [Editable(false)]
        [Display(AutoGenerateField = false)]
        //[ObservableProperty]
        public string AttendanceId{get;set;}

        [Required(ErrorMessage = "Please select Employee")]
        //[ObservableProperty]
        public string EmployeeId{get;set;}

        [Display(GroupName = "Date Time", Name = "Date")]
        [DataFormDateRange(MinimumDate = "17/02/2016", ErrorMessage = "Attendance cannot be beyond 16/Feb/2016, date is invalid")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please select Date")]
        public DateTime OnDate { get; set;} 

        //public DateTime onDate{get;set;}

        //public DateTime OnDate { get{get;set;} set{get;set;} }

        [Display(GroupName = "Date Time")]
        [DataType(DataType.Time)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Entry Time should not be empty")]
        [DataFormValueConverter(typeof(StringToTimeConverter))]
        public string EntryTime { get; set; }

        //public string entryTime{get;set;}

        [Display(Name = "Attndance")]
        [Required(ErrorMessage = "Please select Attendance status")]
        //[ObservableProperty]
        public AttUnit Status{get;set;}

        //[ObservableProperty]
        public string? Remarks{get;set;}
    }
}