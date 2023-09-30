using AprajitaRetails.Mobile.Pages.Auths;

namespace AprajitaRetails.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            //Sync Ngo9BigBOggjHTQxAR8/V1NHaF5cXmVCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdgWXZdcHZVRGFeUUJyW0M=
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NHaF5cXmVCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdgWXZdcHZVRGFeUUJyW0M=");
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new LoginPage(new AppShell());
        }
    }
}