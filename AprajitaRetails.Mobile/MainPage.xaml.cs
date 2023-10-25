using AprajitaRetails.Mobile.Features.Test;

namespace AprajitaRetails.Mobile
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            //count++;

            //if (count == 1)
            //    CounterBtn.Text = $"Clicked {count} time";
            //else
            //    CounterBtn.Text = $"Clicked {count} times";

            //SemanticScreenReader.Announce(CounterBtn.Text);

            //var autoFormPage = new AutoFormPage<Person>();
            //Navigation.PushAsync(autoFormPage);
            var formPage= new FormPage();
            Navigation.PushAsync(formPage);
        }
    }
}