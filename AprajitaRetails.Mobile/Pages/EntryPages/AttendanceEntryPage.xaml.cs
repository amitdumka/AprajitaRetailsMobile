using AprajitaRetails.Mobile.RemoteServices;
using AprajitaRetails.Shared.ViewModels;
using Syncfusion.Maui.DataForm;

namespace AprajitaRetails.Mobile.Pages.EntryPages
{
    public partial class AttendanceEntryPage : ContentPage
    {
        public AttendanceEntryPage()
        {
            InitializeComponent();
            // this.dataForm.ItemsSourceProvider = new StoresSourceProvider();
            // this.dataForm.RegisterEditor("StoreId", DataFormEditorType.ComboBox);
            this.dataForm.RegisterEditor("StoreId", DataFormEditorType.ComboBox);
            this.dataForm.RegisterEditor("EmployeeId", DataFormEditorType.ComboBox);
            this.dataForm.RegisterEditor("IsTailoring", DataFormEditorType.Switch);
            this.dataForm.GenerateDataFormItem += OnGenerateDataFormItem;

        }
        private async void OnGenerateDataFormItem(object sender, GenerateDataFormItemEventArgs e)
        {
            if (e.DataFormItem != null && e.DataFormItem.FieldName == "StoreId" && e.DataFormItem is DataFormComboBoxItem comboBoxItem)
            {
               
                comboBoxItem.DisplayMemberPath = "Value";
                comboBoxItem.SelectedValuePath = "ID";
                var result = await RestService.GetStoreListAsync();
                comboBoxItem.ItemsSource = result;
            }
            if (e.DataFormItem != null && e.DataFormItem.FieldName == "EmployeeId" && e.DataFormItem is DataFormComboBoxItem cbEmp)
            {

                cbEmp.DisplayMemberPath = "Value";
                cbEmp.SelectedValuePath = "ID";
                var result = await RestService.GetEmployeeListAsync(CurrentSession.StoreCode);
                cbEmp.ItemsSource = result;
            }

        }
    }
    public class StoresSourceProvider : IDataFormSourceProvider
    {

        public object GetSource(string sourceName)
        {

            if (sourceName == "StoreId")
            {
                var result= RestService.GetStoreListAsync();

                result.Wait();
                if(result.IsCompletedSuccessfully == true)
                return result.Result;
            }

            return new List<string>();
        }
    }
}
namespace AprajitaRetails.Mobile.ViewModels.EntryPages
{ 
    public class AttendanceEntryiewModel
    {
        public AttendanceDTO info { get; set; }

        public AttendanceEntryiewModel()
        {
            this.info = new AttendanceDTO();
        }
    }

    //TODO: For Entry , One ModelView is need to Created for Each Entry Form
}
