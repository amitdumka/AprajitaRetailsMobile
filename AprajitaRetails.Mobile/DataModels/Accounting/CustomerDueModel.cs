////using AKS.Shared.Commons.Models;
////using AKS.Shared.Commons.Models.Sales;
using AprajitaRetails.Mobile.DataModels.Base;
using AprajitaRetails.Mobile.Operations.Prefernces;
using AprajitaRetails.Shared.Models.Stores;

namespace AprajitaRetails.Mobile.DataModels.Accounting
{
    public class CustomerDueDataModel : BaseDM<CustomerDue>
    {
        public CustomerDueDataModel() : base()
        {
            //$"Employees/bystoredto", $"?storeid={Setting.StoreCode}&isWorking=true")
            apiurl = "CustomerDues";
            apiDtoURL = $"CustomerDues/bystoredto?storeid={CurrentSession.StoreCode}";
        }

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override List<CustomerDue> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<CustomerDue>> GetItemsAsync(string storeid)
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