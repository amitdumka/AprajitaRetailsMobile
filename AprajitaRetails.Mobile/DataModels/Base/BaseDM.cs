using AKS.MAUI.Databases;
using AprajitaRetails.Mobile.RemoteServices;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Mobile.DataModels.Base
{
    //Single Model ,
    //Will be Using DTO to handle multiple  deep inclusion to make it easy implemetation.

    public abstract class BaseDM<T> where T : class
    {
        #region ApiSetting

        protected string apiurl;
        protected string apiDtoURL;

        #endregion ApiSetting

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

        protected RestService _apiDB;

        public AppDBContext GetContextLocal() => _localDb;

        public AppDBContext GetContextAzure() => _azureDb;

        public RestService GetRestDB() => _apiDB;

        #endregion Fields

        #region Constructor

        public BaseDM(ConType conType)
        {
            ConType = conType;
            Role = RolePermission.Salesmen;
            Permissions = "R";
        }

        public BaseDM(ConType conType, RolePermission role)
        {
            ConType = conType;
            Role = role;
            //Permissions = AuthHelper.GetPermission(role);
        }

        public BaseDM()
        {
            ConType = ConType.Remote;
            Role = RolePermission.StoreManager;
            Mode = DBType.API;
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

                    _apiDB = new RestService();
                    _localDb = new AKS.MAUI.Databases.AppDBContext(DBType.Local);
                    return (_localDb != null);

                case ConType.RemoteDb:
                    _azureDb = new AKS.MAUI.Databases.AppDBContext(DBType.Azure);
                    return (_azureDb != null);

                case ConType.HybridApi:
                    _apiDB = new RestService();
                    break;

                case ConType.HybridDB:
                    _azureDb = new AKS.MAUI.Databases.AppDBContext(DBType.Azure);
                    _localDb = new AKS.MAUI.Databases.AppDBContext(DBType.Local);
                    return (_azureDb != null && _localDb != null);

                case ConType.Hybrid:
                    _apiDB = new RestService();
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

        [Obsolete]
        public int Count()
        {
            AppDBContext db;
            switch (Mode)
            {
                //case DBType.API:
                //  break;
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

        #region GET

        //Get By ID
        public async Task<T> GetAsync(string id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    return await _localDb.FindAsync<T>(id);

                case DBType.Azure:
                    return await _azureDb.FindAsync<T>(id);

                case DBType.API:
                    return await _apiDB.GetByIdAsync<T>(apiurl, id);

                default:
                    return null;
            }
        }

        public async Task<T> GetAsync(int id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    return await _localDb.FindAsync<T>(id);

                case DBType.Azure:
                    return await _azureDb.FindAsync<T>(id);

                case DBType.API:
                    return await _apiDB.GetByIdAsync<T>(apiurl, id);

                default:
                    return null;
            }
        }

        public async Task<T> GetAsync(Guid id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    return await _localDb.FindAsync<T>(id);

                case DBType.Azure:
                    return await _azureDb.FindAsync<T>(id);

                case DBType.API:
                    return await _apiDB.GetByIdAsync<T>(apiurl, id);

                default:
                    return null;
            }
        }

        public async Task<List<T>> GetByStoreDTO(string storeId)
        {
            switch (Mode)
            {
                case DBType.Local:
                    Entity = await _localDb.Set<T>().ToListAsync();
                    break;

                case DBType.Azure:
                    Entity = await _azureDb.Set<T>().ToListAsync();
                    break;

                case DBType.API:
                    Entity = await _apiDB.GetAllAsync<T>(apiDtoURL);
                    break;

                default:
                    return null;
            }
            return Entity.ToList();
        }

        public async Task<List<T>> RefreshData()
        {
            switch (Mode)
            {
                case DBType.Local:
                    Entity = await _localDb.Set<T>().ToListAsync();
                    break;

                case DBType.Azure:
                    Entity = await _azureDb.Set<T>().ToListAsync();
                    break;

                case DBType.API:
                    Entity = await _apiDB.GetAllAsync<T>(apiurl);
                    break;

                default:
                    return null;
            }
            return Entity.ToList();
        }

        #endregion GET

        #region DeleteRegion

        public async Task<bool> DeleteAsync(Guid id)
        {
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

                    case DBType.API:
                        return await _apiDB.DeleteAsync(apiurl, id);

                    default: return false;
                }
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
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

                    case DBType.API:
                        return await _apiDB.DeleteAsync(apiurl, id);

                    default: return false;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
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

                    case DBType.API:
                        return await _apiDB.DeleteAsync(apiurl, id);

                    default: return false;
                }
            }
        }

        #endregion DeleteRegion

        #region SaveRegion

        public async Task<T> SaveAsync(T value, bool isNew = true)
        {
            AppDBContext db;

            switch (Mode)
            {
                case DBType.Local:
                    db = _localDb;
                    if (isNew)
                        await db.AddAsync<T>(value);
                    else
                        db.Update<T>(value);

                    if (await db.SaveChangesAsync() > 0) return value;
                    else return null;

                case DBType.Azure:

                    db = _azureDb;
                    if (isNew)
                        await db.AddAsync<T>(value);
                    else
                        db.Update<T>(value);

                    if (await db.SaveChangesAsync() > 0) return value;
                    else return null;

                case DBType.API:
                    object ret = _apiDB.SaveAsync(apiurl, value, isNew);
                    return ret as T;

                default:
                    return null;
            }
        }

        #endregion SaveRegion

        #region Extrafunction

        public abstract Task<List<T>> GetItemsAsync(string storeid);

        public abstract List<T> GetFiltered(QueryParam query);

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

        #endregion Extrafunction
    }

    public abstract class BaseDM<T, DTO> where DTO : class where T : class
    {
        #region ApiSetting

        protected string apiurl;
        protected string apiDtoURL;

        #endregion ApiSetting

        #region Fields

        public string StoreCode;
        protected RolePermission Role;
        protected string Permissions;

        public DBType Mode { get; set; } = DBType.API;
        protected ConType ConType { get; set; } = ConType.Remote;

        protected List<T> Entity { get; set; }
        protected List<DTO> EntityDTO { get; set; }

        protected bool IsError { get; set; }
        protected string ErrorMsg { get; set; }

        public string GetError()
        {
            if (IsError) return ErrorMsg;
            else return "OK";
        }

        // Currently local and azure sql db
        protected AppDBContext _localDb, _azureDb;

        protected RestService _apiDB;

        public AppDBContext GetContextLocal() => _localDb;

        public AppDBContext GetContextAzure() => _azureDb;

        public RestService GetRestDB() => _apiDB;

        #endregion Fields

        #region Constructor

        public BaseDM(ConType conType)
        {
            ConType = conType;
            Role = RolePermission.Salesmen;
            Permissions = "R";
        }

        public BaseDM(ConType conType, RolePermission role)
        {
            ConType = conType;
            Role = role;
            //Permissions = AuthHelper.GetPermission(role);
        }

        public BaseDM()
        {
            ConType = ConType.Remote;
            Role = RolePermission.StoreManager;
            Mode = DBType.API;
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

                    _apiDB = new RestService();
                    _localDb = new AKS.MAUI.Databases.AppDBContext(DBType.Local);
                    return (_localDb != null);

                case ConType.RemoteDb:
                    _azureDb = new AKS.MAUI.Databases.AppDBContext(DBType.Azure);
                    return (_azureDb != null);

                case ConType.HybridApi:
                    _apiDB = new RestService();
                    break;

                case ConType.HybridDB:
                    _azureDb = new AKS.MAUI.Databases.AppDBContext(DBType.Azure);
                    _localDb = new AKS.MAUI.Databases.AppDBContext(DBType.Local);
                    return (_azureDb != null && _localDb != null);

                case ConType.Hybrid:
                    _apiDB = new RestService();
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

        [Obsolete]
        public int Count()
        {
            AppDBContext db;
            switch (Mode)
            {
                //case DBType.API:
                //  break;
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

        #region GET

        //Get By ID
        public async Task<T> GetAsync(string id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    return await _localDb.FindAsync<T>(id);

                case DBType.Azure:
                    return await _azureDb.FindAsync<T>(id);

                case DBType.API:
                    return await _apiDB.GetByIdAsync<T>(apiurl, id);

                default:
                    return null;
            }
        }

        public async Task<T> GetAsync(int id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    return await _localDb.FindAsync<T>(id);

                case DBType.Azure:
                    return await _azureDb.FindAsync<T>(id);

                case DBType.API:
                    return await _apiDB.GetByIdAsync<T>(apiurl, id);

                default:
                    return null;
            }
        }

        public async Task<T> GetAsync(Guid id)
        {
            switch (Mode)
            {
                case DBType.Local:
                    return await _localDb.FindAsync<T>(id);

                case DBType.Azure:
                    return await _azureDb.FindAsync<T>(id);

                case DBType.API:
                    return await _apiDB.GetByIdAsync<T>(apiurl, id);

                default:
                    return null;
            }
        }

        public async Task<List<DTO>> GetByStoreDTO(string storeId)
        {
            switch (Mode)
            {
                case DBType.Local:
                    EntityDTO = await _localDb.Set<DTO>().ToListAsync();
                    break;

                case DBType.Azure:
                    EntityDTO = await _azureDb.Set<DTO>().ToListAsync();
                    break;

                case DBType.API:
                    EntityDTO = await _apiDB.GetAllAsync<DTO>(apiDtoURL);
                    break;

                default:
                    return null;
            }
            return EntityDTO.ToList();
        }

        public async Task<List<DTO>> RefreshData()
        {
            switch (Mode)
            {
                case DBType.Local:
                    EntityDTO = await _localDb.Set<DTO>().ToListAsync();
                    break;

                case DBType.Azure:
                    EntityDTO = await _azureDb.Set<DTO>().ToListAsync();
                    break;

                case DBType.API:
                    EntityDTO = await _apiDB.GetAllAsync<DTO>(apiurl);
                    break;

                default:
                    return null;
            }
            return EntityDTO.ToList();
        }

        #endregion GET

        #region DeleteRegion

        public async Task<bool> DeleteAsync(Guid id)
        {
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

                    case DBType.API:
                        return await _apiDB.DeleteAsync(apiurl, id);

                    default: return false;
                }
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
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

                    case DBType.API:
                        return await _apiDB.DeleteAsync(apiurl, id);

                    default: return false;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
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

                    case DBType.API:
                        return await _apiDB.DeleteAsync(apiurl, id);

                    default: return false;
                }
            }
        }

        #endregion DeleteRegion

        #region SaveRegion

        public async Task<T> SaveAsync(T value, bool isNew = true)
        {
            AppDBContext db;

            switch (Mode)
            {
                case DBType.Local:
                    db = _localDb;
                    if (isNew)
                        await db.AddAsync<T>(value);
                    else
                        db.Update<T>(value);

                    if (await db.SaveChangesAsync() > 0) return value;
                    else return null;

                case DBType.Azure:

                    db = _azureDb;
                    if (isNew)
                        await db.AddAsync<T>(value);
                    else
                        db.Update<T>(value);

                    if (await db.SaveChangesAsync() > 0) return value;
                    else return null;

                case DBType.API:
                    object ret = await _apiDB.SaveAsync(apiurl, value, isNew);
                    return ret as T;

                default:
                    return null;
            }
        }

        #endregion SaveRegion

        #region Extrafunction

        public abstract Task<List<T>> GetItemsAsync(string storeid);

        public abstract List<DTO> GetFiltered(QueryParam query);

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

        #endregion Extrafunction
    }
}