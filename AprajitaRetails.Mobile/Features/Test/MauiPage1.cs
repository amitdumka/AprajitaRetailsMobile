namespace AprajitaRetails.Mobile.Features.Test
{
    public class MauiPage1 : ContentPage
    {
        public MauiPage1()
        {
            Content = new Grid
            {
                Children =
                {
                    new Label
                    {
                        Text = "Welcome to .NET MAUI!!!",
                        TextColor = Colors.Purple,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                    }
                }
            };
        }
    }
}
