//using AKS.Shared.Commons.Models.Inventory;
//using AKS.Shared.Commons.Ops;
//using AprajitaRetails.Mobile.MAUILib.DataModels.Inventory;
//using AprajitaRetails.Mobile.MAUILib.ViewModels.Base;
using AprajitaRetails.Mobile.DataModels.Inventory;
using AprajitaRetails.Shared.Models.Inventory;
using Syncfusion.Maui.DataGrid;

namespace AprajitaRetails.Mobile.ViewModels.List.Inventory
{
    public class ProductViewModel : BaseViewModel<ProductItemDTO, ProductItemDataModel>
    {
        public ProductViewModel() { }
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
            DataModel = new ProductItemDataModel();//DataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<ProductItemDTO>();
            DataModel.Mode = DBType.API;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.Role;
            Title = " Products";
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
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductItem.Barcode), MappingName = nameof(ProductItem.Barcode) });
            
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductItem.Name), MappingName = nameof(ProductItem.Name) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductItem.ProductCategory), MappingName = nameof(ProductItem.ProductCategory) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductItem.ProductSubCategory), MappingName = nameof(ProductItem.ProductSubCategory) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductItem.StyleCode), MappingName = nameof(ProductItem.StyleCode) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductItem.Description), MappingName = nameof(ProductItem.Description) });

            return gridColumns;
        }
        protected override async Task FetchAsync()
        {
              var data = await DataModel.GetByStoreDTO(CurrentSession.StoreCode);
            UpdateEntities(data);

        }
    }
}

