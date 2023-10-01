using AprajitaRetails.Mobile.ViewModels.List.Accounting;

namespace AprajitaRetails.Mobile.Pages.Accounting
{
    public class CashVoucherPage : ListPage
    {
        public CashVoucherPage(CashVoucherViewModel vm)
        {

            BindingContext = vm;
            vm.Setup(this, rlv);
            tbRefesh.Command = vm.RefreshButtonCommand;
            this.tbDelete.Command = vm.DeleteButtonCommand;
            this.tbAdd.Command = vm.AddButtonCommand;

        }

    }
}
