////using AKS.Shared.Commons.Models;
////using AKS.Shared.Commons.Models.Sales;
using AprajitaRetails.Mobile.DataModels.Base;
using AprajitaRetails.Shared.Models.Stores;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Mobile.DataModels.Accounting
{
    [Obsolete]
    public class DailySale_DataModel : BaseDataModel<DailySale, CustomerDue, DueRecovery>
    {
        public DailySale_DataModel(ConType conType) : base(conType)
        {
        }

        public DailySale_DataModel(ConType conType, RolePermission role) : base(conType, role)
        {
        }

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

        public override List<DailySale> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DailySale>> GetItemsAsync(string storeid, bool today, int fil)
        {
            if (today)
            {
                return await GetContext().DailySales.Where(c => c.StoreId == storeid && c.OnDate.Date == DateTime.Today.AddDays(fil).Date)
                 .OrderByDescending(c => c.InvoiceNumber).ToListAsync();
            }
            else
            {
                return await GetContext().DailySales.Where(c => c.StoreId == storeid && c.OnDate.Year == DateTime.Today.Year && c.OnDate.Month == DateTime.Today.Month + fil)
                  .OrderByDescending(c => c.OnDate).ToListAsync();
            }
        }

        public override async Task<List<DailySale>> GetItemsAsync(string storeid)
        {
            var db = GetContext();
            return await db.DailySales.Where(c => c.StoreId == storeid && c.OnDate.Year == DateTime.Today.Year && c.OnDate.Month == DateTime.Today.Month)
                 .OrderByDescending(c => c.OnDate).ToListAsync();
        }

        public override List<int> GetYearList(string storeid)
        {
            var db = GetContext();
            return db.DailySales.Where(c => c.StoreId == storeid).Select(c => c.OnDate.Year).Distinct().ToList();
        }

        public override List<int> GetYearList()
        {
            var db = GetContext();
            return db.DailySales.Select(c => c.OnDate.Year).Distinct().ToList();
        }

        public override async Task<List<int>> GetYearListY(string storeid)
        {
            var db = GetContext();
            return await db.CustomerDues.Where(c => c.StoreId == storeid).Select(c => c.OnDate.Year).Distinct().ToListAsync();
        }

        public override async Task<List<int>> GetYearListY()
        {
            var db = GetContext();
            return await db.CustomerDues.Select(c => c.OnDate.Year).Distinct().ToListAsync();
        }

        public override async Task<List<int>> GetYearListZ(string storeid)
        {
            var db = GetContext();
            return await db.DueRecovery.Where(c => c.StoreId == storeid).Select(c => c.OnDate.Year).Distinct().ToListAsync();
        }

        public override async Task<List<int>> GetYearListZ()
        {
            var db = GetContext();
            return await db.DueRecovery.Select(c => c.OnDate.Year).Distinct().ToListAsync();
        }

        public override Task<List<CustomerDue>> GetYFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<CustomerDue>> GetYItems(string storeid)
        {
            var db = GetContext();
            return await db.CustomerDues.Where(c => c.StoreId == storeid && !c.Paid && c.OnDate.Year == DateTime.Today.Year)
                 .OrderByDescending(c => c.OnDate).ToListAsync();
        }

        public override Task<List<DueRecovery>> GetZFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<DueRecovery>> GetZItems(string storeid)
        {
            var db = GetContext();
            return await db.DueRecovery.Where(c => c.StoreId == storeid && c.OnDate.Year == DateTime.Today.Year)
                 .OrderByDescending(c => c.OnDate).ToListAsync();
        }

        public override async Task<bool> InitContext()
        {
            return Connect();
        }

        public async Task<bool> SyncCurrentMonth()
        {
            var remote = await _azureDb.DailySales.Where(c => c.StoreId == StoreCode && c.OnDate.Year == DateTime.Today.Year && c.OnDate.Month == DateTime.Today.Month).ToListAsync();
            var count = _localDb.DailySales.Where(c => c.StoreId == StoreCode && c.OnDate.Year == DateTime.Today.Year && c.OnDate.Month == DateTime.Today.Month).Count();
            if (count < remote.Count)
            {
                foreach (var item in remote)
                {
                    if (_localDb.DailySales.Any(c => c.InvoiceNumber == item.InvoiceNumber))
                    {
                        if (item.EntryStatus == EntryStatus.Updated || item.EntryStatus == EntryStatus.Approved)
                        {
                            _localDb.DailySales.Update(item);
                        }
                        else if (item.EntryStatus == EntryStatus.DeleteApproved || item.EntryStatus == EntryStatus.Deleted)
                        {
                            _localDb.Remove(item);
                        }
                    }
                    else
                        _localDb.AddAsync(item);
                }
                return (await _localDb.SaveChangesAsync() > 0);
            }
            return true;
        }
    }
}