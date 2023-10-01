//using AKS.Shared.Commons.Models.Inventory;
//using AKS.Shared.Commons.Ops;
//using AprajitaRetails.Mobile.MAUILib.DataModels.Inventory;
//using AprajitaRetails.Mobile.MAUILib.Helpers;
//using AprajitaRetails.Mobile.MAUILib.ViewModels.Base;
using AprajitaRetails.Mobile.DataModels.Inventory;
using AprajitaRetails.Shared.Models.Inventory;
using Syncfusion.Maui.DataGrid;

namespace AprajitaRetails.Mobile.ViewModels.List.Inventory
{
    public class StockViewModel : BaseViewModel<StockDTO, StockDataModel>
    {
        public StockViewModel() { }
        protected override void AddButton()
        {
            //TODO: Need to disable  and use for other purpose if desired
            throw new NotImplementedException();
        }

        protected override void DeleteButton()
        {
            //TODO: Need to disable  and use for other purpose if desired
            throw new NotImplementedException();
        }

        protected override void InitViewModel()
        {
            Icon = Resources.Styles.IconFont.MoneyBillWave;
            DataModel = new StockDataModel();//DataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<StockDTO>();
            DataModel.Mode = DBType.API;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.Role;
            Title = " Stock's";
            DataModel.Connect();
            DefaultSortedColName = nameof(ProductSale.OnDate);
            DefaultSortedOrder = Descending;
            FetchAsync();
        }

        private void RefreshButton_Remove()
        {
            throw new NotImplementedException();
        }

        protected override async Task<ColumnCollection> SetGridCols()
        {
            ColumnCollection gridColumns = new();
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Stock.Barcode), MappingName = nameof(Stock.Barcode) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Stock.CostPrice), MappingName = nameof(Stock.CostPrice) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Stock.MRP), MappingName = nameof(Stock.MRP) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Stock.CurrentQty), MappingName = nameof(Stock.CurrentQty) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Stock.CurrentQtyWH), MappingName = nameof(Stock.CurrentQtyWH) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Stock.StockValue), MappingName = nameof(Stock.StockValue) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Stock.StockValueWH), MappingName = nameof(Stock.StockValueWH) });

            return gridColumns;
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
    }

}

