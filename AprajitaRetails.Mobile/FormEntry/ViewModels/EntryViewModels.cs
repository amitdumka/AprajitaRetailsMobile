using AprajitaRetails.Mobile.FormEntry.Models;

namespace AprajitaRetails.Mobile.FormEntry.ViewModels
{
    public partial class AttendanceEntryViewModel : BaseEntryViewModel<AttendanceEM>
    {
        public AttendanceEntryViewModel() : base()
        {
            HeaderText = "Attendance";
            Entity = new AttendanceEM
            {
                OnDate = DateTime.Now,
                EmployeeId = "ARD-2016-SM-1",
                StoreId = CurrentSession.StoreCode,
                EntryTime = DateTime.Now.ToShortTimeString(),
                Remarks = "",
                Status = AttUnit.Absent
            };
        }
    }

    public partial class EmployeeEntryViewModel : BaseEntryViewModel<EmployeeEM>
    {
        public EmployeeEntryViewModel() : base()
        {
            HeaderText = "Employee";
            Entity = new EmployeeEM { StoreId = CurrentSession.StoreCode, IsWorking = true, Gender = Gender.Male, BirthDate = DateTime.Now.AddYears(-18), Category = EmpType.Salesman, JoiningDate = DateTime.Now, City = string.IsNullOrEmpty(CurrentSession.CityName) ? "" : CurrentSession.CityName };
        }
    }

    public partial class VoucherEntryViewModel : BaseEntryViewModel<VoucherEM>
    {
        public VoucherEntryViewModel() : base()
        {
            HeaderText = "Voucher";
            Entity = new VoucherEM
            {
                OnDate = DateTime.Now,
                EmployeeId = "ARD-2016-SM-1",
                StoreId = CurrentSession.StoreCode,
                Remarks = "",
                Amount = 0,
                VoucherNumber = "",
                PartyId = "",
                Particulars = "",
                PartyName = "",
                PaymentDetails = null,
                PaymentMode = PaymentMode.Cash,
                SlipNumber = "",
                AccountId = null,
                VoucherType = VoucherType.CashPayment
            };
        }
    }

    public partial class CashVoucherEntryViewModel : BaseEntryViewModel<CashVoucherEM>
    {
        public CashVoucherEntryViewModel() : base()
        {
            HeaderText = "Cash Voucher";
            Entity = new CashVoucherEM
            {
                OnDate = DateTime.Now,
                EmployeeId = "ARD-2016-SM-1",
                StoreId = CurrentSession.StoreCode,
                Remarks = "",
                Amount = 0,
                CashVoucherNumber = "",
                PartyId = "",
                Particulars = "",
                PartyName = "",
                 
                SlipNumber = "",
                TransactionId = "",
                VoucherType = VoucherType.CashPayment
            };
        }
    }

    public partial class NoteEntryViewModel : BaseEntryViewModel<NoteEM>
    {
        public NoteEntryViewModel() : base()
        {
            HeaderText = "Note";
            Entity = new NoteEM
            {
                OnDate = DateTime.Now,
                StoreId = CurrentSession.StoreCode,
                Remarks = "",
                Amount = 0,
                NoteNumber = "",
                NotesType = NotesType.DebitNote,
                PartyName = "",
                Reason = "",
                TaxRate = 0,
                PartyId = "",
                WithGST = false
            };
        }
    }
}