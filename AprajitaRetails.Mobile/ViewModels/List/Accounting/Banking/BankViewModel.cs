using System;
using AprajitaRetails.Mobile.DataModels.Accounting;
using AprajitaRetails.Shared.Models.Banking;
//using AKS.Shared.Commons.Models.Accounts;
//using AKS.Shared.Commons.Models.Banking;
//using AKS.Shared.Commons.Ops;
//using AprajitaRetails.Mobile.MAUILib.DataModels.Accounting;
//using AprajitaRetails.Mobile.MAUILib.Helpers;
//using AprajitaRetails.Mobile.MAUILib.ViewModels.Base;
using Syncfusion.Maui.DataGrid;

namespace AprajitaRetails.Mobile.ViewModels.List.Accounting.Banking
{
    public class BankViewModel : BaseViewModel<Bank, BankDataModel>
    {
        protected override async void AddButton()
        {
            await Shell.Current.GoToAsync($"banking/bank/Entry?vm={this}");
        }

        protected override void DeleteButton()
        {
            throw new NotImplementedException();
        }

        protected override void InitViewModel()
        {
            Icon = Resources.Styles.IconFont.MoneyCheck;
            DataModel = new BankDataModel();//DataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<Bank>();
            DataModel.Mode = DBType.API;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.Role;
            Title = "Banks";
            DataModel.Connect();
            DefaultSortedColName = nameof(Bank.Name);
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
                    var data = await DataModel.RefreshData();
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
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Bank.BankId), MappingName = nameof(Bank.BankId) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Bank.Name), MappingName = nameof(Bank.Name) });

            return gridColumns;
        }


    }
}

