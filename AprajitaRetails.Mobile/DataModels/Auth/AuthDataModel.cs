////using AKS.Shared.Commons.Models;
////using AKS.Shared.Commons.Models.Auth;
//using AprajitaRetails.Mobile.DataModels.Base;
//using AprajitaRetails.Shared.Models.Auth;
//using AprajitaRetails.Shared.Models.Stores;
//using Microsoft.EntityFrameworkCore;

//namespace AprajitaRetails.Mobile.DataModels.Auth
//{
//    public class AuthDataModel : BaseDataModel<User>
//    {
//        public AuthDataModel(ConType conType) : base(conType)
//        {
//        }

//        public AuthDataModel(ConType conType,RolePermission role) : base(conType, role)
//        {
//        }

//        public override Task<string> GenrateID()
//        {
//            throw new NotImplementedException();
//        }

//        public override List<User> GetFiltered(QueryParam query)
//        {
//            throw new NotImplementedException();
//        }

//        public override async Task<List<User>> GetItemsAsync(string storeid)
//        {
//            switch (Mode)
//            {
//                case DBType.Local:
//                    return await _localDb.Users.ToListAsync();
//                case DBType.Azure:
//                    return await _azureDb.Users.ToListAsync();

//                default:
//                    return null;

//            }
//        }

//        public override List<int> GetYearList(string storeid)
//        {
//            throw new NotImplementedException();
//        }

//        public override List<int> GetYearList()
//        {
//            throw new NotImplementedException();
//        }

//        public override async Task<bool> InitContext() => Connect();

//        public async Task<Store> GetStore(string sid)
//        {
//            switch (Mode)
//            {
//                case DBType.Local:
//                    return await _localDb.Stores.FindAsync(sid);
//                case DBType.Azure:
//                    return await _azureDb.Stores.FindAsync(sid);

//                default:
//                    return null;

//            }
//        }
//        //public LoggedUser SignIn(string userName, string password)
//        //{
//        //    User user = null;
//        //    switch (Mode)
//        //    {
//        //        case DBType.Local:
//        //            user = _localDb.Users.Where(c => c.UserName == userName && c.Password == password).FirstOrDefault();

//        //            break;
//        //        case DBType.Azure:
//        //            user = _azureDb.Users.Where(c => c.UserName == userName && c.Password == password).FirstOrDefault();
//        //            break;
//        //        case DBType.API:
//        //            break;
//        //        default:
//        //            break;
//        //    }

//        //    return user;
//        //}
//        /// <summary>
//        /// Change Password
//        /// </summary>
//        /// <param name="userName"></param>
//        /// <param name="oldPassword"></param>
//        /// <param name="newPassword"></param>
//        /// <returns></returns>
//        public async Task<bool> PasswordChangeAsync(string userName, string oldPassword, string newPassword)
//        {
//            User user;
//            //TODO: SendInfo or Return in ReturnType class to manage return infomation along with error
//            user = _localDb.Users.Where(c => c.UserName == userName && c.Password == oldPassword).FirstOrDefault();

//            switch (Mode)
//            {
//                case DBType.Local:
//                    user = _localDb.Users.Where(c => c.UserName == userName && c.Password == oldPassword).FirstOrDefault();
//                    break;
//                case DBType.Azure:
//                    user = _azureDb.Users.Where(c => c.UserName == userName && c.Password == oldPassword).FirstOrDefault();
//                    break;
//                default:
//                    break;
//            }

//            if (user != null)
//            {
//                // TODO: Send info that old password is not matched or user not found!.
//                user.Password = newPassword;
//                user = await SaveAsync(user, false);
//                return (user != null);
//            }
//            else
//                return false;

//        }

//        public async Task<bool> SignUpAsync(User user) => (await SaveAsync(user, true)) != null;

//        public async Task<bool> SyncRemote(DBType dbType)
//        {
//            switch (dbType)
//            {
//                case DBType.Azure:
//                    if (_azureDb == null)
//                    {
//                        _azureDb = new AKS.MAUI.Databases.AppDBContext();
//                    }
//                    var users = _azureDb.Users.Where(c => c.StoreId == StoreCode).ToList();
//                    int newUser = 0;
//                    foreach (var user in users)
//                    {
//                        if (!_localDb.Users.Any(c => c.UserName == user.UserName))
//                        {
//                            _localDb.Users.Add(user);
//                            newUser++;
//                        }
//                    }
//                    int count = await _localDb.SaveChangesAsync();
//                    return (newUser == count);
                    

//                case DBType.API:
//                    //RemoteSingleServer server = new RemoteSingleServer("", "User");
//                    //var users = await server.GetByUrl<List<User>>(WebAPI.APIBase + WebAPI.Users);
//                    //foreach (var user in users)
//                    //{
//                    //    user.UserId = 0; //TODO: remove is leagace.
//                    //}
//                    //_localDb.Users.AddRange(users);
//                    //return _localDb.SaveChanges() > 0;
//                    break;
//                case DBType.Mango: break;
//                case DBType.Remote: break;
//                default:
//                    break;
//            }

//            return false;
//        }

//    }
//}