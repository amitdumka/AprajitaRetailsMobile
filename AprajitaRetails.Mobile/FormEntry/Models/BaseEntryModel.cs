using AprajitaRetails.Mobil.Converters;
using Syncfusion.Maui.DataForm;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AprajitaRetails.Mobile.FormEntry.Models
{
    [ObservableRecipient]
    public partial class BaseEntryModel : ObservableValidator, INotifyPropertyChanged
    {
    }

    public partial class VoucherEM
    {
        [Key]
        [ReadOnly(true)]
        [Editable(false)]
        [Display(AutoGenerateField = false)]
        public string VoucherNumber { get; set; }

        [Required(ErrorMessage = "Please select Store")]
        public string StoreId { get; set; }

        [Required(ErrorMessage = "Please select Voucher Type")]
        [Display(Name = "Voucher Type")]
        public VoucherType VoucherType { get; set; }

        [DataFormDateRange(MinimumDate = "17/02/2016", ErrorMessage = "Voucher date cannot be beyond 16/Feb/2016, date is invalid")]
        [Required(ErrorMessage = "Please select date, it cant be empty ")]
        [DataType(DataType.Date)]
        public DateTime OnDate { get; set; }

        [Display(Name = "Slip No")]
        public string SlipNumber { get; set; }

        [Display(Name = "Party Name")]
        [Required(ErrorMessage = "Please enter Party, it cant be empty ")]
        public string PartyName { get; set; }

        [Display(Name = "Particulars")]
        [Required(ErrorMessage = "Please enter Particulars, it cant be empty ")]
        public string Particulars { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "Please enter Amount, it cant be empty or Zero ")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        [Required(ErrorMessage = "Please select Payment Mode, it cant be empty ")]
        public PaymentMode PaymentMode { get; set; }

        [Display(Name = "Payment Details")]
      
        public string? PaymentDetails { get; set; }

        [Display(Name = "Remarks")]
        [Required(ErrorMessage = "Please enter remarks, it cant be empty ")]
        public string? Remarks { get; set; }

        [Display(Name = "Bank Account")]
        public string? AccountId { get; set; }

        [Display(Name = "Issued By")]
        public string EmployeeId { get; set; }

        [Display(Name = "Ledger")]
        public string PartyId { get; set; }
    }

    public partial class CashVoucherEM
    {
        [Key]
        [ReadOnly(true)]
        [Editable(false)]
        [Display(AutoGenerateField = false)]
        public string CashVoucherNumber { get; set; }

        [Required(ErrorMessage = "Please select Store")]
        public string StoreId { get; set; }

        [Required(ErrorMessage = "Please select Voucher Type")]
        [Display(Name = "Voucher Type")]
        public VoucherType VoucherType { get; set; }

        [Required(ErrorMessage = "Please select Transcation Type")]
        [Display(Name = "Transcation Type")]
        public string TransactionId { get; set; }

        [DataFormDateRange(MinimumDate = "17/02/2016", ErrorMessage = "Voucher date cannot be beyond 16/Feb/2016, date is invalid")]
        [Required(ErrorMessage = "Please select date, it cant be empty ")]
        [DataType(DataType.Date)]
        public DateTime OnDate { get; set; }

        [Display(Name = "Slip No")]
        public string SlipNumber { get; set; }

        [Display(Name = "Party Name")]
        [Required(ErrorMessage = "Please enter Party, it cant be empty ")]
        public string PartyName { get; set; }

        [Display(Name = "Particulars")]
        [Required(ErrorMessage = "Please enter Particulars, it cant be empty ")]
        public string Particulars { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "Please enter Amount, it cant be empty or Zero ")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Display(Name = "Remarks")]
        [Required(ErrorMessage = "Please enter remarks, it cant be empty ")]
        public string? Remarks { get; set; }

        [Display(Name = "Issued By")]
        public string EmployeeId { get; set; }

        [Display(Name = "Ledger")]
        public string PartyId { get; set; }
    }

    public partial class NoteEM
    {
        [Key]
        [ReadOnly(true)]
        [Editable(false)]
        [Display(AutoGenerateField = false)]
        public string NoteNumber { get; set; }

        [Required(ErrorMessage = "Please select Store")]
        public string StoreId { get; set; }

        [Display(Name = "Note Type")]
        [Required(ErrorMessage = "Please select type , it can not be empty!")]
        public NotesType NotesType { get; set; }

        [Display(Name = "Date")]
        [DataFormDateRange(MinimumDate = "17/02/2016", ErrorMessage = "Voucher date cannot be beyond 16/Feb/2016, date is invalid")]
        [Required(ErrorMessage = "Please select date, it cant be empty ")]
        [DataType(DataType.Date)]
        public DateTime OnDate { get; set; }

        [Display(Name = "Party Name")]
        [Required(ErrorMessage = "Please enter party name , it can not be empty!")]
        public string PartyName { get; set; }

        [Display(Name = "GST")]
        public bool WithGST { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Please enter Amount , it can not be empty!")]
        public decimal Amount { get; set; }

        [Display(Name = "Tax Rate")]
        [Required(ErrorMessage = "Please enter tax Rate , it can not be empty!")]
        public decimal TaxRate { get; set; }

        [Required(ErrorMessage = "Please enter reson , it can not be empty!")]
        public string Reason { get; set; }

        [Required(ErrorMessage = "Please enter remarks , it can not be empty!")]
        public string Remarks { get; set; }

        [Display(Name = "Ledger")]
        public string PartyId { get; set; }
    }

    public partial class EmployeeEM
    {
        public EmployeeEM()
        {
            this.JoiningDate = DateTime.Now;
            this.City = CurrentSession.CityName; this.State = "Jharkhand";
            this.ZipCode = "814101"; this.Country = "India";
            this.EmailId = this.Address = ""; this.Category = EmpType.Salesman;
            this.Gender = Gender.Male; this.MobileNumber = this.StreetName = "";
            this.BirthDate = DateTime.Now.AddYears(-18);
            this.IsWorking = true; this.FirstName = this.LastName = "";
            this.Title = "Mr.";
            this.StoreId = CurrentSession.StoreCode;
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
        public string MobileNumber { get; set; }

        //[ObservableProperty]

        [Display(GroupName = "Contact", Name = "Email")]
        //[ObservableProperty]

        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [Display(GroupName = "Address", Name = "Street Name")]
        public string? StreetName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(GroupName = "Address")]
        public string? Address { get; set; }

        [Display(GroupName = "Address")]
        [Required(ErrorMessage = "Please type City Name, it cant be empty ")]
        public string? City { get; set; }

        [Display(GroupName = "Address")]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }

        [Display(GroupName = "Address")]
        public string? State { get; set; }

        [Display(GroupName = "Address")]
        public string? Country { get; set; }

        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Please select Employee Type ")]
        public EmpType Category { get; set; }

        [Required(ErrorMessage = "Please select date, it cant be empty ")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [DataFormDateRange(MinimumDate = "17/02/2016", ErrorMessage = "Employee Joining date cannot be beyond 16/Feb/2016, date is invalid")]
        [Required(ErrorMessage = "Please select date, it cant be empty ")]
        [DataType(DataType.Date)]
        public DateTime JoiningDate { get; set; }

        [Display(Name = "Working")]
        public bool IsWorking { get; set; }
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