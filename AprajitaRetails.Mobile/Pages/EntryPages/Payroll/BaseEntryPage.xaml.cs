namespace AprajitaRetails.Mobile.Pages.EntryPages.Payroll
{
    public partial class BaseEntryPage : ContentPage
    {
        public BaseEntryPage()
        {
            InitializeComponent();
            AttendanceEntry attendanceEntry = new AttendanceEntry();
            this .BindingContext = attendanceEntry;
            this.Behaviors.Add(new AttendanceBehvior());

        }
    }
}
