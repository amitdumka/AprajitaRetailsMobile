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
    public class PurchaseViewModel : BaseViewModel<ProductPurchaseDTO, ProductPurchaseDataModel>
    {
        public PurchaseViewModel() { }
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
            Icon = Resources.Styles.IconFont.MoneyBillWave;
            DataModel = new ProductPurchaseDataModel();//DataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<ProductPurchaseDTO>();
            DataModel.Mode = DBType.API;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.Role;
            Title = " Purchase's";
            DataModel.Connect();
            DefaultSortedColName = nameof(ProductPurchase.OnDate);
            DefaultSortedOrder = Descending;
            FetchAsync();
        }

        protected override void RefreshButton()
        {
            Entities.Clear();
            FetchAsync();
        }
        private async void FetchAsync()
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
        protected override async Task<ColumnCollection> SetGridCols()
        {
            ColumnCollection gridColumns = new();
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductPurchase.InvoiceNo), MappingName = nameof(ProductPurchase.InvoiceNo) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductPurchase.OnDate), MappingName = nameof(ProductPurchase.OnDate), Format = "dd/MMM/yyyy" });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductPurchase.BasicAmount), MappingName = nameof(ProductPurchase.BasicAmount) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductPurchase.TaxAmount), MappingName = nameof(ProductPurchase.TaxAmount) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductPurchase.TotalAmount), MappingName = nameof(ProductPurchase.TotalAmount) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductPurchase.TotalQty), MappingName = nameof(ProductPurchase.TotalQty) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductPurchase.Paid), MappingName = nameof(ProductPurchase.Paid) });

            return gridColumns;
        }
    }
}

