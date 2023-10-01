using AprajitaRetails.Mobile.ViewModels.List.Accounting.Banking;

namespace AprajitaRetails.Mobile.Pages.Banking
{
    public class BankPage : ListPage
    {
        public BankPage(BankViewModel vm)
        {

            BindingContext = vm;
            vm.Setup(this, rlv);
            tbRefesh.Command = vm.RefreshButtonCommand;
            this.tbDelete.Command = vm.DeleteButtonCommand;
            this.tbAdd.Command = vm.AddButtonCommand;

        }

    }
    public class BankAccountPage : ListPage
    {
        public BankAccountPage(BankAccountViewModel vm)
        {

            BindingContext = vm;
            vm.Setup(this, rlv);
            tbRefesh.Command = vm.RefreshButtonCommand;
            this.tbDelete.Command = vm.DeleteButtonCommand;
            this.tbAdd.Command = vm.AddButtonCommand;

        }

    }
    public class VendorAccountPage : ListPage
    {
        public VendorAccountPage(VendorAccountViewModel vm)
        {

            BindingContext = vm;
            vm.Setup(this, rlv);
            tbRefesh.Command = vm.RefreshButtonCommand;
            this.tbDelete.Command = vm.DeleteButtonCommand;
            this.tbAdd.Command = vm.AddButtonCommand;

        }

    }

    public class BankTranscationPage : ListPage
    {
        public BankTranscationPage(BankTransactionViewModel vm)
        {

            BindingContext = vm;
            vm.Setup(this, rlv);
            tbRefesh.Command = vm.RefreshButtonCommand;
            this.tbDelete.Command = vm.DeleteButtonCommand;
            this.tbAdd.Command = vm.AddButtonCommand;

        }

    }
}
