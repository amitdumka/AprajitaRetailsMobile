namespace AprajitaRetails.Mobile.Pages.Payroll
{
    using AprajitaRetails.Mobile.ViewModels.List.Payroll;
    public partial class EmployeePage : ContentPage
    {
        EmployeesViewModel viewModel;

        public EmployeePage(EmployeesViewModel vm)
        {
            InitializeComponent();
            BindingContext = viewModel = vm;
            viewModel.Setup(this, RLV);
        }
    }
}
