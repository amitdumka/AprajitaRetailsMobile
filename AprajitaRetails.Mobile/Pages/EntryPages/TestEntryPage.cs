using AprajitaRetails.Mobile.DataForm;

namespace AprajitaRetails.Mobile.Pages.EntryPages
{
    public class TestEntryPage : ContentPage
    {
        public TestEntryPage()
        {
            Content = new Grid
            {
                Children =
                {
                   new PaymentForm()
                }
            };
        }
    }
}
