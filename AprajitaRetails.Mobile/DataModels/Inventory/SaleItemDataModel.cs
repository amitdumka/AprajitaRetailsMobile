////using AKS.Shared.Commons.Models;
////using AKS.Shared.Commons.Models.Inventory;
////using AKS.Shared.Commons.Ops;
using AprajitaRetails.Mobile.DataModels.Base;

namespace AprajitaRetails.Mobile.DataModels.Inventory
{
    public class SaleItemDataModel : BaseDM<SaleItemDTO>
    {
        public SaleItemDataModel() : base()
        {

            apiurl = "SaleItems";
            apiDtoURL = $"{apiurl}/bystoredto?storeid={CurrentSession.StoreCode}";
        }

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override List<SaleItemDTO> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<SaleItemDTO>> GetItemsAsync(string storeid)
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

