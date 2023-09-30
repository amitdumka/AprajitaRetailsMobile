using AprajitaRetails.Mobile.Pages.Auths;

namespace AprajitaRetails.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new LoginPage(new AppShell());
        }
    }
}