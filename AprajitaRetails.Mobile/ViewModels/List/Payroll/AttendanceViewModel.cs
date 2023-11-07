 
using AprajitaRetails.Mobile.DataModels.Payroll;
using AprajitaRetails.Mobile.FormEntry.Views;
 

namespace AprajitaRetails.Mobile.ViewModels.List.Payroll
{
    public class AttendanceViewModel : BaseViewModel<AttendanceDTO, AttendanceDataModel>
    {
        public AttendanceViewModel() : base()
        {


        }
        public override void AddButton()
        {
            _ = CurrentPage.Navigation.PushAsync(new AttendanceEntryPage());

        }

        protected override void DeleteButton()
        {
            throw new NotImplementedException();
        }

        protected override void InitViewModel()
        {
            Icon = Resources.Styles.IconFont.UserCheck;
            DataModel = new AttendanceDataModel();// (ConType.Hybrid, CurrentSession.Role);
            Entities = new ObservableCollection<AttendanceDTO>();
            DataModel.Mode = DBType.API;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.Role;
            Title = "Attendance(s)";
            DataModel.Connect();
            DefaultSortedColName = nameof(Attendance.OnDate);
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
            Entities.Clear();
            Notify.NotifyShort("Refresh Attendances....");
            FetchAsync();
        }

        protected override async Task<ColumnCollection> SetGridCols()
        {
            ColumnCollection gridColumns = new();
            gridColumns.Add(new DataGridTextColumn() { HeaderText = "Name", MappingName = nameof(AttendanceDTO.StaffName) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = "Date", MappingName = nameof(AttendanceDTO.OnDate), Format = "dd/MMM/yyyy" });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Attendance.Status), MappingName = nameof(AttendanceDTO.Status) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = "Time", MappingName = nameof(AttendanceDTO.EntryTime) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Attendance.Remarks), MappingName = nameof(AttendanceDTO.Remarks) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = "Store", MappingName = nameof(AttendanceDTO.StoreName) });

            return gridColumns;
        }
    }
    
}