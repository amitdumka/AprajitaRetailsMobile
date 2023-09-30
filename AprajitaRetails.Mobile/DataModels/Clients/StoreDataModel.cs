using AprajitaRetails.Mobile.DataModels.Base;
using AprajitaRetails.Shared.Models.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprajitaRetails.Mobile.DataModels.Clients
{
    internal class StoreDataModel : BaseDM<Store>
    {
        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override List<Store> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Store>> GetItemsAsync(string storeid)
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
