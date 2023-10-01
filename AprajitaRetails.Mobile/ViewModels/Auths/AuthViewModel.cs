using AprajitaRetails.Mobile.Helpers;
using AprajitaRetails.Mobile.Operations.Prefernces;
using AprajitaRetails.Mobile.Pages.Auths;
using AprajitaRetails.Mobile.RemoteServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;

namespace AprajitaRetails.Mobile.ViewModels.Auths
{
    public partial class AuthViewModel : ObservableValidator
    {
        #region PropertyFields

        [ObservableProperty]
        //[NotifyPropertyChangedRecipients]
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(5)]
        [MaxLength(15)]
        private string _userName;

        [ObservableProperty]
        //[NotifyPropertyChangedRecipients]
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(4)]
        [MaxLength(12)]
        private string _password;

        [ObservableProperty]
        private static Shell _appShell;

        [ObservableProperty]
        private string _guestName;

        #endregion PropertyFields

        [ObservableProperty]
        private bool _isEnableSign;

        [ObservableProperty]
        private string _title;

        public AuthViewModel()
        {
            InitViewModel();
        }

        protected void InitViewModel()
        {
            Title = "Login";
            GuestName = "No user ";
            UserName = "AmitKumar";
            Password = "Dumka@1234";
            IsEnableSign = true;
        }

        #region Login

        [RelayCommand]
        private void ShowErrors()
        {
            string message = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
            Notify.NotifyLong(message);
        }

        [RelayCommand]
        private async Task<bool> SignUP()
        {
            Notify.NotifyVLong("You are not authorized to sign up");
            return true;
        }
        [RelayCommand]
        private async Task<bool> SignIn()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                ShowErrors();
                return false;
            }
            else
            {
                var user = await RestService.DoLoginAsync(_userName, _password);

                if (user != null)
                {
                    //CurrentSession.IsLoggedIn = true;
                    //CurrentSession.LoggedTime = DateTime.Now;

                    Notify.NotifyVLong($"Welcome, {user.FullName}!, Now you can operate in , {user.Permission}, mode. ");

                    //var store = await DataModel.GetStore(user.StoreId);

                    //if (store != null)
                    //{
                    //    CurrentSession.Address = store.City + "\t" + store.State;
                    //    CurrentSession.TaxNumber = store.GSTIN;
                    //    CurrentSession.StoreName = store.StoreName;
                    //    CurrentSession.PhoneNo = store.StorePhoneNumber;
                    //    CurrentSession.CityName = store.City;

                    //    return true;
                    //}

                    Application.Current.MainPage = AppShell;
                    return true;
                }
                Notify.NotifyVLong($"User {UserName} not Found ....");

                return false;
            }
        }

        [RelayCommand]
        private async Task<bool> SignOut()
        {
            CurrentSession.Clear();
            Application.Current.MainPage = new LoginPage(AppShell);
            return true;
        }

        #endregion Login
    }
}