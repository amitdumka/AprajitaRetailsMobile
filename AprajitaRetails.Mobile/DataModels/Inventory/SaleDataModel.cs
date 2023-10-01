////using AKS.Shared.Commons.Models;
////using AKS.Shared.Commons.Models.Inventory;
////using AKS.Shared.Commons.Ops;
using AprajitaRetails.Mobile.DataModels.Base;
using AprajitaRetails.Mobile.Operations.Prefernces;
using AprajitaRetails.Shared.Models.Inventory;
using AprajitaRetails.Shared.Models.Stores;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Mobile.DataModels.Inventory
{


    [Obsolete]
    public class SaleDataModel : BaseDataModel<ProductSale, SaleItem, SalePaymentDetail>
    {
        public SaleDataModel(ConType conType) : base(conType)
        {
        }

        public SaleDataModel(ConType conType,RolePermission role) : base(conType, role)
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

        public override List<ProductSale> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductSale>> GetItemsAsync(string storeid, InvoiceType type, int month = 0, int year = 0)
        {
            if (month == 13)
            {
                if (year == 0) year = DateTime.Today.Year;
                return GetContext().ProductSales.Where(c => c.StoreId == storeid
                            && c.InvoiceType == type && c.OnDate.Year == year).OrderByDescending(c => c.OnDate).ToListAsync();
            }
            else
            {
                if (month == 0) month = DateTime.Today.Month;

                if (year == 0) year = DateTime.Today.Year;

                return GetContext().ProductSales.Where(c => c.StoreId == storeid
                && c.InvoiceType == type && c.OnDate.Month == month && c.OnDate.Year == year).OrderByDescending(c => c.OnDate).ToListAsync();
            }
        }
        public override Task<List<ProductSale>> GetItemsAsync(string storeid)
        {
            return GetContext().ProductSales.Where(c => c.StoreId == storeid).OrderByDescending(c => c.OnDate).ToListAsync();
        }

        public override List<int> GetYearList(string storeid)
        {
            return GetContext().ProductSales.Where(c => c.StoreId == storeid).Select(c => c.OnDate.Year).Distinct().ToList();
        }

        public override List<int> GetYearList()
        {
            return GetContext().ProductSales.Select(c => c.OnDate.Year).Distinct().ToList();
        }

        public override Task<List<int>> GetYearListY(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<int>> GetYearListY()
        {
            throw new NotImplementedException();
        }

        public override Task<List<int>> GetYearListZ(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<int>> GetYearListZ()
        {
            throw new NotImplementedException();
        }

        public override Task<List<SaleItem>> GetYFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<SaleItem>> GetYItems(string storeId)
        {
            return GetContext().SaleItems.Include(c => c.ProductSale).Where(c => c.ProductSale.StoreId == storeId).OrderByDescending(c => c.ProductSale.OnDate).ToListAsync();
        }

        public override Task<List<SalePaymentDetail>> GetZFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<SalePaymentDetail>> GetZItems(string storeid)
        {
            return GetContext().SalePaymentDetails.Include(c => c.ProductSale).Where(c => c.ProductSale.StoreId == storeid).OrderByDescending(c => c.ProductSale.OnDate).ToListAsync();
        }

        public override async Task<bool> InitContext() => Connect();

        #region SaleItemHelpers
        public void GetBarcode(string barcode)
        {
            var stock = GetContext().Stocks.Include(c => c.Product).Where(c => c.StoreId == CurrentSession.StoreCode && c.Barcode == barcode)
                .Select(c => new { c.Barcode, c.CurrentQty, c.CurrentQtyWH, c.Unit, c.MRP, c.Product.Description, c.Product.HSNCode })
                .FirstAsync();
        }

        public IQueryable<CustomerInfo> CustomerWhere(System.Linq.Expressions.Expression<Func<Customer, bool>> predict)
        {
            return GetContext().Customers.Where(predict).Select(c => new CustomerInfo { MobileNo = c.MobileNo, CustomerName = c.CustomerName });
        }

        public void SyncStock() { }
        public async Task<bool> SyncInvoices(InvoiceType type, int year = 0)
        {
            if (year == 0) year = DateTime.Today.Year;

            var remote = GetContextAzure().ProductSales.Where(c => c.StoreId == StoreCode
            && c.InvoiceType == type && c.OnDate.Year == year).Count();
            var local = GetContextLocal().ProductSales.Where(c => c.StoreId == StoreCode
            && c.InvoiceType == type && c.OnDate.Year == year).Count();
            if (remote > local)
            {
                var saleItems = await GetContextAzure().SaleItems.Include(c => c.ProductSale)
                    .Where(c => c.ProductSale.StoreId == StoreCode
            && c.InvoiceType == type && c.ProductSale.OnDate.Year == year).ToListAsync();

                int recordadded = 0;
                foreach (var item in saleItems)
                {
                    if (GetContextLocal().ProductSales.Any(c => c.InvoiceNo == item.InvoiceNumber))
                    {

                    }
                    else
                    {
                        GetContextLocal().SaleItems.Add(item);
                        recordadded++;
                    }
                }
                var count = await GetContextLocal().SaveChangesAsync();
                if (count >= recordadded)
                {
                    return true;
                }
                return false;

            }
            else return true;
        }
        #endregion
    }

    public class CustomerInfo
    {
        public string MobileNo { get; set; }
        public string CustomerName { get; set; }

    }
}

