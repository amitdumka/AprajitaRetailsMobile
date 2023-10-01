//using AKS.Shared.Commons.Models.Accounts;
//using AKS.Shared.Commons.Models.Banking;
//using AKS.Shared.Commons.Ops;
//using AprajitaRetails.Mobile.MAUILib.DataModels.Accounting;
//using AprajitaRetails.Mobile.MAUILib.Helpers;
//using AprajitaRetails.Mobile.MAUILib.ViewModels.Base;
using AprajitaRetails.Mobile.DataModels.Accounting;
using AprajitaRetails.Shared.Models.Banking;
using Syncfusion.Maui.DataGrid;

namespace AprajitaRetails.Mobile.ViewModels.List.Accounting.Banking
{
    public class BankAccountViewModel : BaseViewModel<BankAccount, BankAccountDataModel>
    {
        protected override void AddButton()
        {
            throw new NotImplementedException();
        }

        protected override void DeleteButton()
        {
            throw new NotImplementedException();
        }

        protected override void InitViewModel()
        {
            Icon = Resources.Styles.IconFont.MoneyCheck;
            DataModel = new BankAccountDataModel();//DataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<BankAccount>();
            DataModel.Mode = DBType.API;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.Role;
            Title = "Bank Account";
            DataModel.Connect();
            DefaultSortedColName = nameof(BankAccount.AccountType);
            DefaultSortedOrder = Descending;
            FetchAsync();
        }
        protected override async Task FetchAsync()
        {
            switch (Role)
            {
                case RolePermission.GeneralManager:
                case RolePermission.Owner:
                case RolePermission.StoreManager:
                case RolePermission.Accountant:
                case RolePermission.CA:
                case RolePermission.GroupManager:
                    var data = await DataModel.GetByStoreDTO(CurrentSession.StoreCode);
                    UpdateEntities(data);
                    break;

                default:
                    Notify.NotifyVLong("You are not authorised to access!");
                    break;
            }
        }

        private void RefreshButton_Remove()
        {
            throw new NotImplementedException();
        }


        protected override async Task<ColumnCollection> SetGridCols()
        {
            ColumnCollection gridColumns = new();
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankAccount.AccountNumber), MappingName = nameof(BankAccount.AccountNumber) });
            
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankAccount.AccountHolderName), MappingName = nameof(BankAccount.AccountHolderName) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankAccount.AccountType), MappingName = nameof(BankAccount.AccountType) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankAccount.BankId), MappingName = nameof(BankAccount.BankId) });
           // gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankAccount.Bank.Name), MappingName = nameof(BankAccount.Bank.Name) });
            
            return gridColumns;
        }
    }
}

