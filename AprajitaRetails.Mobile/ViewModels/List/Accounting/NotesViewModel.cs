//using AKS.Shared.Commons.Models.Accounts;
//using AKS.Shared.Commons.Ops;
using AprajitaRetails.Mobile.DataModels.Accounting;
using AprajitaRetails.Mobile.Helpers;
using AprajitaRetails.Mobile.Operations.Prefernces;
using AprajitaRetails.Mobile.ViewModels.Base;
using AprajitaRetails.Shared.AutoMapper.DTO;
using CommunityToolkit.Mvvm.ComponentModel;
//using AprajitaRetails.Mobile.MAUILib.DataModels.Accounting;
//using AprajitaRetails.Mobile.MAUILib.Helpers;
//using AprajitaRetails.Mobile.MAUILib.ViewModels.Base;
using Syncfusion.Maui.DataGrid;

namespace AprajitaRetails.Mobile.ViewModels.List.Accounting
{
    public partial class NoteDTOsViewModel : BaseViewModel<NoteDTO, NoteDataModel>
    {
        [ObservableProperty]
        private VoucherType _voucherType;

        

        protected override void AddButton()
        {
            //  var c = Delete();
            Notify.NotifyLong("Delete: ");
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

        protected override void InitViewModel()
        {
            Icon = Resources.Styles.IconFont.MoneyCheck;
            DataModel = new NoteDataModel();//DataModel(ConType.Hybrid, CurrentSession.Role);
            Entities = new System.Collections.ObjectModel.ObservableCollection<NoteDTO>();
            DataModel.Mode = DBType.API;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.Role;
            Title = "Vouchers";
            DataModel.Connect();
            DefaultSortedColName = nameof(NoteDTO.OnDate);
            DefaultSortedOrder = Descending;
            FetchAsync();
             
        }

        protected override void RefreshButton()
        {
            // throw new NotImplementedException();
        }

        protected new void UpdateEntities(List<NoteDTO> values)
        {
            if (Entities == null) Entities = new System.Collections.ObjectModel.ObservableCollection<NoteDTO>();
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
        
        partial void OnVoucherTypeChanged(VoucherType value)
        {
            // Use filter here to change the view.
            throw new NotImplementedException();
        }

        protected override async Task<ColumnCollection> SetGridCols()
        {
            ColumnCollection gridColumns = new();
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(NoteDTO.NoteNumber), MappingName = nameof(NoteDTO.NoteNumber) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(NoteDTO.OnDate), MappingName = nameof(NoteDTO.OnDate), Format = "dd/MMM/yyyy" });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(NoteDTO.NotesType), MappingName = nameof(NoteDTO.NotesType) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(NoteDTO.PartyName), MappingName = nameof(NoteDTO.PartyName) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(NoteDTO.Reason), MappingName = nameof(NoteDTO.Reason) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(NoteDTO.Remarks), MappingName = nameof(NoteDTO.Remarks) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(NoteDTO.Amount), MappingName = nameof(NoteDTO.Amount) });
            return gridColumns;
        }
    }
}