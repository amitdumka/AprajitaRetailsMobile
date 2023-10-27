using AprajitaRetails.Mobile.FormEntry.Models;
using AprajitaRetails.Mobile.RemoteServices;
using AprajitaRetails.Shared.ViewModels;

namespace AprajitaRetails.Mobile.FormEntry.ViewModels
{
    [ObservableRecipient]
    public partial class BaseEntryViewModel<T> : ObservableObject, INotifyPropertyChanged
    {
        [ObservableProperty]
        private bool isReady;

        [ObservableProperty]
        private T entity;

        [ObservableProperty]
        private static List<SelectOption> stores;

        [ObservableProperty]
        private static List<SelectOption> employees;
        [ObservableProperty]
        private string headerText;

        public BaseEntryViewModel()
        {
            SetupBasicComboBoxField();
            HeaderText = nameof(T);
        }

        private async void SetupBasicComboBoxField()
        {
            if (Stores == null)
                Stores = await GetStoreListAsync();
            if (Employees == null)
                Employees = await GetEmployeeListAsync(CurrentSession.StoreCode);
        }

        private async Task<List<SelectOption>> GetStoreListAsync()
        {
            List<SelectOption> details = await RestService.GetStoreListAsync();
            await Task.Delay(500);
            return details;
        }

        private async Task<List<SelectOption>> GetEmployeeListAsync(string sc)
        {
            List<SelectOption> details = await RestService.GetEmployeeListAsync(CurrentSession.StoreCode);
            await Task.Delay(500);

            return details;
        }

    }



     


}
