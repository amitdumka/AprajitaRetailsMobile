namespace AprajitaRetails.Mobile.Pages.EntryPages.Payroll
{
    public partial class APage : ContentPage
    {
        AttVM vm = new AttVM();
        public APage()
        {
            InitializeComponent();

            vm.AttendanceEntry = new AttendanceEntry
            {
                AttendanceId = string.Empty,
                EntryTime = DateTime.Now.ToShortTimeString(),
                Status = AttUnit.SundayHoliday,
                Remarks = string.Empty,
                StoreId = "ARJ",
                EmployeeId = "ARD-2016-SM-1",
                OnDate = DateTime.Now.AddDays(10),
            };

            this.BindingContext = vm;


        }
    }
}
