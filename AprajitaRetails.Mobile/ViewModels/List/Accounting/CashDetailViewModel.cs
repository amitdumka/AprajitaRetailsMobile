//using AKS.Shared.Commons.Models;
//using AKS.Shared.Commons.Models.Accounts;
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
    public class CashDetailViewModel : BaseViewModel<CashDetailDTO, CashDetailDataModel>
    {
        public CashDetailViewModel() { }
        public override void AddButton()
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
            DataModel = new CashDetailDataModel();//DataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<CashDetailDTO>();
            DataModel.Mode = DBType.API;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.Role;
            Title = "Cash Details";
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
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CashDetail.CashDetailId), MappingName = nameof(CashDetail.CashDetailId) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CashDetail.OnDate), MappingName = nameof(CashDetail.OnDate), Format = "dd/MMM/yyyy" });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CashDetail.Count), MappingName = nameof(CashDetail.Count) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CashDetail.TotalAmount), MappingName = nameof(CashDetail.TotalAmount) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CashDetail.N2000), MappingName = nameof(CashDetail.N2000) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CashDetail.N500), MappingName = nameof(CashDetail.N500) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CashDetail.N200), MappingName = nameof(CashDetail.N200) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(CashDetail.N100), MappingName = nameof(CashDetail.N100) });

            return gridColumns;
        }

        
    }
}

