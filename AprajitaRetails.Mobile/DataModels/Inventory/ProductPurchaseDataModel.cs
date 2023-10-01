////using AKS.Shared.Commons.Models.Inventory;
using AprajitaRetails.Mobile.DataModels.Base;

namespace AprajitaRetails.Mobile.DataModels.Inventory
{
    public class ProductPurchaseDataModel : BaseDM<ProductPurchaseDTO>
    {
        public ProductPurchaseDataModel() : base()
        {

            apiurl = "api/ProductPurchases";
            apiDtoURL = $"{apiurl}/bystoredto?storeid={CurrentSession.StoreCode}";
        }

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override List<ProductPurchaseDTO> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<ProductPurchaseDTO>> GetItemsAsync(string storeid)
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

