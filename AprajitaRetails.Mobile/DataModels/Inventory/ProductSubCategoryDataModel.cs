////using AKS.Shared.Commons.Models.Inventory;
using AprajitaRetails.Mobile.DataModels.Base;
using AprajitaRetails.Shared.Models.Inventory;

namespace AprajitaRetails.Mobile.DataModels.Inventory
{
    public class ProductSubCategoryDataModel : BaseDM<ProductSubCategory>
    {
        public ProductSubCategoryDataModel() : base()
        {

            apiurl = "ProductSubCategorys";
            apiDtoURL = $"{apiurl}/bystoredto?storeid={CurrentSession.StoreCode}";
        }

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override List<ProductSubCategory> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<ProductSubCategory>> GetItemsAsync(string storeid)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList(string storeid)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList()
        {
            throw new NotImplementedException();
        }

        public override Task<bool> InitContext()
        {
            throw new NotImplementedException();
        }
    }
}

