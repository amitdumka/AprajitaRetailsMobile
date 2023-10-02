using AprajitaRetails.Mobile.Behaviors;
using AprajitaRetails.Mobile.DataModels.Payroll;
using AprajitaRetails.Shared.ViewModels;
using Syncfusion.Maui.Buttons;
using Syncfusion.Maui.DataForm;

namespace AprajitaRetails.Mobile.Views.Controls.DataForm
{
    public partial  class BaseEntryPage : ContentPage
    {
        public BaseEntryPage()
        {
            InitializeComponent();
            
        }

        protected SfDataForm entryDataForm;
        protected SfButton PrimaryButton, SecondaryButton, CloseButton, CancleButton;
        protected string Title;
        

    }

    
    public abstract  class EntryPage<M, B, D> 
    {
        protected M Entity { get; set; }
        protected B entryBehavior;
        protected D dataModel;
        public EntryPage() { }
    }
}
