using AprajitaRetails.Mobile.ViewModels.List.Accounting;
using AprajitaRetails.Mobile.Views.Custom;

namespace AprajitaRetails.Mobile.Pages.Accounting
{
    public class VoucherPage : ListPage
    {
        public VoucherPage(VoucherViewModel vm)
        {

            BindingContext = vm;
            vm.Setup(this, rlv);
            tbRefesh.Command = vm.RefreshButtonCommand;
            this.tbDelete.Command = vm.DeleteButtonCommand;
            this.tbAdd.Command = vm.AddButtonCommand;

        }

    }
}
