////using AKS.Shared.Commons.Models.Inventory;
using AprajitaRetails.Mobile.DataModels.Base;

namespace AprajitaRetails.Mobile.DataModels.Inventory
{
    public class SupplierDataModel : BaseDM<SupplierDataModel>
    {
        public SupplierDataModel() : base()
        {

            apiurl = "Suppliers";
            apiDtoURL = $"{apiurl}/bystoredto?storeid={CurrentSession.StoreCode}";
        }

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override List<SupplierDataModel> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<SupplierDataModel>> GetItemsAsync(string storeid)
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

