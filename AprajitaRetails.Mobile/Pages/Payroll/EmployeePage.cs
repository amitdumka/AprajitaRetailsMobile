using AprajitaRetails.Mobile.ViewModels.List.Payroll;
using AprajitaRetails.Mobile.Views.Custom;

namespace AprajitaRetails.Mobile.Pages.Payroll
{
    public class EmployeePage : ListPage
    {
        public EmployeePage(EmployeesViewModel vm)
        {
            BindingContext = vm;
            vm.Setup(this, rlv);
            tbRefesh.Command = vm.RefreshButtonCommand;
            this.tbDelete.Command = vm.DeleteButtonCommand;
            this.tbAdd.Command = vm.AddButtonCommand;

        }

    }
}
