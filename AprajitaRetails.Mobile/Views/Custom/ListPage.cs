using AprajitaRetails.Mobile.ViewModels.Base;


namespace AprajitaRetails.Mobile.Views.Custom
{
    public class ListPage : ContentPage
    {
        //protected T viewModel;
        protected RecordListingView rlv;
        public ToolbarItem tbAdd, tbRefesh,tbDelete;

        private void AddToolBar()
        {
            tbAdd = new ToolbarItem
            {
                Text = "Add",
                IconImageSource = ImageSource.FromFile("add.png"),
                Order = ToolbarItemOrder.Primary,
                Command = AddTBCommand

            };
            tbRefesh = new ToolbarItem
            {
                Command = RefreshTBCommand,
                Order = ToolbarItemOrder.Secondary,
                Text = "Refresh",
                IconImageSource = ImageSource.FromFile("add.png")
            };
            tbDelete = new ToolbarItem
            {
                Command = DeleteTBCommand,
                Text = "Delete",
                Order = ToolbarItemOrder.Secondary,
                IconImageSource = ImageSource.FromFile("add.png")
            };

            // "this" refers to a Page object
            this.ToolbarItems.Add(tbAdd);
            this.ToolbarItems.Add(tbRefesh);
            this.ToolbarItems.Add(tbDelete);

        }

        public ListPage()
        {

            AddToolBar();
            rlv = new RecordListingView
            {
                BindingContext = this,
                AddButtonText = "Add",
                RefreshButtonText = "Refresh",

                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            Content = new VerticalStackLayout
            {
                Children =
                {
                 rlv
                },
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
        }


        //Button
        public static readonly BindableProperty AddCommand = BindableProperty.Create(nameof(AddTBCommand), typeof(Command), typeof(ListPage), null);

        public Command AddTBCommand
        {
            get => (Command)GetValue(AddCommand);
            set => SetValue( AddCommand,value);
        }



        public static readonly BindableProperty DeleteCommand = BindableProperty.Create(nameof(DeleteTBCommand), typeof(Command), typeof(ListPage), null);

        public Command DeleteTBCommand
        {
            get => (Command)GetValue(DeleteCommand);
            set => SetValue(DeleteCommand, value);
        }

        public static readonly BindableProperty RefreshCommand = BindableProperty.Create(nameof(RefreshTBCommand), typeof(Command), typeof(ListPage), null);

        public Command RefreshTBCommand
        {
            get => (Command)GetValue(RefreshCommand);
            set => SetValue(RefreshCommand, value);
        }


    }
}