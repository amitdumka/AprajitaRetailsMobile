using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;

namespace AprajitaRetails.Mobile.Views.Custom;


public partial class RecordView : Popup
{
    private RecordViewModel viewModel;
   
    public RecordView(RecordViewModel model)
    {
        InitializeComponent();
        Size = new Microsoft.Maui.Graphics.Size(400, 500);
        viewModel = model;
        BindingContext = viewModel;
        SetTableData();

    }

    public void SetTableData()
    {
        var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(viewModel.JsonData);
        TableView TView = new TableView();
        ScrollView scroll = new ScrollView();
        scroll.HorizontalScrollBarVisibility=ScrollBarVisibility.Always;
        scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Always;
        scroll.WidthRequest = 350;
        scroll.HeightRequest = 350;
        scroll.BackgroundColor = Colors.LightSkyBlue;

        TableSection row = new TableSection();

        foreach (var item in dict)
        {
            if (item.Value != null)
            {
                TextCell cell = new TextCell();
                cell.Text = item.Key;
                cell.Detail = item.Value.ToString();
                row.Add(cell);
            }
            else
            {
                TextCell  Cell = new TextCell();
                Cell.Text = item.Key;
                Cell.Detail = " ";
                row.Add(Cell);
            }
        }
        TView.Root.Add(row);
        scroll.Content = TView;
        ContentFrame.Add(scroll);
    }

    void OnOKButtonClicked(object? sender, EventArgs e) => Close();

    private void OnEditButtonClicked(object sender, EventArgs e)
    {

    }
}

[INotifyPropertyChanged]
public partial class RecordViewModel
{
    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private string _message;

    [ObservableProperty]
    private string _id;

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string _jsonData;

    private void SetView()
    {
        var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(_jsonData);



    }
}
