//using AKS.Shared.Commons.Models.Accounts;
//using AKS.Shared.Commons.Ops;
//using AprajitaRetails.Mobile.MAUILib.DataModels.Accounting;
//using AprajitaRetails.Mobile.MAUILib.Helpers;
//using AprajitaRetails.Mobile.MAUILib.ViewModels.Base;
using AprajitaRetails.Mobile.DataModels.Accounting;
using AprajitaRetails.Mobile.Helpers;
using AprajitaRetails.Mobile.Operations.Prefernces;
using AprajitaRetails.Mobile.ViewModels.Base;
using AprajitaRetails.Shared.Models.Vouchers;
using Syncfusion.Maui.DataGrid;

namespace AprajitaRetails.Mobile.ViewModels.List.Accounting
{
    public class PettyCashViewMoldel : BaseViewModel<PettyCashSheet, PettyCashDataModel>
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
            DataModel = new PettyCashDataModel();//DataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<PettyCashSheet>();
            DataModel.Mode = DBType.API;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.Role;
            Title = "Petty Cash Sheet";
            DataModel.Connect();
            DefaultSortedColName = nameof(Voucher.OnDate);
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
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.Id), MappingName = nameof(PettyCashSheet.Id) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.OnDate), MappingName = nameof(PettyCashSheet.OnDate), Format = "dd/MMM/yyyy" });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.OpeningBalance), MappingName = nameof(PettyCashSheet.OpeningBalance) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.DailySale), MappingName = nameof(PettyCashSheet.DailySale) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.ManualSale), MappingName = nameof(PettyCashSheet.ManualSale) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.ClosingBalance), MappingName = nameof(PettyCashSheet.ClosingBalance) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.ReceiptsTotal), MappingName = nameof(PettyCashSheet.ReceiptsTotal) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.BankWithdrawal), MappingName = nameof(PettyCashSheet.BankWithdrawal) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.PaymentTotal), MappingName = nameof(PettyCashSheet.PaymentTotal) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.BankDeposit), MappingName = nameof(PettyCashSheet.BankDeposit) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.CardSale), MappingName = nameof(PettyCashSheet.CardSale) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(PettyCashSheet.NonCashSale), MappingName = nameof(PettyCashSheet.NonCashSale) });
            return gridColumns;
        }
    }
}

