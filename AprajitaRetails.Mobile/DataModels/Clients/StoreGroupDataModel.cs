using AprajitaRetails.Mobile.DataModels.Base;
using AprajitaRetails.Shared.Models.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprajitaRetails.Mobile.DataModels.Clients
{
    public class StoreGroupDataModel : BaseDM<StoreGroup>
    {
        public StoreGroupDataModel() : base()
        {
            //$"Employees/bystoredto", $"?storeid={Setting.StoreCode}&isWorking=true")
            apiurl = "AppClient";
            apiDtoURL = $"AppClient";
        }

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override List<StoreGroup> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<StoreGroup>> GetItemsAsync(string storeid)
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
