using AKS.MAUI.Databases;

namespace AprajitaRetails.Mobile.DataModels.Base
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

        public DashboardDataModel()
        {
            ConType = ConType.Remote;
            Mode = DBType.API;
        }

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
                    if (_localDb == null)
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
}