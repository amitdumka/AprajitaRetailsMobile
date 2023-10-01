////using AKS.Shared.Commons.Models;
////using AKS.Shared.Commons.Models.Sales;
using AprajitaRetails.Mobile.DataModels.Base;
using AprajitaRetails.Mobile.Operations.Prefernces;
using AprajitaRetails.Shared.Models.Stores;

namespace AprajitaRetails.Mobile.DataModels.Accounting
{
    public class DueRecoveryDataModel : BaseDM<DueRecovery>
    {
        public DueRecoveryDataModel() : base()
        {
            //$"Employees/bystoredto", $"?storeid={Setting.StoreCode}&isWorking=true")
            apiurl = "api/DueRecoverys";
            apiDtoURL = $"api/DueRecovery/bystoredto?storeid={CurrentSession.StoreCode}";
        }

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override List<DueRecovery> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<DueRecovery>> GetItemsAsync(string storeid)
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