using AKS.MAUI.Databases;
using System.Data;

namespace AprajitaRetails.Mobile.DataModels
{
    public class DashboardDataModel
    {
        public string StoreCode = "ARD";

        public DBType Mode { get; set; }
        public ConType ConType { get; set; }

        // Currently local and azure sql db
        protected AppDBContext _localDb, _azureDb;

        public AppDBContext GetContextLocal() => _localDb;

        public AppDBContext GetContextAzure() => _azureDb;

        public DashboardDataModel(ConType conType)
        {
            ConType = conType;
        }

        public DashboardDataModel(ConType conType, AppDBContext db)
        {
            ConType = conType;
            switch (ConType)
            {
                case ConType.Local:
                    _localDb = db;
                    break;

                case ConType.Remote:
                    break;

                case ConType.RemoteDb:
                    _azureDb = db;
                    break;

                case ConType.HybridApi:
                    break;

                case ConType.HybridDB:
                    break;

                case ConType.Hybrid:
                    _azureDb = db; _localDb = db;
                    break;

                default:
                    break;
            }
        }

        public DashboardDataModel(ConType conType, AppDBContext local, AppDBContext azure)
        {
            ConType = conType;
            _localDb = local; _azureDb = azure;
        }

        public AppDBContext GetContext()
        {
            switch (Mode)
            {
                case DBType.Local:
                    return _localDb;
                    break;

                case DBType.Azure:
                    return _azureDb;
                    break;

                case DBType.API:
                    break;

                case DBType.Remote:
                    break;

                case DBType.Mango:
                    break;

                case DBType.Others:
                    break;

                default:
                    return _localDb;
                    break;
            }
            return null;
        }

        public bool Connect()
        {
            switch (ConType)
            {
                case ConType.Local:
                    if(_localDb==null)
                    _localDb = new AKS.MAUI.Databases.AppDBContext(DBType.Local);
                    return (_localDb != null);

                case ConType.Remote:
                    break;

                case ConType.RemoteDb:
                    if (_azureDb == null)
                        _azureDb = new AKS.MAUI.Databases.AppDBContext(DBType.Azure);
                    return (_azureDb != null);

                case ConType.HybridApi:
                    break;

                case ConType.HybridDB:
                    if (_azureDb == null)
                        _azureDb = new AKS.MAUI.Databases.AppDBContext(DBType.Azure);
                    if (_localDb == null)
                        _localDb = new AKS.MAUI.Databases.AppDBContext(DBType.Local);
                    return (_azureDb != null && _localDb != null);

                case ConType.Hybrid:
                    if (_azureDb == null)
                        _azureDb = new AKS.MAUI.Databases.AppDBContext(DBType.Azure);
                    if (_localDb == null)
                        _localDb = new AKS.MAUI.Databases.AppDBContext(DBType.Local);
                    return (_azureDb != null && _localDb != null);

                default:
                    if (_localDb == null)
                        _localDb = new AKS.MAUI.Databases.AppDBContext(DBType.Local);
                    return (_localDb != null);
            }
            return false;
        }
    }

    public class CommonDataModel
    {
        public static List<DynVM> GetBankAccount(AppDBContext db)
        {
            return db.BankAccounts.Select(c => new DynVM
            {
                StoreId = c.StoreId,
                ValueMember = "AccountNumber",
                ValueData = c.AccountNumber,
                BoolMember = "IsActive",
                BoolValue = c.IsActive,
                DisplayData = c.AccountHolderName,
                DisplayMember = "AccountHolderName"
            }).ToList();
        }

        public static List<DynVM> GetEmployeeList(AppDBContext db)
        {
            return db.Employees.Where(c => c.IsWorking && !c.IsTailors).Select(c => new DynVM
            {
                StoreId = c.StoreId,
                DisplayMember = "StaffName",
                BoolMember = "IsWorking",
                ValueMember = "EmployeeId",
                ValueData = c.EmployeeId,
                DisplayData = c.StaffName,
                BoolValue = c.IsWorking
            }).ToList();
        }

        public static List<DynVM> GetParty(AppDBContext db)
        {
            return db.Parties.Select(c => new DynVM
            {
                StoreId = c.StoreId,
                DisplayMember = "PartyName",
                ValueMember = "PartyId",
                DisplayData = c.PartyName,
                ValueData = c.PartyId,
            }).ToList();
        }

        public static List<DynVM> GetStoreList(AppDBContext db)
        {
            return db.Stores.Select(c => new DynVM
            {
                StoreId = c.StoreId,
                DisplayMember = "StoreName",
                DisplayData = c.StoreName,
                BoolValue = c.IsActive,
                BoolMember = "IsActive"
            }).ToList();
        }

        public static List<DynVM> GetTranscation(AppDBContext db)
        {
            return db.TranscationModes.Select(c => new DynVM
            {
                DisplayMember = "TranscationName",
                ValueMember = "TranscationId",
                DisplayData = c.TransactionName,
                ValueData = c.TransactionId,
            }).ToList();
        }

        public static List<DynVM> GetSalemanList(AppDBContext db, string StoreCode)
        {
            return db.Salesmen.Where(c => c.StoreId == StoreCode && c.IsActive)
                   .Select(c => new DynVM
                   {
                       ValueData = c.SalesmanId,
                       DisplayData = c.Name
                   }).ToList();
        }

        public static List<DynVM> GetPosList(AppDBContext db, string StoreCode)
        {
            return db.EDCTerminals.Where(c => c.StoreId == StoreCode && c.Active).Select(c => new DynVM { ValueData = c.EDCTerminalId, DisplayData = c.Name }).ToList();
        }
    }
}