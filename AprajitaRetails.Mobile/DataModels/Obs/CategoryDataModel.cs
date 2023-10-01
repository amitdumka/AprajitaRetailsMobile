////using AKS.Shared.Commons.Models.Inventory;
using AprajitaRetails.Mobile.DataModels.Base;
using AprajitaRetails.Shared.Models.Inventory;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Mobile.DataModels.Inventory
{


    [Obsolete]
    public class CategoryDataModel : BaseDataModel<ProductSubCategory, ProductType, Supplier>
    {
        public CategoryDataModel(ConType conType) : base(conType)
        {

        }

        public CategoryDataModel(ConType conType,RolePermission role) : base(conType, role)
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

        public override List<ProductSubCategory> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<ProductSubCategory>> GetItemsAsync(string storeid)
        {
            return GetContext().ProductSubCategories.ToListAsync();
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

        public override Task<List<int>> GetYearListZ(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<int>> GetYearListZ()
        {
            throw new NotImplementedException();
        }

        public override Task<List<ProductType>> GetYFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<ProductType>> GetYItems(string storeid)
        {
            return GetContext().ProductTypes.ToListAsync();
        }

        public override Task<List<Supplier>> GetZFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Supplier>> GetZItems(string storeid)
        {
            return GetContext().Suppliers.ToListAsync();
        }

        public override async Task<bool> InitContext() => Connect();
    }
}

