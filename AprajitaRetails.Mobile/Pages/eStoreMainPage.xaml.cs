namespace AprajitaRetails.Mobile.Pages;

public partial class eStoreMainPage : ContentPage
{
	public eStoreMainPage()
	{
		InitializeComponent();
	}
}

public class eStoreMainViewModel
{
	private string _title;
	private string _storeName;

	private UserType _role;

	private decimal _totalSale;
	private decimal _yearlySale;

	private SortedDictionary<string, decimal> _saleStaffWise;

	private decimal _totalExpenses;
	private decimal _totalreciepts;
	private decimal _staffPayment;

	private SortedDictionary<string, AttUnit> _todayAttendaces;

	public eStoreMainViewModel()
	{
		InitView();
		_title = $"eStore : {_storeName}";
	}
	private void InitView()
	{
		
	}

}
