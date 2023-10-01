//using AKS.Shared.Commons.Models.Accounts;
//using AKS.Shared.Commons.Ops;
using AprajitaRetails.Mobile.DataModels.Accounting;
using AprajitaRetails.Mobile.Helpers;
using AprajitaRetails.Mobile.Operations.Prefernces;
using AprajitaRetails.Mobile.ViewModels.Base;
using AprajitaRetails.Shared.AutoMapper.DTO;
using AprajitaRetails.Shared.Models.Vouchers;
using CommunityToolkit.Mvvm.ComponentModel;
////using AprajitaRetails.Mobile.MAUILib.DataModels.Accounting;
////using AprajitaRetails.Mobile.MAUILib.Helpers;
////using AprajitaRetails.Mobile.MAUILib.ViewModels.Base;
////using AprajitaRetails.Mobile.MAUILib.Views.Custom;
using Syncfusion.Maui.DataGrid;

namespace AprajitaRetails.Mobile.ViewModels.List.Accounting
{
    public partial class VoucherViewModel : BaseViewModel<VoucherDTO, VoucherDataModel>
    {
        [ObservableProperty]
        private VoucherType _voucherType;
        //public static ColumnCollection gridColumns;

        
        protected override void InitViewModel()
        {
            Icon = Resources.Styles.IconFont.MoneyBillWave;
            DataModel = new VoucherDataModel();//DataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<VoucherDTO>();
            DataModel.Mode = DBType.API;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.Role;
            Title = "Vouchers";
            DataModel.Connect();
            DefaultSortedColName = nameof(Voucher.OnDate);
            DefaultSortedOrder = Descending;
            FetchAsync();

        }
        protected override async void AddButton()
        {
            await Shell.Current.GoToAsync($"voucher/Entry?vm={this}");
        }

        //protected override async Task<bool> Delete()
        //{
        //    var dl = DataModel.GetContextLocal()
        //        .Vouchers.Where(c => c.UserId.Contains("#TESTING")).ToList();

        //    DataModel.GetContextLocal().Vouchers.RemoveRange(dl);
        //    DataModel.GetContextAzure().Vouchers.RemoveRange(dl);
        //    try
        //    {
        //        bool local = await DataModel.GetContextLocal().SaveChangesAsync() > 0;
        //        bool azure = DataModel.GetContextAzure().SaveChanges() > 0;
        //        if (!azure)
        //        {
        //            Notify.NotifyVLong("Failed to remove on remote");
        //        }
        //        if (local)
        //        {
        //            Entities.Clear();
        //        }
        //        return local;
        //    }
        //    catch (Exception e)
        //    {
        //        Notify.NotifyLong($"Error: {e.Message} ");
        //        return false;
        //    }
        //}

        protected override void DeleteButton()
        {
            //var c = Delete();
            // Notify.NotifyLong("Deleted: " + c.Result);
        }

        //protected override Task<bool> Edit(Voucher value)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Task<List<Voucher>> Filter(string fitler)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Task<Voucher> Get(string id)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Task<Voucher> GetById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Task<List<Voucher>> GetList()
        //{
        //    throw new NotImplementedException();
        //}

        

        protected override void RefreshButton()
        {
            Entities.Clear();
            Notify.NotifyShort("Refresh Vouchers....");
            FetchAsync();
        }

        protected new void UpdateEntities(List<VoucherDTO> values)
        {
            if (Entities == null) Entities = new System.Collections.ObjectModel.ObservableCollection<VoucherDTO>();
            foreach (var item in values)
            {
                Entities.Add(item);
            }
            RecordCount = _entities.Count;
        }

        protected async Task FetchAsync()
        {
            switch (Role)
            {
                case RolePermission.GroupManager:
                case RolePermission.Owner:
                case RolePermission.StoreManager:
                case RolePermission.Accountant:
                case RolePermission.CA:
                case RolePermission.GeneralManager:
                      var data = await DataModel.GetByStoreDTO(CurrentSession.StoreCode);
                    UpdateEntities(data);
                    break;

                default:
                    Notify.NotifyVLong("You are not authorised to access!");
                    break;
            }
        }

        partial void OnVoucherTypeChanged(VoucherType value)
        {
            // Use filter here to change the view.
            throw new NotImplementedException();
        }
        protected override async Task<ColumnCollection> SetGridCols()
        { 
            ColumnCollection gridColumns= new();
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Voucher.VoucherNumber), MappingName = nameof(Voucher.VoucherNumber) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Voucher.OnDate), MappingName = nameof(Voucher.OnDate), Format = "dd/MMM/yyyy" });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Voucher.VoucherType), MappingName = nameof(Voucher.VoucherType) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Voucher.PartyName), MappingName = nameof(Voucher.PartyName) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Voucher.Particulars), MappingName = nameof(Voucher.Particulars) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Voucher.Remarks), MappingName = nameof(Voucher.Remarks) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Voucher.Amount), MappingName = nameof(Voucher.Amount) });
            return gridColumns;
        }
    }
}