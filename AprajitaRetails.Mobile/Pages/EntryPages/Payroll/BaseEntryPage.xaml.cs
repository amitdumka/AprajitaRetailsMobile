using Microsoft.Identity.Client;

namespace AprajitaRetails.Mobile.Pages.EntryPages.Payroll
{
    public partial class BaseEntryPage : ContentPage
    {
         
        public BaseEntryPage()
        {
            
            InitializeComponent();
            Atvm attendanceEntry = new Atvm();
            this.BindingContext = attendanceEntry;
            this.Behaviors.Add(new AttendanceBehvior());
        }

        [INotifyPropertyChanged]
        partial class Atvm
        {
          
            [ObservableProperty]
            protected string _formTitle;// { get; set; }   
            [ObservableProperty]
            protected string _formSubTitle;
            [ObservableProperty]
            protected string _displayImage;
            [ObservableProperty]
            protected string _primaryButtonText;

            public Atvm()
            {
                //_entity = new AttendanceEntry();
                _formTitle = "Attendance";
                _displayImage = "thearvindstore004.jpg";
                _formSubTitle = "HR Module for managing Attendances!";
                _primaryButtonText = "Save Attendance";
            }
        }

        [INotifyPropertyChanged]
         partial class FormVilewModel<T>
        {
            //[ObservableProperty]
            //protected T _entity;//{ get; set; }
            [ObservableProperty]
            protected string _formTitle;// { get; set; }   
            [ObservableProperty]
            protected string _formSubTitle;
            [ObservableProperty]
            protected string _displayImage;


            //public FormVilewModel( )
            //{
                //_entity = enty;
               // _formTitle = "Attendance";
                //_displayImage = "thearvindstore004.jpg";
               // _formSubTitle = "HR Module for managing Attendances!";
            //}
        }
    }
}
