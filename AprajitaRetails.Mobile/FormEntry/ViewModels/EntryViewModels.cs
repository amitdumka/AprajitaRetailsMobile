﻿using AprajitaRetails.Mobile.FormEntry.Models;

namespace AprajitaRetails.Mobile.FormEntry.ViewModels
{
    public partial class AttendanceEntryViewModel : BaseEntryViewModel<AttendanceEM>
    {

        public AttendanceEntryViewModel() : base()
        {

            HeaderText = "Attendance";
            Entity = new AttendanceEM();
        }
         

    }
    public partial class EmployeeEntryViewModel : BaseEntryViewModel<EmployeeEM>
    {

        public EmployeeEntryViewModel() : base()
        {
            HeaderText= "Employee";
            Entity = new EmployeeEM();
        }


    }


}
