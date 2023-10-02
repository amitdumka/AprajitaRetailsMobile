using Syncfusion.Maui.DataForm;
using System.ComponentModel.DataAnnotations;

namespace AprajitaRetails.Mobile.ModelView
{
    public class AttendanceMV
    {
        [ReadOnly(true)]
        [Editable(false)]
        [Display(AutoGenerateField = false)]
        public string AttendanceId { get; set; }

       // [Display(Name = "Employee")]
        [Required(ErrorMessage = "Please select Employee")]
        public string EmployeeId { get; set; }

        [Display(GroupName = "Date Time")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please select Date")]
        public DateTime OnDate { get; set; }

        [Display(GroupName = "Date Time")]
        [DataFormDateRange(MinimumDate = "17/02/2016", ErrorMessage = "Attendance cannot be beyond 16/Feb/2016, date is invalid")]
        [DataType(DataType.Time)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Entry Time should not be empty")]
        public string? EntryTime { get; set; }

        [Required(ErrorMessage = "Please select Status")]
        public AttUnit Status { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Remarks is requried")]
        public string? Remarks { get; set; }

        [Display(Name = "Tailor")]
        public bool IsTailoring { get; set; }

        //[Display(Name = "Store")]
        public string StoreId { get; set; }
    }


}
