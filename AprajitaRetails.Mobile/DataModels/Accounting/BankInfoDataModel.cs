////using AKS.Shared.Commons.Models.Banking;
using AprajitaRetails.Mobile.DataModels.Base;
using AprajitaRetails.Mobile.Operations.Prefernces;
using AprajitaRetails.Shared.Models.Banking;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Mobile.DataModels.Accounting
{

    public class VendorBankAccountDataModel : BaseDM<VendorBankAccount>
    {
        public VendorBankAccountDataModel() : base()
    {
        //$"Employees/bystoredto", $"?storeid={Setting.StoreCode}&isWorking=true")
        apiurl = "api/BankAccountList";
        apiDtoURL = $"api/BankAccountList/bystoredto?storeid={CurrentSession.StoreCode}";
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

    [Obsolete]
    public class BankInfoDataModel : BaseDataModel<BankAccountList, VendorBankAccount>
    {
        public BankInfoDataModel(ConType conType) : base(conType)
        {
        }

        public BankInfoDataModel(ConType conType, RolePermission role) : base(conType, role)
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

        public override List<BankAccountList> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<BankAccountList>> GetItemsAsync(string storeid)
        {
            var db = GetContext();
            return await db.AccountLists.Where(c => c.StoreId == storeid).ToListAsync();
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

        public override Task<List<VendorBankAccount>> GetYFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<VendorBankAccount>> GetYItems(string storeid)
        {
            var db = GetContext();
            return await db.VendorBankAccounts.Where(c => c.StoreId == storeid && c.IsActive)
                .ToListAsync();
        }

        public override async Task<bool> InitContext()
        {
            return Connect();
        }
    }
}

