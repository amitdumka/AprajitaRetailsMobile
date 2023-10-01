//using AKS.Shared.Commons.Models.Inventory;
//using AKS.Shared.Commons.Ops;
using AprajitaRetails.Mobile.DataModels.Inventory;
using AprajitaRetails.Shared.Models.Inventory;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
//using AprajitaRetails.Mobile.MAUILib.DataModels.Inventory;
//using AprajitaRetails.Mobile.MAUILib.Helpers;
//using AprajitaRetails.Mobile.MAUILib.ViewModels.Base;
using Syncfusion.Maui.DataGrid;

namespace AprajitaRetails.Mobile.ViewModels.List.Inventory
{
    public partial class SaleViewModel : BaseViewModel<ProductSaleDTO, ProductSaleDataModel>
    {
        public SaleViewModel() { }
        #region Fields

        [ObservableProperty]
        private bool _synced = false;

        [ObservableProperty]
        private InvoiceType _invoiceType = InvoiceType.Sales;

        #endregion Fields

        [ObservableProperty]
        private List<string> _invTypes;// = Enum.GetNames(typeof(InvoiceType)).ToList();

        partial void OnSyncedChanged(bool value)
        {
            if (value)
            {
                Entities.Clear();
                FetchAsync();
            }
        }

        partial void OnInvoiceTypeChanged(InvoiceType value)
        {
            Entities.Clear();
            FetchAsync();
        }

        [RelayCommand]
        protected async void Sync()
        {
            //Synced = await DataModel.SyncInvoices(_invoiceType);
        }

        protected override async void AddButton()
        {
            await Shell.Current.GoToAsync($"sale/Entry?vm={this}&invType={_invoiceType}");
        }

        protected override void DeleteButton()
        {
            throw new NotImplementedException();
        }

        protected override void InitViewModel()
        {
            InvTypes = Enum.GetNames(typeof(InvoiceType)).ToList();
            Icon = Resources.Styles.IconFont.FileInvoiceDollar;

            DataModel = new ProductSaleDataModel();//DataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<ProductSaleDTO>();
            DataModel.Mode = DBType.API;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.Role;
            Title = " Sale's";
            DataModel.Connect();
            DefaultSortedColName = nameof(ProductSale.OnDate);
            DefaultSortedOrder = Descending;
            FetchAsync();
        }

        protected override void RefreshButton()
        {
            Entities.Clear();
            FetchAsync();
        }

        protected override async Task<ColumnCollection> SetGridCols()
        {
            ColumnCollection gridColumns = new();
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductSale.InvoiceNo), MappingName = nameof(ProductSale.InvoiceNo) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductSale.OnDate), MappingName = nameof(ProductSale.OnDate), Format = "dd/MMM/yyyy" });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductSale.TotalBasicAmount), MappingName = nameof(ProductSale.TotalBasicAmount) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductSale.TotalTaxAmount), MappingName = nameof(ProductSale.TotalTaxAmount) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductSale.TotalPrice), MappingName = nameof(ProductSale.TotalPrice) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductSale.TotalQty), MappingName = nameof(ProductSale.TotalQty) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(ProductSale.Tailoring), MappingName = nameof(ProductSale.Tailoring) });

            return gridColumns;
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
                    var data = await DataModel.GetByStoreDTO(CurrentSession.StoreCode);//, _invoiceType,13);
                    UpdateEntities(data);
                    break;

                default:
                    Notify.NotifyVLong("You are not authorised to access!");
                    break;
            }
        }
    }
}