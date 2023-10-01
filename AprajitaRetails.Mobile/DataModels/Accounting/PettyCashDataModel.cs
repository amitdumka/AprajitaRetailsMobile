﻿using System;
//using AKS.Shared.Commons.Models;
//using AKS.Shared.Commons.Models.Accounts;
using AprajitaRetails.Mobile.DataModels.Base;
using AprajitaRetails.Mobile.Operations.Prefernces;
using AprajitaRetails.Shared.AutoMapper.DTO;
using AprajitaRetails.Shared.Models.Stores;
using AprajitaRetails.Shared.Models.Vouchers;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Mobile.DataModels.Accounting
{

    public class PettyCashDataModel : BaseDM<PettyCashSheet>
    {
        public PettyCashDataModel() : base()
        {
            //$"Employees/bystoredto", $"?storeid={Setting.StoreCode}&isWorking=true")
            apiurl = "api/PettyCasshSheet";
            apiDtoURL = $"api/PettyCashSheet/bystoredto?storeid={CurrentSession.StoreCode}";
        }

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override List<PettyCashSheet> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<PettyCashSheet>> GetItemsAsync(string storeid)
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

    public class CashDetailDataModel : BaseDM<CashDetailDTO>
    {
        public CashDetailDataModel() : base()
        {
            //$"Employees/bystoredto", $"?storeid={Setting.StoreCode}&isWorking=true")
            apiurl = "api/CashDetailsModels";
            apiDtoURL = $"{apiurl}/bystoredto?storeid={CurrentSession.StoreCode}";
        }

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override List<CashDetailDTO> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<CashDetailDTO>> GetItemsAsync(string storeid)
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

    [Obsolete]
    public class PettyCashDatasModel : BaseDataModel<PettyCashSheet, CashDetail>
    {
        public PettyCashDatasModel(ConType conType) : base(conType)
        {
        }
        public PettyCashDatasModel(ConType conType, RolePermission role):base(conType, role)
        {

        }

         

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override Task<string> GenrateYID()
        {
            throw new NotImplementedException();
        }

        public override List<PettyCashSheet> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

         

        public override Task<List<PettyCashSheet>> GetItemsAsync(string storeid)
        {
            var db=GetContext();
            return db.PettyCashSheets.Where(c => c.OnDate.Year == DateTime.Today.Year)
                .OrderByDescending(c => c.OnDate).ToListAsync();

        }

        public override List<int> GetYearList()
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<int>> GetYearListY(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<int>> GetYearListY()
        {
            throw new NotImplementedException();
        }

        public override Task<List<CashDetail>> GetYFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

         

        public override async Task<List<CashDetail>> GetYItems(string storeid)
        {
            var db = GetContext();
            return await db.CashDetails.Where(c => c.OnDate.Year == DateTime.Today.Year)
                .OrderByDescending(c => c.OnDate).ToListAsync();
        }

        public override async Task<bool> InitContext()
        {
            return Connect();
        }
    }
}

