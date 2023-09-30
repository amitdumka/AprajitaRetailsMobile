using AprajitaRetails.Mobile.ViewModels.List.Payroll;

namespace AprajitaRetails.Mobile.Pages.Payroll
{
    public partial class AttendancePage : ContentPage
    {
        AttendanceViewModel viewModel;

        public AttendancePage(AttendanceViewModel vm)
        {
            InitializeComponent();
            BindingContext = viewModel = vm;
            viewModel.Setup(this, RLV);
        }
    }
}
