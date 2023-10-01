////using AKS.Shared.Commons.Models;
////using AKS.Shared.Commons.Models.Inventory;
using AprajitaRetails.Mobile.DataModels.Base;
using AprajitaRetails.Shared.Models.Inventory;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Mobile.DataModels.Inventory
{
    [Obsolete]
    public class PurchaseDataModel : BaseDataModel<ProductPurchase, PurchaseItem, Stock>
    {
        public PurchaseDataModel(ConType conType) : base(conType)
        {
        }

        public PurchaseDataModel(ConType conType,RolePermission role) : base(conType, role)
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

        public override List<ProductPurchase> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<ProductPurchase>> GetItemsAsync(string storeid)
        {
            return GetContext().PurchaseProducts.Where(c => c.StoreId == storeid).OrderByDescending(c => c.OnDate).ToListAsync();
        }

        public override List<int> GetYearList(string storeid)
        {
            return GetContext().PurchaseProducts.Where(c => c.StoreId == storeid).Select(c => c.OnDate.Year).ToList();
        }

        public override List<int> GetYearList()
        {
            return GetContext().PurchaseProducts.Select(c => c.OnDate.Year).ToList();
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

        public override Task<List<PurchaseItem>> GetYFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<PurchaseItem>> GetYItems(string storeid)
        {
            return GetContext().PurchaseItems.Include(c=>c.PurchaseProduct).Where(c => c.PurchaseProduct.StoreId == storeid).OrderByDescending(c => c.PurchaseProduct.OnDate).ToListAsync();
        }

        public override Task<List<Stock>> GetZFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Stock>> GetZItems(string storeid)
        {
            return GetContext().Stocks.Where(c => c.StoreId == storeid && c.CurrentQtyWH >= 1).ToListAsync();
        }

        public override Task<bool> InitContext()
        {
            throw new NotImplementedException();
        }
    }
}

