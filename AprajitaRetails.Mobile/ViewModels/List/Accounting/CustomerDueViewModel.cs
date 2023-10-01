//using AKS.Shared.Commons.Models.Sales;
//using AKS.Shared.Commons.Ops;
//using AprajitaRetails.Mobile.MAUILib.DataModels.Accounting;
//using AprajitaRetails.Mobile.MAUILib.Helpers;
//using AprajitaRetails.Mobile.MAUILib.ViewModels.Base;
using AprajitaRetails.Mobile.DataModels.Accounting;
using AprajitaRetails.Shared.Models.Stores;
using Syncfusion.Maui.DataGrid;

namespace AprajitaRetails.Mobile.ViewModels.List.Accounting
{
    public class CustomerDueViewModel : BaseViewModel<CustomerDue, CustomerDueDataModel>
    {
        public CustomerDueViewModel() { }
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
            DataModel = new CustomerDueDataModel();//DataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<CustomerDue>();
            DataModel.Mode = DBType.API;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.Role;
            Title = "Customer Dues";
            DataModel.Connect();
            DefaultSortedColName = nameof(DailySale.OnDate);
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
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CustomerDue.InvoiceNumber), MappingName = nameof(CustomerDue.InvoiceNumber) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CustomerDue.OnDate), MappingName = nameof(CustomerDue.OnDate), Format = "dd/MMM/yyyy" });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CustomerDue.Amount), MappingName = nameof(CustomerDue.Amount) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CustomerDue.ClearingDate), MappingName = nameof(CustomerDue.ClearingDate) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CustomerDue.Paid), MappingName = nameof(CustomerDue.Paid) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CustomerDue.StoreId), MappingName = nameof(CustomerDue.StoreId) });

            return gridColumns;
        }

        protected new void UpdateEntities(List<CustomerDue> values)
        {
            if (Entities == null) Entities = new System.Collections.ObjectModel.ObservableCollection<CustomerDue>();
            foreach (var item in values)
            {
                Entities.Add(item);
            }
            RecordCount = _entities.Count;
        }
    }
}