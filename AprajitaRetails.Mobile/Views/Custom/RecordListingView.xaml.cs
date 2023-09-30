using Syncfusion.Maui.DataGrid;

namespace AprajitaRetails.Mobile.Views.Custom;

public partial class RecordListingView : ContentView
{
    public  SfDataGrid DataGrid;
    public ColumnCollection gridColumns;
    
    public RecordListingView()
	{
		InitializeComponent();
        // var p = this.Parent;
        //var c = p.BindingContext;
        //BindingContext = this.Parent.BindingContext;
        //DataGrid = dataGrid;
        //if (gridColumns != null) 
        //   dataGrid.Columns = gridColumns;
        if(Cols!=null)
            dataGrid.Columns = Cols;
	}

    #region PropertyDefine

    // Cols for Datagrids
    public static readonly BindableProperty ColsProperty = BindableProperty.Create(nameof(Cols), typeof(ColumnCollection), typeof(RecordListingView), null);
    
    public ColumnCollection Cols
    {
        get => (ColumnCollection)GetValue(RecordListingView.ColsProperty);
        set => UpdateGrid(RecordListingView.ColsProperty, value);
    }
    
    private void UpdateGrid(BindableProperty prop,object value)
    {
        SetValue(prop, value);
        dataGrid.Columns = (ColumnCollection)value;
    }
   
    //Add Button
    public static readonly BindableProperty AddButtonTextProperty = BindableProperty.Create(nameof(AddButtonText), typeof(string), typeof(RecordListingView), "Add");

    public string AddButtonText
    {
        get => (string)GetValue(RecordListingView.AddButtonTextProperty);
        set => SetValue(RecordListingView.AddButtonTextProperty, value);
    }

    //Delete Button
    public static readonly BindableProperty DeleteButtonTextProperty = BindableProperty.Create(nameof(DeleteButtonText), typeof(string), typeof(RecordListingView), "Delete");
    
    public string DeleteButtonText
    {
        get => (string)GetValue(RecordListingView.DeleteButtonTextProperty);
        set => SetValue(RecordListingView.DeleteButtonTextProperty, value);
    }
    //Delete Button
    public static readonly BindableProperty RefreshButtonTextProperty = BindableProperty.Create(nameof(RefreshButtonText), typeof(string), typeof(RecordListingView), "Refresh");

    public string RefreshButtonText
    {
        get => (string)GetValue(RecordListingView.RefreshButtonTextProperty);
        set => SetValue(RecordListingView.RefreshButtonTextProperty, value);
    }

    #endregion
}
