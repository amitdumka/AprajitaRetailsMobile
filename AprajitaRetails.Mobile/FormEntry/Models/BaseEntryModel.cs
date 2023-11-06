using AprajitaRetails.Mobil.Converters;
using Syncfusion.Maui.DataForm;
using System.ComponentModel.DataAnnotations;

namespace AprajitaRetails.Mobile.FormEntry.Models
{
    [ObservableRecipient]
    public partial class BaseEntryModel : ObservableValidator, INotifyPropertyChanged
    {
    }

    public partial class EmployeeEM
    {

        public EmployeeEM() { 
        
            this.JoiningDate=DateTime.Now;
            this.City = CurrentSession.CityName;  this.State = "Jharkhand";
            this.ZipCode = "814101"; this.Country = "India";
            this.EmailId = this.Address = ""; this.Category = EmpType.Salesman;
            this.StoreId = CurrentSession.StoreCode;
            this.Gender = Gender.Male; this.MobileNumber= this.PhoneNumber = ""; 
            this.BirthDate= DateTime.Now.AddYears(-18);
            this.IsWorking = true; this.FirstName = this.LastName = "";
             
        }

        [ReadOnly(true)]
        [Editable(false)]
        [Display(AutoGenerateField = false)]
        public string EmployeeId { get; set; }

        [Required(ErrorMessage = "Please select Store")]
        public string StoreId { get; set; }




        [Display(GroupName = "Name")]
        public string? Title { get; set; }
        [Display(GroupName = "Name")]

        [Required(ErrorMessage = "Please type First Name, it cant be empty ")]
        public string FirstName { get; set; }
        [Display(GroupName = "Name")]

        [Required(ErrorMessage = "Please type Last Name, it cant be empty ")]
        public string LastName { get; set; }


       
        //[ObservableProperty]
        [Required(ErrorMessage = "Please type Mobile Number, it cant be empty ")]
        [DataType(DataType.PhoneNumber)]
        [Display(GroupName = "Contact")]
        public string MobileNumber{get;set;}
        //[ObservableProperty]
        [Display(GroupName = "Contact")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber{get;set;}
        [Display(GroupName = "Contact", Name = "Email")]
        //[ObservableProperty]
       
        [DataType(DataType.EmailAddress)]
        public string EmailId{get;set;}

       
        //[ObservableProperty]
        [DataType(DataType.MultilineText)]
        public string? Address{get;set;}



        [Display(GroupName = "Address")]
        //[ObservableProperty]
        [Required(ErrorMessage = "Please type City Name, it cant be empty ")]
        public string? City{get;set;}
        [Display(GroupName = "Address")]
        //[ObservableProperty]
        [DataType(DataType.PostalCode)]
        public string ZipCode{get;set;}

        [Display(GroupName = "Address")]
        //[ObservableProperty]
        public string? State{get;set;}
        [Display(GroupName = "Address")]
        //[ObservableProperty]
        public string? Country{get;set;}


        //[ObservableProperty]
        public Gender Gender{get;set;}

        //[ObservableProperty]
        [Required(ErrorMessage = "Please select Employee Type ")]
        public EmpType Category{get;set;}
        //[ObservableProperty]
        [Required(ErrorMessage = "Please select date, it cant be empty ")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate{get;set;}

        //[ObservableProperty]
        [DataFormDateRange(MinimumDate = "17/02/2016", ErrorMessage = "Attendance cannot be beyond 16/Feb/2016, date is invalid")]
        [Required(ErrorMessage = "Please select date, it cant be empty ")]
        [DataType(DataType.Date)]
        public DateTime JoiningDate{get;set;}// { get{get;set;} set{get;set;} }

        //[ObservableProperty]
        public bool IsWorking{get;set;}

    }


    public partial class EmployeeEM_O : BaseEntryModel
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
        [Display(GroupName = "Name")]
        private string? title;
        [Display(GroupName = "Name")]
        [ObservableProperty]
        [Required(ErrorMessage = "Please type First Name, it cant be empty ")]
        private string firstName;
        [Display(GroupName = "Name")]
        [ObservableProperty]
        [Required(ErrorMessage = "Please type Last Name, it cant be empty ")]
        private string lastName;

        //[Display(GroupName = "Contact Details")]
        [ObservableProperty]
        [Required(ErrorMessage = "Please type Mobile Number, it cant be empty ")]
        [DataType(DataType.PhoneNumber)]
        [Display(GroupName = "Contact")]
        private string mobileNumber;
        [ObservableProperty]
        [Display(GroupName = "Contact")]
        [DataType(DataType.PhoneNumber)]
        private string? phoneNumber;
        [Display(GroupName = "Contact", Name = "Email")]
        [ObservableProperty]
        //[Display(Name ="Email")]
        [DataType(DataType.EmailAddress)]
        private string emailId;

        // [Display(GroupName = "Address")]
        [ObservableProperty]
        [DataType(DataType.MultilineText)]
        private string? address;



        [Display(GroupName = "Address")]
        [ObservableProperty]
        [Required(ErrorMessage = "Please type City Name, it cant be empty ")]
        private string? city;
        [Display(GroupName = "Address")]
        [ObservableProperty]
        [DataType(DataType.PostalCode)]
        private string zipCode;

        [Display(GroupName = "Address")]
        [ObservableProperty]
        private string? state;
        [Display(GroupName = "Address")]
        [ObservableProperty]
        private string? country;


        [ObservableProperty]
        private Gender gender;

        [ObservableProperty]
        [Required(ErrorMessage = "Please select Employee Type ")]
        private EmpType category;
        [ObservableProperty]
        [Required(ErrorMessage = "Please select date, it cant be empty ")]
        [DataType(DataType.Date)]
        private DateTime? birthDate;
        [ObservableProperty]
        //[DataFormDateRange(MinimumDate = "17/02/2016", ErrorMessage = "Attendance cannot be beyond 16/Feb/2016, date is invalid")]
        [Required(ErrorMessage = "Please select date, it cant be empty ")]
        [DataType(DataType.Date)]
        public DateTime joiningDate;// { get; set; }

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


        public string? Remarks { get; set; }
    }
}