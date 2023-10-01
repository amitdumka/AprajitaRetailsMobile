using AprajitaRetails.Mobile.Pages.Auths;
using AprajitaRetails.Mobile.Pages.EntryPages;
using AprajitaRetails.Mobile.ViewModels.EntryPages;

namespace AprajitaRetails.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            try
            {
                InitializeComponent();
                RegisterRoutes();
            }
            catch (Exception ex)
            {

                // Notify.NotifyLong("Error:" + ex.Message);
            }

        }

        private void RegisterRoutes()
        {
            // Routing.RegisterRoute("voucher/Entry", typeof(VoucherEntryPage));
            //Routing.RegisterRoute("cashvoucher/Entry", typeof(CashVoucherEntryPage));
            //Routing.RegisterRoute("banking/bank/Entry", typeof(BankEntryPage));
            //Routing.RegisterRoute("sale/Entry", typeof(SaleEntryPage));
            Routing.RegisterRoute("Attendance/Entry", typeof(AttendanceEntryPage));
        }
        async void MenuItem_Clicked(System.Object sender, System.EventArgs e)
        {
            var result = await DisplayAlert("Logout", "Do you want to Logout!", "Yes", "No");
            if (result)
            {
                CurrentSession.Clear();
                App.Current.MainPage = new LoginPage(new AppShell());
            }
        }

        private void SyncDown_Clicked(object sender, EventArgs e)
        {
            //BackgroundService service = new SyncDownService();
            //service.InitService();
            //service.GetInstance.RunWorkerAsync(LocalSync.All);
        }

        
    }
}