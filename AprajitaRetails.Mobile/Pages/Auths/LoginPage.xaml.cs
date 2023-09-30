namespace AprajitaRetails.Mobile.Pages.Auths
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        public LoginPage(Shell appshell)
        {
            InitializeComponent();
            viewModel.AppShell = appshell;
            //viewModel.SyncLocal();
        }

    }
}
