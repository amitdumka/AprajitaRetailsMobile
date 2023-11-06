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
    public partial class EmployeeEntryViewModel : BaseEntryViewModel<EmployeeEM>
    {

        public EmployeeEntryViewModel() : base()
        {
            HeaderText = "Employee";
            Entity = new EmployeeEM { StoreId = CurrentSession.StoreCode,  IsWorking = true, Gender = Gender.Male, BirthDate = DateTime.Now.AddYears(-18), Category = EmpType.Salesman, JoiningDate = DateTime.Now, City = string.IsNullOrEmpty(CurrentSession.CityName) ? "" : CurrentSession.CityName };
        }


    }


}
