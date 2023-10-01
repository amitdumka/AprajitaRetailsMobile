using AprajitaRetails.Mobile.DataModels.Base;
using AprajitaRetails.Mobile.Operations.Prefernces;
using AprajitaRetails.Shared.AutoMapper.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprajitaRetails.Mobile.DataModels.Payroll
{
    public class MonthlyAttendanceDataModel : BaseDM<MonthlyAttendanceDTO>
    {
        public MonthlyAttendanceDataModel() : base()
        {
            //$"Employees/bystoredto", $"?storeid={Setting.StoreCode}&isWorking=true")
            apiurl = "MonthlyAttendances";
            apiDtoURL = $"MonthlyAttendances/bystoredto?storeid={CurrentSession.StoreCode}&isWorking=false";
        }

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override List<MonthlyAttendanceDTO> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<MonthlyAttendanceDTO>> GetItemsAsync(string storeid)
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
