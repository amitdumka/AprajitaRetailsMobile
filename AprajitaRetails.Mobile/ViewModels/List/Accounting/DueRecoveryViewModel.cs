//using AKS.Shared.Commons.Models.Sales;
//using AKS.Shared.Commons.Ops;
//using AprajitaRetails.Mobile.MAUILib.DataModels.Accounting;
//using AprajitaRetails.Mobile.MAUILib.Helpers;
//using AprajitaRetails.Mobile.MAUILib.ViewModels.Base;
using AprajitaRetails.Mobile.DataModels.Accounting;
using AprajitaRetails.Mobile.Helpers;
using AprajitaRetails.Mobile.Operations.Prefernces;
using AprajitaRetails.Mobile.ViewModels.Base;
using AprajitaRetails.Shared.Models.Stores;
using Syncfusion.Maui.DataGrid;

namespace AprajitaRetails.Mobile.ViewModels.List.Accounting
{
    public class DueRecoveryViewModel : BaseViewModel<DueRecovery, DueRecoveryDataModel>
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
            Icon = Resources.Styles.IconFont.ChalkboardTeacher;
            DataModel = new DueRecoveryDataModel();//DataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<DueRecovery>();
            DataModel.Mode = DBType.API;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.Role;
            Title = "Dues Recovered";
            DataModel.Connect();
            DefaultSortedColName = nameof(DailySale.OnDate);
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
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DueRecovery.InvoiceNumber), MappingName = nameof(DueRecovery.InvoiceNumber) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DueRecovery.OnDate), MappingName = nameof(DueRecovery.OnDate), Format = "dd/MMM/yyyy" });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DueRecovery.Amount), MappingName = nameof(DueRecovery.Amount) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DueRecovery.Due), MappingName = nameof(DueRecovery.Due) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DueRecovery.PartialPayment), MappingName = nameof(DueRecovery.PartialPayment) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DueRecovery.PayMode), MappingName = nameof(DueRecovery.PayMode) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(DueRecovery.Remarks), MappingName = nameof(DueRecovery.Remarks) });
            return gridColumns;
        }
    }
}