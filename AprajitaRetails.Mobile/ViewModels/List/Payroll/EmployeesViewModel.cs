using AprajitaRetails.Mobile.DataModels.Payroll;
using AprajitaRetails.Mobile.Helpers;
using AprajitaRetails.Mobile.Operations.Prefernces;
using AprajitaRetails.Mobile.ViewModels.Base;
using AprajitaRetails.Shared.AutoMapper.DTO;
using AprajitaRetails.Shared.Models.Payroll;
using Syncfusion.Maui.DataGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprajitaRetails.Mobile.ViewModels.List.Payroll
{
    public class EmployeesViewModel : BaseViewModel<EmployeeDTO, EmployeeDataModel>
    {

        public EmployeesViewModel():base() { 
        
           
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
            DataModel = new EmployeeDataModel();
            Entities = new System.Collections.ObjectModel.ObservableCollection<EmployeeDTO>();
            DataModel.Mode = DBType.API;
            DataModel.StoreCode = CurrentSession.StoreCode;
            Role = CurrentSession.Role;
            Title = "Employee(s)";
            IsRefreshing = false;
            DataModel.Connect();
            DefaultSortedColName = nameof(EmployeeDTO.FirstName);
            DefaultSortedOrder = Descending;
            FetchAsync();
        }

        
        private void RefreshButton_Remove()
        {
            Entities.Clear();
            Notify.NotifyShort("Refresh Employees....");
            FetchAsync();
        }

        protected override async Task<ColumnCollection> SetGridCols()
        {
            ColumnCollection gridColumns = new();
            gridColumns.Add(new DataGridTextColumn() { HeaderText = "ID", MappingName = nameof(EmployeeDTO.EmployeeId) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = "Name", MappingName = nameof(EmployeeDTO.StaffName) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = "Dept", MappingName = nameof(EmployeeDTO.Category) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = "Working", MappingName = nameof(EmployeeDTO.IsWorking) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = "Store", MappingName = nameof(EmployeeDTO.StoreName) });
            gridColumns.Add(new DataGridTextColumn() { HeaderText = "Gender", MappingName = nameof(EmployeeDTO.Gender) });

            gridColumns.Add(new DataGridTextColumn() { HeaderText = "Since", MappingName = nameof(EmployeeDTO.JoiningDate), Format = "dd/MMM/yyyy" });
            return gridColumns;
        }

        #region Functions
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
        #endregion
    }
}
