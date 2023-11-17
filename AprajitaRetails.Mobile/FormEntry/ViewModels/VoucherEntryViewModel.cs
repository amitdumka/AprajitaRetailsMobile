using AprajitaRetails.Mobile.FormEntry.Models;

namespace AprajitaRetails.Mobile.FormEntry.ViewModels
{

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
                Remarks = string.Empty,
                Amount = 0,
                VoucherNumber = string.Empty,
                PartyId = string.Empty,
                Particulars = string.Empty,
                PartyName = string.Empty,
                PaymentDetails = null,
                PaymentMode = PaymentMode.Cash,
                SlipNumber = string.Empty,
                AccountId = null,
                VoucherType =(VchType) ((int)VoucherType.Expense)
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
                Remarks = string.Empty,
                Amount = 0,
                CashVoucherNumber = string.Empty,
                PartyId = string.Empty,
                Particulars = string.Empty,
                PartyName = string.Empty,

                SlipNumber = string.Empty,
                TransactionId = string.Empty,
                VoucherType = (CashVchType)VoucherType.CashPayment
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
                Remarks = string.Empty,
                Amount = 0,
                NoteNumber = string.Empty,
                NotesType = NotesType.DebitNote,
                PartyName = string.Empty,
                Reason = string.Empty,
                TaxRate = 0,
                PartyId = string.Empty,
                WithGST = false
            };
        }
    }
}