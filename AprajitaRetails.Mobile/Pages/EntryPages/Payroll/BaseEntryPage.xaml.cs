using Microsoft.Identity.Client;

namespace AprajitaRetails.Mobile.Pages.EntryPages.Payroll
{
    public partial class BaseEntryPage : ContentPage
    {
         
        public BaseEntryPage()
        {
            Notify.NotifyVShort("1");
            InitializeComponent();
            Notify.NotifyVShort("2");
            Atvm attendanceEntry = new Atvm();
            Notify.NotifyVShort("3");
            this .BindingContext = attendanceEntry;
            Notify.NotifyVShort("4");
            this.Behaviors.Add(new AttendanceBehvior());
            Notify.NotifyVShort("5");



        }

         
       partial class Atvm:FormVilewModel<AttendanceEntry>
        { 


            public Atvm():base()
            {
                _entity = new AttendanceEntry();
                _formTitle = "Attendance";
                _displayImage = "thearvindstore004.jpg";
                _formSubTitle = "HR Module for managing Attendances!";
            }
        }

        //[INotifyPropertyChanged]
         partial class FormVilewModel<T>:ObservableObject
        {
            [ObservableProperty]
            protected T _entity;//{ get; set; }
            [ObservableProperty]
            protected string _formTitle;// { get; set; }   
            [ObservableProperty]
            protected string _formSubTitle;
            [ObservableProperty]
            protected string _displayImage;


            public FormVilewModel( )
            {
                //_entity = enty;
               // _formTitle = "Attendance";
                //_displayImage = "thearvindstore004.jpg";
               // _formSubTitle = "HR Module for managing Attendances!";
            }
        }
    }
}
