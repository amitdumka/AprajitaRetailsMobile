namespace AprajitaRetails.Mobile.Views;

public partial class WidgetStat : ContentView
{
    public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(WidgetStat), string.Empty);

    public static readonly BindableProperty TitleFontSizeProperty = BindableProperty.Create(nameof(TitleFontSize), typeof(string), typeof(WidgetStat), string.Empty);
    public static readonly BindableProperty TitleFontColorProperty = BindableProperty.Create(nameof(TitleFontColor), typeof(string), typeof(WidgetStat), string.Empty);
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(WidgetStat), string.Empty);

    public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create(nameof(HeaderText), typeof(string), typeof(WidgetStat), string.Empty);
    public static readonly BindableProperty HeaderFontSizeProperty = BindableProperty.Create(nameof(HeaderFontSize), typeof(string), typeof(WidgetStat), string.Empty);
    public static readonly BindableProperty HeaderFontColorProperty = BindableProperty.Create(nameof(HeaderFontColor), typeof(string), typeof(WidgetStat), string.Empty);



    public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(nameof(Description), typeof(string), typeof(WidgetStat), string.Empty);
    public static readonly BindableProperty DescriptionFontSizeProperty = BindableProperty.Create(nameof(DescriptionFontSize), typeof(string), typeof(WidgetStat), string.Empty);
    public static readonly BindableProperty DescriptionFontColorProperty = BindableProperty.Create(nameof(DescriptionFontColor), typeof(string), typeof(WidgetStat), string.Empty);

    public static readonly BindableProperty WidgetBackgroundProperty = BindableProperty.Create(nameof(WidgetBackground), typeof(string), typeof(WidgetStat), string.Empty);
    public static readonly BindableProperty BoxColorProperty = BindableProperty.Create(nameof(BoxColor), typeof(string), typeof(WidgetStat), string.Empty);
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(string), typeof(WidgetStat), string.Empty);
   
    public string TitleFontSize
    {
        get => (string)GetValue(TitleFontSizeProperty);
        set => SetValue(TitleFontSizeProperty, value);
    }
    public string HeaderFontSize
    {
        get => (string)GetValue(HeaderFontSizeProperty);
        set => SetValue(HeaderFontSizeProperty, value);
    }
    public string DescriptionFontSize
    {
        get => (string)GetValue(DescriptionFontSizeProperty);
        set => SetValue(DescriptionFontSizeProperty, value);
    }

    public string TitleFontColor
    {
        get => (string)GetValue(TitleFontColorProperty);
        set => SetValue(TitleFontColorProperty, value);
    }
    public string HeaderFontColor
    {
        get => (string)GetValue(HeaderFontColorProperty);
        set => SetValue(HeaderFontColorProperty, value);
    }
    public string DescriptionFontColor
    {
        get => (string)GetValue(DescriptionFontColorProperty);
        set => SetValue(DescriptionFontColorProperty, value);
    }

    public string WidgetBackground
    {
        get => (string)GetValue(WidgetBackgroundProperty);
        set => SetValue(WidgetBackgroundProperty, value);
    }
    public string BoxColor
    {
        get => (string)GetValue(BoxColorProperty);
        set => SetValue(BoxColorProperty, value);
    }
    public string BorderColor
    {
        get => (string)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }



    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Icon
    {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
    public string HeaderText
    {
        get => (string)GetValue(HeaderTextProperty);
        set => SetValue(HeaderTextProperty, value);
    }

    public string Description
    {
        get => (string)GetValue( DescriptionProperty);
        set => SetValue( DescriptionProperty, value);
    }
    public WidgetStat()
	{
		InitializeComponent();
	}
}