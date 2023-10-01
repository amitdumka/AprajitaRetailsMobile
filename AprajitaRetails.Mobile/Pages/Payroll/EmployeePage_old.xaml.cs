namespace AprajitaRetails.Mobile.Pages.Payroll
{
    using AprajitaRetails.Mobile.ViewModels.List.Payroll;
    public partial class EmployeePage_old : ContentPage
    {
        EmployeesViewModel viewModel;

        public EmployeePage_old(EmployeesViewModel vm)
        {
            InitializeComponent();
            BindingContext = viewModel = vm;
            viewModel.Setup(this, RLV);
        }
    }
}
