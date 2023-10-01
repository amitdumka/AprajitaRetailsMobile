using AprajitaRetails.Mobile.ViewModels.List.Payroll;

namespace AprajitaRetails.Mobile.Pages.Payroll
{
    public partial class AttendancePage_old : ContentPage
    {
        AttendanceViewModel viewModel;

        public AttendancePage_old(AttendanceViewModel vm)
        {
            InitializeComponent();
            BindingContext = viewModel = vm;
            viewModel.Setup(this, RLV);
        }
    }
}
