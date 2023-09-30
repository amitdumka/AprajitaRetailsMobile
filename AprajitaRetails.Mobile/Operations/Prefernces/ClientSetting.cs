using AprajitaRetails.Shared.Models.Auth;
using System.Text.Json;

namespace AprajitaRetails.Mobile.Operations.Prefernces
{
    public class ClientSetting
    {
        
        public static void SetPref<T>(string Key, T Value)
        {
            Preferences.Default.Set<T>(Key, Value);
        }
        public static T GetPref<T>(string Key, T defaultValue)
        {
            return Preferences.Default.Get(Key, defaultValue);
        }

        public static async void SetSecure(string Key, string Value)
        {
            await SecureStorage.Default.SetAsync(Key, Value);
        }
        public static async Task<string> GetSecureAsync(string Key)
        {
            return await SecureStorage.Default.GetAsync(Key);
        }

        
        public static void UpdateCurrentSession()
        {
            var loged = GetPref<string>("LoggedUser","");
            var user = JsonSerializer.Deserialize< LoggedUser > (loged);
            CurrentSession.Name = user.FullName;
            CurrentSession.UserName = user.Id;
            CurrentSession.StoreGroupId = user.StoreGroupId;
            CurrentSession.AppClinetId = user.AppClinetId;
            CurrentSession.StoreCode = user.StoreId;
            CurrentSession.UserName = user.Id;
            CurrentSession.UserType = user.UserType;
            CurrentSession.EmployeeId = user.EmployeeId;
            CurrentSession.Role = user.Permission;

        }
        public static void SetPostLogin(LoggedUser user)
        {
            // Preferences.Default.Set(Key, Value);
            //Preferences.Default.Set<LoggedUser>("LoggedUser", user);
            CurrentSession.Name = user.FullName;
            CurrentSession.UserName = user.Id; 
            CurrentSession.StoreGroupId = user.StoreGroupId;
            CurrentSession.AppClinetId = user.AppClinetId;
            CurrentSession.StoreCode = user.StoreId;
            CurrentSession.UserName = user.Id;
            CurrentSession.UserType = user.UserType;
            CurrentSession.EmployeeId=user.EmployeeId;
            CurrentSession.Role = user.Permission;

        }

       
    }


    public static class CurrentSession
    {
        public static string StoreCode { get; set; } = "ARD";
        public static string StoreName { get; set; } = "Aprajita Retails";
        public static string CityName { get; set; } = "Dumka";
        public static string Address { get; set; } = "Bhagalpur Road, Dumka";
        public static string TaxNumber { get; set; } = "20AJHP7396P1ZV";
        public static string PhoneNo { get; set; } = "06434-224461";

        //Client 
        public static string StoreGroupId { get; set; }
        public static Guid AppClinetId { get; set; }
        public static string AppClinetName { get; private set; } = "Aprajita Retails";

        public static string Name { get;  set; }

        public static string UserName { get; set; } = "AuotAdmin";
        public static UserType UserType { get; set; } = UserType.Guest;
        public static string EmployeeId { get; set; }
        public static RolePermission Role { get; set; } = RolePermission.Guest;
        
        public static void Clear()
        {
            UserName  = StoreCode = StoreName = CityName = Address = TaxNumber = PhoneNo = StoreGroupId="";
            UserType = UserType.Guest; Role = RolePermission.Guest;

        }
    }

}
