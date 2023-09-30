using AprajitaRetails.Mobile.DataModels.Base;
using AprajitaRetails.Mobile.Operations.Prefernces;
using AprajitaRetails.Shared.AutoMapper.DTO;
using AprajitaRetails.Shared.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AprajitaRetails.Mobile.DataModels.Payroll
{
    public class EmployeeDataModel : BaseDM<EmployeeDTO>
    {
        public EmployeeDataModel():base() {
            //$"Employees/bystoredto", $"?storeid={Setting.StoreCode}&isWorking=true")
            apiurl = "Emlpoyees";
            apiDtoURL= $"Employees/bystoredto?storeid={CurrentSession.StoreCode}&isWorking=false";
        }
        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override List<EmployeeDTO> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<EmployeeDTO>> GetItemsAsync(string storeid)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList(string storeid)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList()
        {
            throw new NotImplementedException();
        }

        public override Task<bool> InitContext()
        {
            throw new NotImplementedException();
        }
    }
}
