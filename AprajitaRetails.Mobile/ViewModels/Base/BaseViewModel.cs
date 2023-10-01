using AprajitaRetails.Mobile.Views.Custom;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.Maui.DataGrid;
using System.Collections.ObjectModel;
using AprajitaRetails.Mobile.DataModels.Base;
using System.ComponentModel;

namespace AprajitaRetails.Mobile.ViewModels.Base
{
    [ObservableRecipient]
    public abstract partial class BaseViewModel<T, DM> : ObservableValidator
    {
        public const string Descending = "Descending";
        public const string Ascending = "Ascending";

        #region Field

        protected DM DataModel;

        [ObservableProperty]
        protected RecordListingView _rlv;

        [ObservableProperty]
        protected Page _currentPage;

        [ObservableProperty]
        protected string _defaultSortedColName;

        [ObservableProperty]
        protected string _defaultSortedOrder = Descending;

        [ObservableProperty]
        protected string _icon;

        [ObservableProperty]
        protected string _title;

        //[ObservableProperty]
        //protected bool _isNew;

        [ObservableProperty]
        protected T _entity;

        [ObservableProperty]
        protected ObservableCollection<T> _entities;

        [ObservableProperty]
        protected int _recordCount;

        [ObservableProperty]
        protected RolePermission _role;
        [ObservableProperty]
        protected UserType _userType;

        [ObservableProperty]
        protected bool isRefreshing;

        #endregion Field

        public DM GetDataModel() => DataModel;
        public BaseViewModel()
        {
            InitViewModel();
        }
        #region Abstractfunctions

        // [RelayCommand]
        //protected abstract Task<bool> Edit(T value);
        //[RelayCommand]
        //protected abstract Task<bool> Delete();
        //[RelayCommand]
        //protected abstract Task<T> Get(string id);
        //[RelayCommand]
        //protected abstract Task<T> GetById(int id);
        //[RelayCommand]
        //protected abstract Task<List<T>> GetList();
        //[RelayCommand]
        //protected abstract Task<List<T>> Filter(string fitler);
        [RelayCommand]
        protected abstract void AddButton();

        [RelayCommand]
        protected abstract void DeleteButton();

        [RelayCommand]
        protected  void RefreshButton()
        {
            Entities.Clear();
            Notify.NotifyShort("Refresh Cash Vouches....");
            FetchAsync();
        }
        protected abstract  Task FetchAsync();
        protected abstract void InitViewModel();

        //protected abstract void UpdateEntities(List<T> values);
        protected void UpdateEntities(List<T> values)
        {
            if (Entities == null) 
                Entities = new ObservableCollection<T>();
            foreach (var item in values)
            {
                Entities.Add(item);
            }
            RecordCount = _entities.Count;
            isRefreshing = false;
        }

        [RelayCommand]
        protected abstract Task<ColumnCollection> SetGridCols();
        // public abstract void Setup(Page page, RecordListingView rlv);
        public async void Setup(Page page, RecordListingView rlv)
        {
            CurrentPage = page;
            Rlv = rlv;
            Rlv.BindingContext = this;
            Rlv.Cols = await SetGridCols();
            

        }
        #endregion Abstractfunctions
    }

    [ObservableRecipient]
    public abstract partial class BaseDashoardViewModel<T> : ObservableObject
    {
        public const string Descending = "Descending";
        public const string Ascending = "Ascending";

        protected DashboardDataModel DataModel;

        [ObservableProperty]
        protected Page _currentPage;

        [ObservableProperty]
        protected string _defaultSortedColName;

        [ObservableProperty]
        protected string _defaultSortedOrder = Descending;

        [ObservableProperty]
        protected string _icon;

        [ObservableProperty]
        protected string _title;

        [ObservableProperty]
        protected T _entity;

        [ObservableProperty]
        protected UserType _role;

        public DashboardDataModel GetDataModel() => DataModel;
    }

    [ObservableRecipient]
    public abstract partial class BaseEntryViewModel<T, DM> : ObservableValidator
    {
        protected DM DataModel;

        [ObservableProperty]
        protected string _title;

        [ObservableProperty]
        protected string _icon;

        [ObservableProperty]
        protected bool _isNew;

        [ObservableProperty]
        protected UserType _role;

        [RelayCommand]
        protected abstract void Save();

        [RelayCommand]
        protected abstract void Cancle();

        [RelayCommand]
        protected abstract void InitViewModel();
    }
}