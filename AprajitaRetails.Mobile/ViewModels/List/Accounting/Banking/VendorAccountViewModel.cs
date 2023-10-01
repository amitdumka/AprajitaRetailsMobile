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
    public class VendorAccountViewModel : BaseViewModel<VendorBankAccount, VendorBankAccountDataModel>
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
            DataModel = new VendorBankAccountDataModel();// (ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<VendorBankAccount>();
            DataModel.Mode = DBType.API;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.Role;
            Title = "Vendor Bank Account";
            DataModel.Connect();
            DefaultSortedColName = nameof(VendorBankAccount.AccountHolderName);
            DefaultSortedOrder = Descending;
            FetchAsync();
        }
        protected async Task FetchAsync()
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

        protected override void RefreshButton()
        {
            throw new NotImplementedException();
        }


        protected override async Task<ColumnCollection> SetGridCols()
        {
            ColumnCollection gridColumns = new();
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(VendorBankAccount.AccountNumber), MappingName = nameof(VendorBankAccount.AccountNumber) });
            
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(VendorBankAccount.AccountHolderName), MappingName = nameof(VendorBankAccount.AccountHolderName) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(VendorBankAccount.BranchName), MappingName = nameof(VendorBankAccount.BranchName) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(VendorBankAccount.AccountType), MappingName = nameof(VendorBankAccount.AccountType) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(VendorBankAccount.OpeningBalance), MappingName = nameof(VendorBankAccount.OpeningBalance) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(VendorBankAccount.IsActive), MappingName = nameof(VendorBankAccount.IsActive) });
            return gridColumns;
        }
    }
}

