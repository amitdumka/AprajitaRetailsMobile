//////using AKS.Shared.Commons.Models.Banking;
using AprajitaRetails.Mobile.DataModels.Base;
using AprajitaRetails.Mobile.Operations.Prefernces;
using AprajitaRetails.Shared.Models.Banking;

namespace AprajitaRetails.Mobile.DataModels.Accounting
{
    public class BankAccountListDataModel : BaseDM<BankAccountList>
    {
        public BankAccountListDataModel() : base()
        {
            //$"Employees/bystoredto", $"?storeid={Setting.StoreCode}&isWorking=true")
            apiurl = "api/BankAccountList";
            apiDtoURL = $"api/BankAccountList/bystoredto?storeid={CurrentSession.StoreCode}";
        }

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override List<BankAccountList> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<BankAccountList>> GetItemsAsync(string storeid)
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

