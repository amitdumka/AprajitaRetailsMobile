namespace AprajitaRetails.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {

                // Notify.NotifyLong("Error:" + ex.Message);
            }

        }
        async void MenuItem_Clicked(System.Object sender, System.EventArgs e)
        {
            //var result = await DisplayAlert("Logout", "Do you want to Logout!", "Yes", "No");
            //if (result)
            //{
            //    CurrentSession.Clear();
            //    App.Current.MainPage = new LoginPage(new AppShell());
            //}
        }

        private void SyncDown_Clicked(object sender, EventArgs e)
        {
            //BackgroundService service = new SyncDownService();
            //service.InitService();
            //service.GetInstance.RunWorkerAsync(LocalSync.All);
        }
    }
}