using Syncfusion.Maui.DataForm;

namespace AprajitaRetails.Mobile.FormEntry.Views
{
    public partial class BaseEntryView : ContentView
    {
        public SfDataForm DataForm { get { return dataForm; } set { dataForm = value; } }
        public BaseEntryView()
        {
            InitializeComponent();
           
            
        }

        private void PrimaryButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}
