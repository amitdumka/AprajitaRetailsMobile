using AprajitaRetails.Mobile.ViewModels.List.Payroll;
using AprajitaRetails.Mobile.Views.Custom;

namespace AprajitaRetails.Mobile.Pages.Payroll
{
    public class AttendancePage : ListPage
    {
        public AttendancePage(AttendanceViewModel vm)
        {

            BindingContext = vm;
            vm.Setup(this, rlv);
            tbRefesh.Command = vm.RefreshButtonCommand;
            this.tbDelete.Command = vm.DeleteButtonCommand;
            this.tbAdd.Command = vm.AddButtonCommand;

        }

    }
}
