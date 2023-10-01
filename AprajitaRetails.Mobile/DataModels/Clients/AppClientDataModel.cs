using AprajitaRetails.Mobile.DataModels.Base;
using AprajitaRetails.Mobile.Operations.Prefernces;
using AprajitaRetails.Shared.Models.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprajitaRetails.Mobile.DataModels.Clients
{
    public class AppClientDataModel : BaseDM<AppClient>
    {
        public AppClientDataModel() : base()
        {
            //$"Employees/bystoredto", $"?storeid={Setting.StoreCode}&isWorking=true")
            apiurl = "AppClient";
            apiDtoURL = $"AppClient";
        }

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override List<AppClient> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<AppClient>> GetItemsAsync(string storeid)
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
