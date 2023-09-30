using System;
//using AKS.Shared.Commons.Models.Inventory;
using AprajitaRetails.Mobile.DataModels.Base;
using AprajitaRetails.Shared.Models.Inventory;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Mobile.DataModels.Inventory
{
    public class ProductDataModel : BaseDataModel<ProductItem, Stock>
    {
        public ProductDataModel(ConType conType) : base(conType)
        {
        }

        public ProductDataModel(ConType conType,RolePermission role) : base(conType, role)
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

        public override List<ProductItem> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<ProductItem>> GetItemsAsync(string storeid)
        {
            return GetContext().ProductItems.ToListAsync();
        }

        public override List<int> GetYearList(string storeid)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList()
        {
            throw new NotImplementedException();
        }

        public override Task<List<int>> GetYearListY(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<int>> GetYearListY()
        {
            throw new NotImplementedException();
        }

        public override Task<List<Stock>> GetYFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Stock>> GetYItems(string storeid)
        {
           return GetContext().Stocks.Include(c => c.Product).Where(c => c.StoreId == storeid).ToListAsync();
        }

        public override Task<bool> InitContext()
        {
            throw new NotImplementedException();
        }
    }
}

