//////using AKS.Shared.Commons.Models.Banking;
using AprajitaRetails.Mobile.DataModels.Base;
using AprajitaRetails.Mobile.Operations.Prefernces;
using AprajitaRetails.Shared.Models.Banking;

namespace AprajitaRetails.Mobile.DataModels.Accounting
{
    public class VendorBankAccountDataModel : BaseDM<VendorBankAccount>
    {
        public VendorBankAccountDataModel() : base()
    {
        //$"Employees/bystoredto", $"?storeid={Setting.StoreCode}&isWorking=true")
        apiurl = "BankAccountList";
        apiDtoURL = $"BankAccountList/bystoredto?storeid={CurrentSession.StoreCode}";
    }

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override List<VendorBankAccount> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<VendorBankAccount>> GetItemsAsync(string storeid)
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

