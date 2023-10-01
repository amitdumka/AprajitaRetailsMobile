////using AKS.Shared.Commons.Models.Accounts;
using AprajitaRetails.Shared.Models.Vouchers;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Mobile.DataModels.Accounting
{
    [Obsolete]
    public class VouchersDataModel : Base.BaseDataModel<Voucher, CashVoucher, Note>
    {
        public VouchersDataModel(ConType conType) : base(conType)
        {
        }

        public VouchersDataModel(ConType conType,RolePermission role) : base(conType, role)
        {
        }

        #region IDGen

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override Task<string> GenrateYID()
        {
            throw new NotImplementedException();
        }

        public override Task<string> GenrateZID()
        {
            throw new NotImplementedException();
        }

        #endregion IDGen

        public override async Task<bool> InitContext()
        {
            return Connect();
        }

        #region Vouchers

        public override List<Voucher> GetFiltered(QueryParam query)
        {
            //public int Id { get; set; }
            //public string Ids { get; set; }
            //public List<string> Command { get; set; }
            //public List<string> Query { get; set; }
            //public int Order { get; set; }
            //public List<string> Filters { get; set; }
            //public int StoreId { get; set; }

            throw new NotImplementedException();
        }

        public override async Task<List<Voucher>> GetItemsAsync(string storeid)
        {
            if (Permissions.Contains("RW"))
            {
                var db = GetContext();

                return await db.Vouchers.Where(c => c.StoreId == storeid && c.OnDate.Year == DateTime.Today.Year)
                                  .OrderByDescending(c => c.OnDate)
                                  .ToListAsync();
            }
            IsError = true;
            ErrorMsg = "Access Deninde";
            return null;
        }

        #endregion Vouchers

        #region WhereQuerry

        public IQueryable<Voucher> WhereO(System.Linq.Expressions.Expression<Func<Voucher, bool>> predict)
        {
            return GetContext().Vouchers.Where(predict);
        }

        public IQueryable<CashVoucher> WhereO(System.Linq.Expressions.Expression<Func<CashVoucher, bool>> predict)
        {
            return GetContext().CashVouchers.Where(predict);
        }

        public IQueryable<Note> WhereO(System.Linq.Expressions.Expression<Func<Note, bool>> predict)
        {
            return GetContext().Notes.Where(predict);
        }

        #endregion WhereQuerry

        #region YearList

        public override List<int> GetYearList(string storeid)
        {
            var db = GetContext();
            return db.Vouchers.Where(c => c.StoreId == storeid).Select(c => c.OnDate.Year).Distinct().ToList();
        }

        public override List<int> GetYearList()
        {
            var db = GetContext();
            return db.Vouchers.Select(c => c.OnDate.Year).Distinct().ToList();
        }

        public override Task<List<int>> GetYearListY(string storeid)
        {
            var db = GetContext();
            return db.CashVouchers.Where(c => c.StoreId == storeid).Select(c => c.OnDate.Year).Distinct().ToListAsync();
        }

        public override Task<List<int>> GetYearListY()
        {
            var db = GetContext();
            return db.CashVouchers.Select(c => c.OnDate.Year).Distinct().ToListAsync();
        }

        public override Task<List<int>> GetYearListZ(string storeid)
        {
            var db = GetContext();
            return db.Notes.Where(c => c.StoreId == storeid).Select(c => c.OnDate.Year).Distinct().ToListAsync();
        }

        public override Task<List<int>> GetYearListZ()
        {
            var db = GetContext();
            return db.Notes.Select(c => c.OnDate.Year).Distinct().ToListAsync();
        }

        #endregion YearList

        #region CashVouchers

        public override Task<List<CashVoucher>> GetYFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<CashVoucher>> GetYItems(string storeid)
        {
            if (Permissions.Contains("RW"))
            {
                var db = GetContext();
                return await db.CashVouchers.Where(c => c.StoreId == storeid && c.OnDate.Year == DateTime.Today.Year)
                      .OrderByDescending(c => c.OnDate).ToListAsync();
            }
            IsError = true;
            ErrorMsg = "Access Deninde";
            return null;
        }

        #endregion CashVouchers

        #region Notes

        public override Task<List<Note>> GetZFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Note>> GetZItems(string storeid)
        {
            if (Permissions.Contains("RW"))
            {
                var db = GetContext();
                return db.Notes.Where(c => c.StoreId == storeid && c.OnDate.Year == DateTime.Today.Year)
                      .OrderByDescending(c => c.OnDate).ToListAsync();
            }
            IsError = true;
            ErrorMsg = "Access Deninde";
            return null;
        }

        #endregion Notes

        #region CustomCount

        public int Count(VoucherType type)
        {
            int count = 0;
            switch (type)
            {
                case VoucherType.Payment:
                case VoucherType.Receipt:
                case VoucherType.Expense:
                    count = GetContextAzure().Vouchers.Count(c => c.VoucherType == type);
                    break;

                //case VoucherType.Contra:
                //    break;
                //case VoucherType.DebitNote:
                //    break;
                //case VoucherType.CreditNote:
                //    break;
                //case VoucherType.JV:
                //    count = GetContextAzure().Notes.Count(c => c.NotesType == type);
                //    break;

                case VoucherType.CashReceipt:
                case VoucherType.CashPayment:
                    count = GetContextAzure().CashVouchers.Count(c => c.VoucherType == type);
                    break;

                default:

                    break;
            }
            return count;
        }

        #endregion CustomCount

        #region SyncUp

        //Todo in add/update
        public void SyncUp(Voucher v, bool isnew = true, bool delete = false)
        {
            var db = GetContextAzure();
            if (delete)
               db.Vouchers.Remove(v);
            else
            {
                if (v.AccountId == null)
                    v.AccountId = "";
                if(v.PartyId==null) 
                    v.PartyId = "ARD/PTY/43";
                if (isnew)
                    db.Vouchers.Add(v);
                else
                    db.Vouchers.Update(v);
            }
           var x= db.SaveChanges();
            if (x > 0)
            {
                Console.WriteLine("c");
            }
            else 
                Console.WriteLine("e");
        }

        public void SyncUp(CashVoucher v, bool isnew = true, bool delete = false)
        {
            if (delete)
            {
                GetContextAzure().CashVouchers.Remove(v);
                if (isnew)
                    GetContextAzure().CashVouchers.AddAsync(v);
                else
                    GetContextAzure().CashVouchers.Update(v);
            }
            GetContextAzure().SaveChangesAsync();
        }

        public void SyncUp(Note n, bool isnew = true, bool delete = false)
        {
            if (delete)
                GetContextAzure().Notes.Remove(n);
            {
                if (isnew)
                    GetContextAzure().Notes.AddAsync(n);
                else
                    GetContextAzure().Notes.Update(n);
            }
            GetContextAzure().SaveChangesAsync();
        }

        public void SyncUp()
        {
        }

        #endregion SyncUp
    }
}