using AprajitaRetails.Mobile.FormEntry.Models;

namespace AprajitaRetails.Mobile.FormEntry.ViewModels
{
    public partial class AttendanceEntryViewModel : BaseEntryViewModel<AttendanceEM>
    {
        public AttendanceEntryViewModel() : base()
        {
            HeaderText = "Attendance";
            Entity = new AttendanceEM
            {
                OnDate = DateTime.Now,
                EmployeeId = "ARD-2016-SM-1",
                StoreId = CurrentSession.StoreCode,
                EntryTime = DateTime.Now.ToShortTimeString(),
                Remarks = "",
                Status = AttUnit.Absent
            };
        }
    }
}