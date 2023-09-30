

using AKS.MAUI.Databases;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Mobile.DataModels.Base
{
    /// <summary>
    /// Base Data Model Version:2
    /// With Role access restricted
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseDataModel<T> where T : class
    {
        #region Fields

        public string StoreCode;
        protected RolePermission Role;
        protected string Permissions;

        public DBType Mode { get; set; } = DBType.API;
        protected ConType ConType { get; set; } = ConType.Remote;

        protected List<T> Entity { get; set; }

        protected bool IsError { get; set; }
        protected string ErrorMsg { get; set; }

        public string GetError()
        {
            if (IsError) return ErrorMsg;
            else return "OK";
        }

        // Currently local and azure sql db
        protected AppDBContext _localDb, _azureDb;

        public AppDBContext GetContextLocal() => _localDb;

        public AppDBContext GetContextAzure() => _azureDb;

        #endregion Fields

        #region Constructor

        public BaseDataModel(ConType conType)
        {
            ConType = conType;
            Role = RolePermission.Salesmen;
            Permissions = "R";
        }

        public BaseDataModel(ConType conType, RolePermission role)
        {
            ConType = conType;
            Role = role;
            //Permissions = AuthHelper.GetPermission(role);
        }

        #endregion Constructor

        #region Mthods

        public bool Connect()
        {
            switch (ConType)
            {
                case ConType.Local:

                    _localDb = new AKS.MAUI.Databases.AppDBContext(DBType.Local);
                    return (_localDb != null);

                case ConType.Remote:
                    break;

                case ConType.RemoteDb:
                    _azureDb = new AKS.MAUI.Databases.AppDBContext(DBType.Azure);
                    return (_azureDb != null);

                case ConType.HybridApi:
                    break;

                case ConType.HybridDB:
                    _azureDb = new AKS.MAUI.Databases.AppDBContext(DBType.Azure);
                    _localDb = new AKS.MAUI.Databases.AppDBContext(DBType.Local);
                    return (_azureDb != null && _localDb != null);

                case ConType.Hybrid:
                    _azureDb = new AKS.MAUI.Databases.AppDBContext(DBType.Azure);
                    _localDb = new AKS.MAUI.Databases.AppDBContext(DBType.Local);
                    return (_azureDb != null && _localDb != null);

                default:
                    _localDb = new AKS.MAUI.Databases.AppDBContext(DBType.Local);
                    return (_localDb != null);
            }
            return false;
        }

        public abstract Task<bool> InitContext();

        public abstract Task<string> GenrateID();

        public AppDBContext GetContext()
        {
            AppDBContext db;
            switch (Mode)
            {
                case DBType.Local: db = _localDb; break;
                case DBType.Azure: db = _azureDb; break;
                default:
                    db = _localDb;
                    break;
            }
            return db;
        }

        public int Count()
        {
            AppDBContext db;
            switch (Mode)
            {
                case DBType.Local:
                    db = _localDb;
                    break;

                case DBType.Azure:
                    db = _azureDb;
                    break;

                default:
                    db = _localDb; break;
            }

            return db.Set<T>().Count();
        }

        #endregion Mthods

        //Get By ID
        public async Task<T> GetAsync(string id)
        {
            //if (Permissions.Contains("R"))
            //{
            switch (Mode)
            {
                case DBType.Local:
                    return await _localDb.FindAsync<T>(id);

                case DBType.Azure:
                    return await _azureDb.FindAsync<T>(id);


                case DBType.API:


                default:
                    return null;
            }
            //}
            //else
            //{
            //  //IsError = true;
            // //ErrorMsg = "Not Authozised to access!";
            // return null;
            //}
        }

        public async Task<T> GetAsync(int id)
        {
            //if (Permissions.Contains("R"))
            //{
                switch (Mode)
                {
                    case DBType.Local:
                        return await _localDb.FindAsync<T>(id);

                    case DBType.Azure:
                        return await _azureDb.FindAsync<T>(id);

                    default:
                        return null;
                }
            //}
            //else
            //{
            //    //IsError = true;
            //    //ErrorMsg = "Not Authozised to access!";
            //    return null;
            //}
        }

        //Get Items
        public abstract Task<List<T>> GetItemsAsync(string storeid);

        public abstract List<T> GetFiltered(QueryParam query);

        public async Task<List<T>> GetItemsAsync()
        {
            //if (Permissions.Contains("R"))
            //{
                switch (Mode)
                {
                    case DBType.Local:
                        return await _localDb.Set<T>().ToListAsync();

                    case DBType.Azure:
                        return await _azureDb.Set<T>().ToListAsync();

                    default:
                        return await _localDb.Set<T>().ToListAsync();
                }
            //}
            //else
            //{
            //    //IsError = true;
            //    //ErrorMsg = "Not Authozised to access!";
            //    return null;
            //}
        }

        #region Where

        public IQueryable<T> Where<TKey>(System.Linq.Expressions.Expression<Func<T, bool>> predict, Order? orderby,
            System.Linq.Expressions.Expression<Func<T, TKey>> order)
        {
            if (order != null && orderby != null)
                switch (orderby)
                {
                    case Order.Asc:
                        return GetContext().Set<T>().Where(predict).OrderBy(order);

                    case Order.Desc:
                        return GetContext().Set<T>().Where(predict).OrderByDescending(order);

                    default:
                        return GetContext().Set<T>().Where(predict).OrderBy(order);
                }
            return GetContext().Set<T>().Where(predict);
        }

        #endregion Where

        //Save
        public async Task<T> SaveAsync(T value, bool isNew = true)
        {
            //if (Permissions.Contains("W"))
            //{
                AppDBContext db;

                switch (Mode)
                {
                    case DBType.Local:
                        db = _localDb;
                        break;

                    case DBType.Azure:
                        db = _azureDb;
                        break;

                    default:
                        return null;
                }
                if (isNew)
                    await db.AddAsync<T>(value);
                else
                    db.Update<T>(value);

                if (await db.SaveChangesAsync() > 0) 
                return value;
                
            //}
            ////IsError = true;
            ////ErrorMsg = "Access Denied";
            return null;
        }

        public async Task<List<T>> SaveAllAsync(List<T> values, bool isNew = true)
        {
            //if (Permissions.Contains("W"))
            {
                AppDBContext db;
                switch (Mode)
                {
                    case DBType.Local:
                        db = _localDb;
                        break;

                    case DBType.Azure:
                        db = _azureDb;
                        break;

                    default:
                        return null;
                }
                if (isNew)
                    await db.AddRangeAsync(values);
                else
                    db.UpdateRange(values);
                if (await db.SaveChangesAsync() > 0) return values;
            }
            ////IsError = true;
            ////ErrorMsg = "Access Denied";
            return null;
        }

        //Delete
        public async Task<bool> DeleteAsync(string id)
        {
            //if (Permissions.Contains("D"))
            {
                switch (Mode)
                {
                    case DBType.Local:
                        var element = await _localDb.FindAsync<T>(id);
                        _localDb.Remove<T>(element);
                        return (await _localDb.SaveChangesAsync()) > 0;

                    case DBType.Azure:
                        var azureEle = await _azureDb.FindAsync<T>(id);
                        _azureDb.Remove<T>(azureEle);
                        return (await _azureDb.SaveChangesAsync()) > 0;

                    default: return false;
                }
            }
            ////IsError = true;
            ////ErrorMsg = "Access Denied";
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            //if (Permissions.Contains("D"))
            {
                switch (Mode)
                {
                    case DBType.Local:
                        var element = await _localDb.FindAsync<T>(id);
                        _localDb.Remove<T>(element);
                        return (await _localDb.SaveChangesAsync()) > 0;

                    case DBType.Azure:
                        var azureEle = await _azureDb.FindAsync<T>(id);
                        _azureDb.Remove<T>(azureEle);
                        return (await _azureDb.SaveChangesAsync()) > 0;

                    default:
                        return false;
                }
            }
            ////IsError = true;
            ////ErrorMsg = "Access Denied";
            return false;
        }

        public async Task<bool> DeleteAsync(T value)
        {
            //if (Permissions.Contains("D"))
            {
                switch (Mode)
                {
                    case DBType.Local:

                        _localDb.Remove<T>(value);
                        return (await _localDb.SaveChangesAsync()) > 0;

                    case DBType.Azure:

                        _azureDb.Remove<T>(value);
                        return (await _azureDb.SaveChangesAsync()) > 0;

                    default:
                        return false;
                }
            }

            ////IsError = true;
            ////ErrorMsg = "Access Denied";
            return false;
        }

        public async Task<bool> DeleteAsync(List<T> values)
        {
            //if (Permissions.Contains("D"))
            {
                switch (Mode)
                {
                    case DBType.Local:

                        _localDb.RemoveRange(values);
                        return (await _localDb.SaveChangesAsync()) == values.Count;

                    case DBType.Azure:

                        _azureDb.RemoveRange(values);
                        return (await _azureDb.SaveChangesAsync()) == values.Count;

                    default:
                        return false;
                }
            }
            ////IsError = true;
            ////ErrorMsg = "Access Denied";
            return false;
        }

        //YearList
        public abstract List<int> GetYearList(string storeid);

        public abstract List<int> GetYearList();

        //Exsits
        public async Task<bool> IsExists(string id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    if (await _localDb.FindAsync<T>(id) != null) return true; else return false;

                case DBType.Azure:
                    if (await _azureDb.FindAsync<T>(id) != null) return true; else return false;

                case DBType.API:
                    return false;

                default:
                    return false;
            }
        }

        public async Task<bool> IsExists(int id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    if (await _localDb.FindAsync<T>(id) != null) return true; else return false;
                case DBType.Azure:
                    if (await _azureDb.FindAsync<T>(id) != null) return true; else return false;

                case DBType.API:
                    return false;

                default:
                    return false;
            }
        }
    }

    public abstract class BaseDataModel<T, Y> : BaseDataModel<T> where T : class where Y : class
    {
        public BaseDataModel(ConType conType) : base(conType)
        {
        }

        public BaseDataModel(ConType conType, RolePermission role) : base(conType, role)
        {
        }

        public abstract Task<string> GenrateYID();

        public int CountY()
        {
            AppDBContext db;
            switch (Mode)
            {
                case DBType.Local:
                    db = _localDb;
                    break;

                case DBType.Azure:
                    db = _azureDb;
                    break;

                default:
                    db = _localDb; break;
            }

            return db.Set<Y>().Count();
        }

        //Get By ID
        public async Task<Y> GetYAsync(string id)
        {
           //// if (Permissions.Contains("R"))
            {
                switch (Mode)
                {
                    case DBType.Local:
                        return await _localDb.FindAsync<Y>(id);

                    case DBType.Azure:
                        return await _azureDb.FindAsync<Y>(id);

                    default:
                        return null;
                }
            }
           // //IsError = true;
           // //ErrorMsg = "Access Denied";
            return null;
        }

        public async Task<Y> GetYAsync(int id)
        {
            //if (Permissions.Contains("R"))
            {
                switch (Mode)
                {
                    case DBType.Local:
                        return await _localDb.FindAsync<Y>(id);

                    case DBType.Azure:
                        return await _azureDb.FindAsync<Y>(id);

                    default:
                        return null;
                }
            }
           // //IsError = true;
           // //ErrorMsg = "Access Denied";
            return null;
        }

        //Get Items
        public abstract Task<List<Y>> GetYItems(string storeid);

        public abstract Task<List<Y>> GetYFiltered(QueryParam query);

        public async Task<List<Y>> GetYItemsAsync()
        {
            //if (Permissions.Contains("R"))
            {
                switch (Mode)
                {
                    case DBType.Local:
                        return await _localDb.Set<Y>().ToListAsync();

                    case DBType.Azure:
                        return await _azureDb.Set<Y>().ToListAsync();

                    default:
                        return await _localDb.Set<Y>().ToListAsync();
                }
            }
            ////IsError = true;
           // //ErrorMsg = "Access Denied";
            return null;
        }

        #region Where

        public IQueryable<Y> Where(System.Linq.Expressions.Expression<Func<Y, bool>> predict)
        {
            return GetContext().Set<Y>().Where(predict);
        }

        #endregion Where

        //Save
        public async Task<Y> SaveAsync(Y value, bool isNew = true)
        {
           //// if (Permissions.Contains("W"))
            {
                AppDBContext db;
                switch (Mode)
                {
                    case DBType.Local:
                        db = _localDb;
                        break;

                    case DBType.Azure:
                        db = _azureDb;
                        break;

                    default:
                        return null;
                }
                if (isNew)
                    await db.AddAsync<Y>(value);
                else
                    db.Update<Y>(value);
                if (await db.SaveChangesAsync() > 0) return value;
            }
            ////IsError = true;
           // //ErrorMsg = "Access Denied";
            return null;
        }

        public async Task<List<Y>> SaveAllAsync(List<Y> values, bool isNew = true)
        {
           //// if (Permissions.Contains("W"))
            {
                AppDBContext db;
                switch (Mode)
                {
                    case DBType.Local:
                        db = _localDb;
                        break;

                    case DBType.Azure:
                        db = _azureDb;
                        break;

                    default:
                        return null;
                }
                if (isNew)
                    await db.AddRangeAsync(values);
                else
                    db.UpdateRange(values);
                if (await db.SaveChangesAsync() > 0) return values;
            }
            ////IsError = true;
            ////ErrorMsg = "Access Denied";
            return null;
        }

        //Delete
        public async Task<bool> DeleteYAsync(string id)
        {
           //// if (Permissions.Contains("D"))
            {
                switch (Mode)
                {
                    case DBType.Local:
                        var element = await _localDb.FindAsync<Y>(id);
                        _localDb.Remove<Y>(element);
                        return (await _localDb.SaveChangesAsync()) > 0;

                    case DBType.Azure:
                        var azureEle = await _azureDb.FindAsync<Y>(id);
                        _azureDb.Remove<Y>(azureEle);
                        return (await _azureDb.SaveChangesAsync()) > 0;

                    default:
                        return false;
                }
            }
            ////IsError = true;
           ////ErrorMsg = "Access Denied";
            return false;
        }

        public async Task<bool> DeleteY(int id)
        {
           //// if (Permissions.Contains("D"))
            {
                switch (Mode)
                {
                    case DBType.Local:
                        var element = await _localDb.FindAsync<Y>(id);
                        _localDb.Remove<Y>(element);
                        return (await _localDb.SaveChangesAsync()) > 0;

                    case DBType.Azure:
                        var azureEle = await _azureDb.FindAsync<Y>(id);
                        _azureDb.Remove<Y>(azureEle);
                        return (await _azureDb.SaveChangesAsync()) > 0;

                    default:
                        return false;
                }
            }
           // //IsError = true;
           // //ErrorMsg = "Access Denied";
            return false;
        }

        public async Task<bool> DeleteAsync(Y value)
        {
           //// if (Permissions.Contains("D"))
            {
                switch (Mode)
                {
                    case DBType.Local:

                        _localDb.Remove<Y>(value);
                        return (await _localDb.SaveChangesAsync()) > 0;

                    case DBType.Azure:

                        _azureDb.Remove<Y>(value);
                        return (await _azureDb.SaveChangesAsync()) > 0;

                    default:
                        return false;
                }
            }
            ////IsError = true;
           // //ErrorMsg = "Access Denied";
            return false;
        }

        public async Task<bool> DeleteAsync(List<Y> values)
        {
            //if (Permissions.Contains("D"))
            {
                switch (Mode)
                {
                    case DBType.Local:

                        _localDb.RemoveRange(values);
                        return (await _localDb.SaveChangesAsync()) == values.Count;

                    case DBType.Azure:

                        _azureDb.RemoveRange(values);
                        return (await _azureDb.SaveChangesAsync()) == values.Count;

                    default:
                        return false;
                }
            }
            ////IsError = true;
            ////ErrorMsg = "Access Denied";
            return false;
        }

        //YearList
        public abstract Task<List<int>> GetYearListY(string storeid);

        public abstract Task<List<int>> GetYearListY();

        //Exsits
        public async Task<bool> IsYExists(string id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    if (await _localDb.FindAsync<T>(id) != null) return true; else return false;
                    break;

                case DBType.Azure:
                    if (await _azureDb.FindAsync<T>(id) != null) return true; else return false;
                    break;

                case DBType.API:
                    return false;
                    break;

                default:
                    return false;
                    break;
            }
        }

        public async Task<bool> IsYExists(int id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    if (await _localDb.FindAsync<T>(id) != null) return true; else return false;
                    break;

                case DBType.Azure:
                    if (await _azureDb.FindAsync<T>(id) != null) return true; else return false;
                    break;

                case DBType.API:
                    return false;
                    break;

                default:
                    return false;
                    break;
            }
        }
    }

    public abstract class BaseDataModel<T, Y, Z> : BaseDataModel<T, Y> where T : class where Y : class where Z : class
    {
        public BaseDataModel(ConType conType) : base(conType)
        {
        }

        public BaseDataModel(ConType conType, RolePermission role) : base(conType, role)
        {
        }

        public abstract Task<string> GenrateZID();

        public int CountZ()
        {
            AppDBContext db;
            switch (Mode)
            {
                case DBType.Local:
                    db = _localDb;
                    break;

                case DBType.Azure:
                    db = _azureDb;
                    break;

                default:
                    db = _localDb; break;
            }

            return db.Set<Z>().Count();
        }

        #region Get

        //Get By ID
        public async Task<Z> GetZAsync(string id)
        {
           // if (Permissions.Contains("R"))
            {
                switch (Mode)
                {
                    case DBType.Local:
                        return await _localDb.FindAsync<Z>(id);

                    case DBType.Azure:
                        return await _azureDb.FindAsync<Z>(id);

                    default:
                        return null;
                }
            }
            //IsError = true;
            //ErrorMsg = "Access Denied";
            return null;
        }

        public async Task<Z> GetZAsync(int id)
        {
           // if (Permissions.Contains('R'))
                switch (Mode)
                {
                    case DBType.Local:
                        return await _localDb.FindAsync<Z>(id);

                    case DBType.Azure:
                        return await _azureDb.FindAsync<Z>(id);

                    default:
                        return null;
                }
            //IsError = true;
            //ErrorMsg = "Access Denied";
            return null;
        }

        //Get Items
        public abstract Task<List<Z>> GetZItems(string storeid);

        public abstract Task<List<Z>> GetZFiltered(QueryParam query);

        public async Task<List<Z>> GetZItemsAsync()
        {
           // if (Permissions.Contains('R'))
                switch (Mode)
                {
                    case DBType.Local:
                        return await _localDb.Set<Z>().ToListAsync();

                    case DBType.Azure:
                        return await _azureDb.Set<Z>().ToListAsync();

                    default:
                        return await _localDb.Set<Z>().ToListAsync();
                }
            //IsError = true;
            //ErrorMsg = "Access Denied";
            return null;
        }

        #endregion Get

        #region Where

        public IQueryable<Z> Where(System.Linq.Expressions.Expression<Func<Z, bool>> predict)
        {
            return GetContext().Set<Z>().Where(predict);
        }

        #endregion Where

        #region Save

        //Save
        public async Task<Z> SaveAsync(Z value, bool isNew = true)
        {
           // if (Permissions.Contains("W"))
            {
                AppDBContext db;
                switch (Mode)
                {
                    case DBType.Local:
                        db = _localDb;
                        break;

                    case DBType.Azure:
                        db = _azureDb;
                        break;

                    default:
                        return null;
                }
                if (isNew)
                    await db.AddAsync<Z>(value);
                else
                    db.Update<Z>(value);
                if (await db.SaveChangesAsync() > 0) return value;
            }
            //IsError = true;
            //ErrorMsg = "Access Denied";

            return null;
        }

        public async Task<List<Z>> SaveAllAsync(List<Z> values, bool isNew = true)
        {
           // if (Permissions.Contains("W"))
            {
                AppDBContext db;
                switch (Mode)
                {
                    case DBType.Local:
                        db = _localDb;
                        break;

                    case DBType.Azure:
                        db = _azureDb;
                        break;

                    default:
                        return null;
                }
                if (isNew)
                    await db.AddRangeAsync(values);
                else
                    db.UpdateRange(values);
                if (await db.SaveChangesAsync() > 0) return values;
            }
            //IsError = true;
            //ErrorMsg = "Access Denied";

            return null;
        }

        #endregion Save

        #region Delete

        //Delete
        public async Task<bool> DeleteZAsync(string id)
        {
           // if (Permissions.Contains("D"))
                switch (Mode)
                {
                    case DBType.Local:
                        var element = await _localDb.FindAsync<Z>(id);
                        _localDb.Remove<Z>(element);
                        return (await _localDb.SaveChangesAsync()) > 0;

                    case DBType.Azure:
                        var azureEle = await _azureDb.FindAsync<Z>(id);
                        _azureDb.Remove<Z>(azureEle);
                        return (await _azureDb.SaveChangesAsync()) > 0;

                    default:
                        return false;
                }
            //IsError = true;
            //ErrorMsg = "Access Denied";
            return false;
        }

        public async Task<bool> Delete(int id)
        {
           // if (Permissions.Contains("D"))
                switch (Mode)
                {
                    case DBType.Local:
                        var element = await _localDb.FindAsync<Z>(id);
                        _localDb.Remove<Z>(element);
                        return (await _localDb.SaveChangesAsync()) > 0;

                    case DBType.Azure:
                        var azureEle = await _azureDb.FindAsync<Z>(id);
                        _azureDb.Remove<Z>(azureEle);
                        return (await _azureDb.SaveChangesAsync()) > 0;

                    default:
                        return false;
                }
            //IsError = true;
            //ErrorMsg = "Access Denied";
            return false;
        }

        public async Task<bool> DeleteAsync(Z value)
        {
           // if (Permissions.Contains("D"))
                switch (Mode)
                {
                    case DBType.Local:

                        _localDb.Remove<Z>(value);
                        return (await _localDb.SaveChangesAsync()) > 0;

                    case DBType.Azure:

                        _azureDb.Remove<Z>(value);
                        return (await _azureDb.SaveChangesAsync()) > 0;

                    default:
                        return false;
                }
            //IsError = true;
            //ErrorMsg = "Access Denied";
            return false;
        }

        public async Task<bool> DeleteAsync(List<Z> values)
        {
           // if (Permissions.Contains("D"))
                switch (Mode)
                {
                    case DBType.Local:

                        _localDb.RemoveRange(values);
                        return (await _localDb.SaveChangesAsync()) == values.Count;

                    case DBType.Azure:

                        _azureDb.RemoveRange(values);
                        return (await _azureDb.SaveChangesAsync()) == values.Count;

                    default:
                        return false;
                }
            //IsError = true;
            //ErrorMsg = "Access Denied";
            return true;
        }

        #endregion Delete

        #region YearList

        //YearList
        public abstract Task<List<int>> GetYearListZ(string storeid);

        public abstract Task<List<int>> GetYearListZ();

        #endregion YearList

        #region Exsits

        //Exsits
        public async Task<bool> IsZExists(string id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    if (await _localDb.FindAsync<T>(id) != null) return true; else return false;

                case DBType.Azure:
                    if (await _azureDb.FindAsync<T>(id) != null) return true; else return false;

                default:
                    return false;
            }
        }

        public async Task<bool> IsZExists(int id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    if (await _localDb.FindAsync<T>(id) != null) return true; else return false;

                case DBType.Azure:
                    if (await _azureDb.FindAsync<T>(id) != null) return true; else return false;

                default:
                    return false;
            }
        }

        #endregion Exsits
    }

}