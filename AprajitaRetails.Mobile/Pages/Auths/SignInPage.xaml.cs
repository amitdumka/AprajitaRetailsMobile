using AprajitaRetails.Mobile.RemoteServices;
using Syncfusion.Maui.DataForm;
using System.ComponentModel.DataAnnotations;

namespace AprajitaRetails.Mobile.Pages.Auths
{
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
        }
    }

    public class LoginFormModel
    {
        [Display(Prompt = "Enter User name", Name = "User Name")]
        //[EmailAddress(ErrorMessage = "Enter your email - example@mail.com")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter the password")]
        public string Password { get; set; }
    }
    public class SignInFormViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignInFormViewModel" /> class.
        /// </summary>
        public SignInFormViewModel()
        {
            this.LoginFormModel = new LoginFormModel();
        }

        /// <summary>
        /// Gets or sets the login form model.
        /// </summary>
        public LoginFormModel LoginFormModel { get; set; }
    }
    public class LoginFormBehavior : Behavior<ContentPage>
    {

        /// <summary>
        /// Holds the data form object.
        /// </summary>
        private SfDataForm dataForm;

        /// <summary>
        /// Holds the login button instance.
        /// </summary>
        private Button loginButton;

        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            this.dataForm = bindable.FindByName<SfDataForm>("loginForm");
            this.dataForm.GenerateDataFormItem += this.OnGenerateDataFormItem;

            this.loginButton = bindable.FindByName<Button>("loginButton");

            if (this.loginButton != null)
            {
                this.loginButton.Clicked += OnLoginButtonCliked;
            }
        }

        /// <summary>
        /// Invokes on each data form item generation.
        /// </summary>
        /// <param name="sender">The data form.</param>
        /// <param name="e">The event arguments.</param>
        private void OnGenerateDataFormItem(object sender, GenerateDataFormItemEventArgs e)
        {
            if (e.DataFormItem != null && e.DataFormItem.FieldName == nameof(LoginFormModel.UserName) && e.DataFormItem is DataFormTextEditorItem textItem)
            {
                textItem.Keyboard = Keyboard.Text;
            }
        }

        /// <summary>
        /// Invokes on login button click.
        /// </summary>
        /// <param name="sender">The login button.</param>
        /// <param name="e">The event arguments.</param>
        private async void OnLoginButtonCliked(object sender, EventArgs e)
        {
            if (this.dataForm != null && App.Current?.MainPage != null)
            {
                if (this.dataForm.Validate())
                {
                    var usr = dataForm.DataObject as LoginFormModel;
                    //await App.Current.MainPage.DisplayAlert("", "Signed in successfully", "OK");
                    var user = await RestService.DoLoginAsync(usr.UserName, usr.Password);

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

                        Application.Current.MainPage = new AppShell();
                       // return true;
                    }
                    Notify.NotifyVLong($"User {usr.UserName} not Found ....");

                    //return false;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("", "Please enter the required details", "OK");
                }
            }
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);
            if (this.loginButton != null)
            {
                this.loginButton.Clicked -= OnLoginButtonCliked;
            }

            if (this.dataForm != null)
            {
                this.dataForm.GenerateDataFormItem -= this.OnGenerateDataFormItem;
            }
        }
    }
}
