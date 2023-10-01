////using AKS.Shared.Commons.Models;
////using AKS.Shared.Commons.Models.Inventory;
////using AKS.Shared.Commons.Ops;
using AprajitaRetails.Mobile.DataModels.Base;
using AprajitaRetails.Shared.Models.Inventory;

namespace AprajitaRetails.Mobile.DataModels.Inventory
{
    public class SalePaymentDetailDataModel : BaseDM<SalePaymentDetail>
    {
        public SalePaymentDetailDataModel() : base()
        {

            apiurl = "SalePaymentDetails";
            apiDtoURL = $"{apiurl}/bystoredto?storeid={CurrentSession.StoreCode}";
        }

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override List<SalePaymentDetail> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<SalePaymentDetail>> GetItemsAsync(string storeid)
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

