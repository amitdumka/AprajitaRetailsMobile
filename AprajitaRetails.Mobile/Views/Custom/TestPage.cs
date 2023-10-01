using AprajitaRetails.Mobile.ViewModels.List.Payroll;


namespace AprajitaRetails.Mobile.Views.Custom
{
    public class TestPage : ListPage
    {
        public TestPage(EmployeesViewModel vm) {

            BindingContext=vm;
            vm.Setup(this, rlv);
            tbRefesh.Command = vm.RefreshButtonCommand;
            this.tbDelete.Command = vm.DeleteButtonCommand;
            this.tbAdd.Command = vm.AddButtonCommand;
        
        }
        
    }
}
