using System;
////using AKS.Shared.Commons.Models.Banking;
using AprajitaRetails.Mobile.DataModels.Base;
using AprajitaRetails.Mobile.Operations.Prefernces;
using AprajitaRetails.Shared.Models.Banking;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Mobile.DataModels.Accounting
{

    public class BankDataModel : BaseDM<Bank>
    {
        public BankDataModel() : base()
        {
            //$"Employees/bystoredto", $"?storeid={Setting.StoreCode}&isWorking=true")
            apiurl = "api/Banks";
            apiDtoURL = $"api/Banks";
        }

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override List<Bank> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Bank>> GetItemsAsync(string storeid)
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

    public class BankAccountDataModel : BaseDM<BankAccount>
    {
        public BankAccountDataModel() : base()
        {
            //$"Employees/bystoredto", $"?storeid={Setting.StoreCode}&isWorking=true")
            apiurl = "api/BankAccounts";
            apiDtoURL = $"api/BankAccounts/bystoredto?storeid={CurrentSession.StoreCode}";
        }

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override List<BankAccount> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<BankAccount>> GetItemsAsync(string storeid)
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

    public class BankTranscationDataModel : BaseDM<BankTransaction>
    {
        public BankTranscationDataModel() : base()
        {
            //$"Employees/bystoredto", $"?storeid={Setting.StoreCode}&isWorking=true")
            apiurl = "api/BankTrancations";
            apiDtoURL = $"api/BankTranscations";
        }

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override List<BankTransaction> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<BankTransaction>> GetItemsAsync(string storeid)
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
    public partial class BankingDataModel : BaseDataModel<Bank, BankAccount, BankTransaction>
    {
        public BankingDataModel(ConType conType) : base(conType)
        {
        }

        public BankingDataModel(ConType conType,RolePermission role) : base(conType, role)
        {
        }

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }
        public string GenerateBankId(string name)
        {
            string bankId = "";
            var letters = name.Trim().Split(' ');
            foreach (var letter in letters)
            {
                bankId += letter[0];
            }
            bankId+=GetContextAzure().Banks.Count().ToString();
            return bankId;
        }
        public override Task<string> GenrateYID()
        {
            throw new NotImplementedException();
        }

        public override Task<string> GenrateZID()
        {
            throw new NotImplementedException();
        }

        public override List<Bank> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<Bank>> GetItemsAsync(string storeid)
        {
            var db = GetContext();
            return await db.Banks
                .ToListAsync();
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

        public override Task<List<int>> GetYearListZ(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<int>> GetYearListZ()
        {
            throw new NotImplementedException();
        }

        public override Task<List<BankAccount>> GetYFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<BankAccount>> GetYItems(string storeid)
        {
            var db = GetContext();
            return await db.BankAccounts.Where(c => c.StoreId == storeid && c.IsActive)
                .ToListAsync();
        }

        public override Task<List<BankTransaction>> GetZFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<BankTransaction>> GetZItems(string storeid)
        {
            var db = GetContext();
            return await db.BankTranscations.Where(c => c.StoreId == storeid && c.OnDate.Year == DateTime.Today.Year)
                .OrderByDescending(c => c.OnDate)
                .ToListAsync();
        }

        public override async Task<bool> InitContext()
        {
            return Connect();
        }

        public void SyncUp(Bank bank, bool isNew=true, bool delete=false)
        {
            var db = GetContextAzure();
            if (delete)
                db.Banks.Remove(bank);
            else
            {
                if (isNew)
                    db.Banks.Add(bank);
                else
                    db.Banks.Update(bank);
            }
            var x = db.SaveChanges();
            if (x > 0)
            {
                Console.WriteLine("c");
            }
            else
                Console.WriteLine("e");
        }
        public void SyncUp(BankAccount bank, bool isNew = true, bool delete = false)
        {
            var db = GetContextAzure();
            if (delete)
                db.BankAccounts.Remove(bank);
            else
            {
                if (isNew)
                    db.BankAccounts.Add(bank);
                else
                    db.BankAccounts.Update(bank);
            }
            var x = db.SaveChanges();
            if (x > 0)
            {
                Console.WriteLine("c");
            }
            else
                Console.WriteLine("e");
        }
        public void SyncUp(BankTransaction bank, bool isNew = true, bool delete = false)
        {
            var db = GetContextAzure();
            if (delete)
                db.BankTranscations.Remove(bank);
            else
            {
                if (isNew)
                    db.BankTranscations.Add(bank);
                else
                    db.BankTranscations.Update(bank);
            }
            var x = db.SaveChanges();
            if (x > 0)
            {
                Console.WriteLine("c");
            }
            else
                Console.WriteLine("e");
        }
    }
}

