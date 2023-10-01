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
    public class BankTransactionViewModel : BaseViewModel<BankTransaction, BankTranscationDataModel>
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
            DataModel = new BankTranscationDataModel();//DataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<BankTransaction>();
            DataModel.Mode = DBType.API;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.Role;
            Title = "Transcations";
            DataModel.Connect();
            DefaultSortedColName = nameof(BankTransaction.OnDate);
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
                    var data = await DataModel.GetByStoreDTO (CurrentSession.StoreCode);
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
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankTransaction.OnDate), MappingName = nameof(BankTransaction.OnDate), Format = "dd/MMM/yyyy" });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankTransaction.AccountNumber), MappingName = nameof(BankTransaction.AccountNumber) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankTransaction.Amount), MappingName = nameof(BankTransaction.Amount) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankTransaction.Balance), MappingName = nameof(BankTransaction.Balance) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankTransaction.DebitCredit), MappingName = nameof(BankTransaction.DebitCredit) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankTransaction.Narration), MappingName = nameof(BankTransaction.Narration) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(BankTransaction.Verified), MappingName = nameof(BankTransaction.Verified) });
            return gridColumns;
        }
    }
}

