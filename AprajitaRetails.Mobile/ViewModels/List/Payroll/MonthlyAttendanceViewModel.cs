using AprajitaRetails.Mobile.DataModels.Payroll;
using AprajitaRetails.Mobile.Helpers;
using AprajitaRetails.Mobile.Operations.Prefernces;
using AprajitaRetails.Mobile.ViewModels.Base;
using AprajitaRetails.Shared.AutoMapper.DTO;
using Syncfusion.Maui.DataGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprajitaRetails.Mobile.ViewModels.List.Payroll
{
    public class MonthlyAttendanceViewModel : BaseViewModel<MonthlyAttendanceDTO, MonthlyAttendanceDataModel>
    {
        public MonthlyAttendanceViewModel()
        {

        }
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
            Icon = AprajitaRetails.Mobile.Resources.Styles.IconFont.UserCheck;
            DataModel = new MonthlyAttendanceDataModel();
            Entities = new System.Collections.ObjectModel.ObservableCollection<MonthlyAttendanceDTO>();
            DataModel.Mode = DBType.API;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.Role;
            Title = "MonthlyAttendance(s)";
            IsRefreshing = false;
            DataModel.Connect();
            DefaultSortedColName = nameof(MonthlyAttendanceDTO.StaffName);
            DefaultSortedOrder = Descending;
            FetchAsync();
        }


        protected override void RefreshButton()
        {
            Entities.Clear();
            Notify.NotifyShort("Refresh MonthlyAttendances....");
            FetchAsync();
        }

        protected override async Task<ColumnCollection> SetGridCols()
        {
            ColumnCollection gridColumns = new();
            gridColumns.Add(new DataGridTextColumn() { HeaderText = "ID", MappingName = nameof(MonthlyAttendanceDTO.MonthlyAttendanceId) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = "Name", MappingName = nameof(MonthlyAttendanceDTO.StaffName) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = "Working Day", MappingName = nameof(MonthlyAttendanceDTO.NoOfWorkingDays) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = "Present", MappingName = nameof(MonthlyAttendanceDTO.Present) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = "Absent", MappingName = nameof(MonthlyAttendanceDTO.Absent) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = "Billable", MappingName = nameof(MonthlyAttendanceDTO.BillableDays)});
            gridColumns.Add(new DataGridTextColumn() { HeaderText = "Remark", MappingName = nameof(MonthlyAttendanceDTO.Remarks) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = "Month", MappingName = nameof(MonthlyAttendanceDTO.OnDate), Format = "MMM/yyyy" });
            return gridColumns;
        }

        #region Functions
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
        #endregion
    }
}

