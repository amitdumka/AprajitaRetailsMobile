using AprajitaRetails.Mobile.Features.Test;
using AprajitaRetails.Mobile.FormEntry.Views;

namespace AprajitaRetails.Mobile
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            var formPage= new EmployeeEntryPage();
            Navigation.PushAsync(formPage);
        }
    }
}