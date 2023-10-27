using AprajitaRetails.Mobile.DataModels.Payroll;
using AprajitaRetails.Mobile.FormEntry.Behviours;
using AprajitaRetails.Mobile.FormEntry.Models;
using AprajitaRetails.Mobile.RemoteServices;
using AprajitaRetails.Shared.ViewModels;
using Microsoft.Maui.Platform;
using Syncfusion.Maui.Core.Internals;
using Syncfusion.Maui.DataForm;
using System.ComponentModel.DataAnnotations;

namespace AprajitaRetails.Mobile.Pages.EntryPages.Payroll
{
    public partial class APage : ContentPage
    {



        public APage()
        {
            InitializeComponent();
            //string basePath = "AprajitaRetails.Mobile.Pdf.Invoice.pdf";
            //// if (BaseConfig.IsIndividualSB)
            ////   basePath = "SampleBrowser.Maui.PdfViewer.Samples.Pdf.";
            //viewModel.DocumentStream = this.GetType().Assembly.GetManifestResourceStream(basePath);
            //cbx.SelectedValuePath = "ID";
            //cbx.DisplayMemberPath = "Value";
            entryView.DataForm.BindingContext = viewModel;
            entryView.DataForm.Behaviors.Add(new AttendanceEntryFormBehavior());
        }

        //private void MainPage_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)

        //{
        //    if (!string.IsNullOrEmpty(e.PropertyName))

        //    {
        //        dataForm.UpdateEditor(e.PropertyName);
        //    }
        //}

        private string old1 = "ARD";
        private string old2 = "ARD-2016-SM-1";

        private void PrimaryButton_Clicked(object sender, EventArgs e)
        {
            var x = entryView.DataForm.DataObject as AttendanceEM;
            if (x.StoreId != null && x.EmployeeId != null)
            {
                Notify.NotifyVShort(x.StoreId);
                Notify.NotifyVShort(x.EmployeeId);
            }
            else { Notify.NotifyVShort("Values are null"); }
            var x1 = x.StoreId;
            var x2 = x.EmployeeId;

            viewModel.Entity.StoreId = old1;
            viewModel.Entity.EmployeeId = old2;
            entryView.DataForm.DataObject = viewModel.Entity;
            entryView.DataForm.UpdateEditor("StoreId");
            entryView.DataForm.UpdateEditor("EmployeeId");

            old1 = x1;
            old2 = x2;

            //dataForm.DataObject = viewModel.Attendance;
        }

        private async void SecondaryButton_Clicked(object sender, EventArgs e)
        {
            //var xx = await this.signaturePad.GetStreamAsync(Syncfusion.Maui.Core.ImageFileFormat.Png);
            //TODO: Save File using IFilesave singleton object in .net maui comunity tool kit
            //Notify.NotifyLong(xx.ToString());
            //string fileName = "";
            //string basePath = "AprajitaRetails.Mobile.Pdf.Invoice.pdf";
            // if (BaseConfig.IsIndividualSB)
            //   basePath = "SampleBrowser.Maui.PdfViewer.Samples.Pdf.";
            // viewModel.DocumentStream = this.GetType().Assembly.GetManifestResourceStream(basePath );
            // var xxs= this.GetType().Assembly.GetManifestResourceStream(basePath);
            // Notify.NotifyShort((xxs != null)?""+xxs.Length:""+0) ;

            //Notify.NotifyLong();
            //AttendanceDataModel dataModel;

            //dataModel = new AttendanceDataModel
            //{
            //    Mode = DBType.API,
            //    StoreCode = CurrentSession.StoreCode
            //};
            //dataModel.Connect();

            //var list = (await dataModel.GetByStoreDTO(CurrentSession.StoreCode)).FirstOrDefault();

            //if (list != null)
            //{
            //    var x = new Attendance
            //    {
            //        AttendanceId = list.AttendanceId,
            //        EmployeeId = list.EmployeeId,
            //        EntryTime = list.EntryTime,
            //        IsTailoring = list.IsTailoring,
            //        OnDate = list.OnDate.Date,
            //        Remarks = list.Remarks + "TESTME",
            //        Status = list.Status,
            //        StoreId = list.StoreId
            //    };
            //    viewModel.Attendance = x;
            //    //dataForm.DataObject= viewModel.Attendance;
            //    dataForm.UpdateEditor("StoreId");
            //    dataForm.UpdateEditor("EmployeeId");
            //    //(dataForm.DataObject as Attendance).StoreId = list.StoreId;
            //    //(dataForm.DataObject as Attendance).EmployeeId = list.EmployeeId;
            //    dataForm.UpdateEditor("StoreId");
            //    dataForm.UpdateEditor("EmployeeId");
            //}
            //else Notify.NotifyVShort("list is null");
            ////await Task.Delay(500);
        }
    }

    //[ObservableRecipient]
    //public partial class AttendanceDFViewModel : ObservableObject, INotifyPropertyChanged
    //{
    //    [ObservableProperty]
    //    private Stream? _documentStream;
    //    [ObservableProperty]
    //    private bool isReady;

    //    [ObservableProperty]
    //    private List<SelectOption> stores;

    //    [ObservableProperty]
    //    private List<SelectOption> employees;

    //    [ObservableProperty]
    //    private List<string> employeeId;

    //    [ObservableProperty]
    //    private List<string> employeeValue;

    //    [ObservableProperty]
    //    private List<string> storeId;

    //    [ObservableProperty]
    //    private List<string> storeValue;

    //    [ObservableProperty]
    //    private Attendance attendance;//{ get; set; }
    //    [ObservableProperty]
    //    private ComboBoxOptionList comboBoxOptions;

    //    //public Attendance Attendance { get; set; }
    //    public AttendanceDFViewModel()
    //    {

    //        //LoadDataSources();
    //        Attendance = new Attendance();
    //        SetFormValue();
    //    }

    //    private async void LoadDataSources()
    //    {
    //        StoreId = new List<string>();
    //        StoreValue = new List<string>();
    //        EmployeeId = new List<string>(); EmployeeValue = new List<string>();
    //        // if (Stores == null || !Stores.Any())
    //        //Employees= await RestService.GetEmployeeListAsync(CurrentSession.StoreCode);
    //        //Stores=await RestService.GetStoreListAsync();
    //        Stores = await GetStoreListAsync();
    //        Employees = await GetEmployeeListAsync(CurrentSession.StoreCode);

    //        //Stores = await GetStoreListAsync();
    //        // if (Employees == null || !Employees.Any())
    //        //Employees = await GetEmployeeListAsync(CurrentSession.StoreCode);
    //    }

    //    private async Task<List<SelectOption>> GetStoreListAsync()
    //    {
    //        var stores = "[{\"ID\":\"ARD\",\"Value\":\"Aprajita Retails, #: Dumka\"},{\"ID\":\"ARJ\",\"Value\":\"Aprajita Retails, Jamshedpur, #: Jamshedpur\"},{\"ID\":\"JKD\",\"Value\":\"Aprajita Retails(Jockey EBO), #: Dumka\"}]";

    //        List<SelectOption> storeDetails = JsonSerializer.Deserialize<List<SelectOption>>(stores);
    //        List<SelectOption> sd = new List<SelectOption>();

    //        foreach (var item in storeDetails)
    //        {
    //            StoreId.Add(item.ID.Trim().ToString());
    //            StoreValue.Add(item.Value.ToString().Trim());
    //        }

    //        await Task.Delay(500);

    //        return sd;
    //    }

    //    private async Task<List<SelectOption>> GetEmployeeListAsync(string sc)
    //    {
    //        var emp = "[{\"ID\":\"ARD-2023-HK\",\"Value\":\"Keli Devi\"},{\"ID\":\"ARD-2016-SM-1\",\"Value\":\"Alok Kumar\"},{\"ID\":\"ARD-2020-ACC-11\",\"Value\":\"Geetanjali Kumari Verma\"}]";

    //        List<SelectOption> storeDetails = JsonSerializer.Deserialize<List<SelectOption>>(emp);
    //        List<SelectOption> sd = new List<SelectOption>();
    //        foreach (var item in storeDetails)
    //        {
    //            EmployeeId.Add(item.ID.Trim().ToString());
    //            EmployeeValue.Add(item.Value.ToString().Trim());
    //        }

    //        await Task.Delay(500);

    //        return sd;
    //    }

    //    private async void SetFormValue()
    //    {
    //        if (ComboBoxOptions == null)
    //        {
    //            ComboBoxOptions = new ComboBoxOptionList();
    //            await Task.Delay(500);
    //        }
    //        do
    //        {
    //            if (ComboBoxOptions.IsReady && !IsReady)
    //            {
    //                Attendance = new Attendance
    //                {
    //                    StoreId = ComboBoxOptions.StoreValue.ElementAt(ComboBoxOptions.StoreId.IndexOf(Attendance.StoreId)),
    //                    EmployeeId = ComboBoxOptions.EmployeeValue.ElementAt(ComboBoxOptions.EmployeeId.IndexOf(Attendance.EmployeeId))
    //                };
    //                IsReady = true;
    //            }
    //            else
    //            {
    //                await Task.Delay(500);
    //            }
    //        }
    //        while (!IsReady);
    //        //while (StoreId == null && !StoreId.Any() && EmployeeId == null && !EmployeeId.Any());
    //    }

    //    private async void SetFormValue(Attendance att)
    //    {
    //        do
    //        {
    //            if (StoreId != null && StoreId.Any() && EmployeeId != null && EmployeeId.Any() && !IsReady)
    //            {
    //                Attendance = new Attendance
    //                {
    //                    StoreId = StoreValue.ElementAt(StoreId.IndexOf(att.StoreId)),
    //                    EmployeeId = EmployeeValue.ElementAt(EmployeeId.IndexOf(att.StoreId))
    //                };
    //                IsReady = true;
    //            }
    //            else
    //            {
    //                await Task.Delay(500);
    //            }
    //        }
    //        while (StoreId == null && !StoreId.Any() && EmployeeId == null && !EmployeeId.Any());
    //    }
    //}

    //[INotifyPropertyChanged]
    //public partial class Attendance
    //{
    //    public Attendance()
    //    {
    //        this.AttendanceId = string.Empty;
    //        this.EntryTime = DateTime.Now.ToShortTimeString();
    //        this.Status = AttUnit.SundayHoliday;
    //        this.Remarks = string.Empty;
    //        this.StoreId = "ARD";
    //        this.EmployeeId = "ARD-2016-SM-1";
    //        this.OnDate = DateTime.Now;
    //    }

    //    // [Required(ErrorMessage = "Please select Store")]
    //    [ObservableProperty]
    //    private string storeId;

    //    //public string StoreId { get; set; }

    //    [ReadOnly(true)]
    //    [Editable(false)]
    //    [Display(AutoGenerateField = false)]
    //    [ObservableProperty]
    //    private string attendanceId;

    //    //public string AttendanceId { get; set; }

    //    //[Required(ErrorMessage = "Please select Employee")]
    //    [ObservableProperty]
    //    private string employeeId;

    //    //public event PropertyChangedEventHandler PropertyChanged;

    //    //public string EmployeeId { get; set; }

    //    [Display(GroupName = "Date Time", Name = "Date")]
    //    //[DataFormDateRange(MinimumDate = "17/02/2016", ErrorMessage = "Attendance cannot be beyond 16/Feb/2016, date is invalid")]
    //    //[DataType(DataType.Date)]
    //    //[Required(ErrorMessage = "Please select Date")]
    //    [ObservableProperty]
    //    private DateTime onDate;

    //    //public DateTime OnDate { get; set; }

    //    [Display(GroupName = "Date Time")]
    //    //[DataType(DataType.Time)]
    //    // [Required(AllowEmptyStrings = false, ErrorMessage = "Entry Time should not be empty")]
    //    //[DataFormValueConverter(typeof(StringToTimeConverter))]
    //    [ObservableProperty]
    //    private string entryTime;

    //    //public string EntryTime { get; set; }

    //    [Display(Name = "Attndance")]
    //    //[Required(ErrorMessage = "Please select Attendance status")]
    //    [ObservableProperty]
    //    private AttUnit status;

    //    //public AttUnit Status { get; set; }

    //    // [Required(AllowEmptyStrings = false, ErrorMessage = "Remarks is requried")]
    //    [ObservableProperty]
    //    private string? remarks;

    //    //public string? Remarks { get; set; }

    //    [Display(Name = "Tailor")]
    //    [ObservableProperty]
    //    private bool isTailoring;

    //    //public bool IsTailoring { get; set; }
    //}

    //public class AttendanceFormBehvour : Behavior<SfDataForm>
    //{
    //    private Attendance Attendance { get; set; }
    //    private SfDataForm DataForm { get; set; }
    //    private AttendanceDFViewModel viewModel;

    //    protected override async void OnAttachedTo(SfDataForm dataForm)
    //    {
    //        base.OnAttachedTo(dataForm);
    //        // dataForm = bindable.Content.FindByName<SfDataForm>("dataForm");

    //        if (dataForm != null)
    //        {
    //            DataForm = dataForm;
    //            dataForm.ColumnCount = 2;
    //            // dataForm.ItemsSourceProvider = new ItemSourceProvider2();

    //            dataForm.RegisterEditor(nameof(Attendance.EmployeeId), DataFormEditorType.ComboBox);
    //            dataForm.RegisterEditor(nameof(Attendance.StoreId), DataFormEditorType.ComboBox);
    //            dataForm.RegisterEditor("IsTailoring", DataFormEditorType.Switch);
    //            //dataForm.GenerateDataFormItem += this.OnGenerateDataFormItem;
    //            viewModel = DataForm.BindingContext as AttendanceDFViewModel;
    //            dataForm.Commit();
    //            dataForm.GenerateDataFormItem += OnGenerateDataFormItem;
    //            // (dataForm.DataObject as Attendance).PropertyChanged += OnDataObjectPropertyChanged;

    //            //await WorkArroundForComboBoxLoad();
    //        }
    //    }

    //    private async Task WorkArroundForComboBoxLoad()
    //    {
    //        Attendance = new Attendance
    //        {
    //            AttendanceId = "11",

    //            EntryTime = DateTime.Now.ToShortTimeString(),
    //            Status = AttUnit.SundayHoliday,
    //            Remarks = "10:30 PM",
    //            StoreId = "ARD",
    //            EmployeeId = "ARD-2016-SM-3",
    //            OnDate = DateTime.Now.AddDays(5),
    //        };
    //        if (viewModel.Stores != null && viewModel.Employees != null && viewModel.Stores.Any() && viewModel.Employees.Any())
    //        {
    //            viewModel.Attendance = Attendance;
    //            DataForm.DataObject = Attendance;
    //        }
    //        else
    //        {
    //            await Task.Delay(10000);
    //            viewModel.Attendance = Attendance;
    //            DataForm.DataObject = Attendance;
    //        }
    //    }

    //    private void OnGenerateDataFormItem(object sender, GenerateDataFormItemEventArgs e)
    //    {
    //        if (e.DataFormItem != null && (e.DataFormItem.FieldName == "StoreId" || e.DataFormItem.FieldName == "Store") && e.DataFormItem is DataFormComboBoxItem comboBoxItem)
    //        {
    //            // e.DataFormItem.LabelText = "Store";
    //            //comboBoxItem.DisplayMemberPath = "Value";
    //            //comboBoxItem.SelectedValuePath = "ID";
    //            //comboBoxItem.IsEditable = true;
    //            // comboBoxItem.FieldName = "cbStoreId";
    //            // comboBoxItem.PlaceholderText = "Select Store";

    //            var viewModel = DataForm.BindingContext as AttendanceDFViewModel;
    //            comboBoxItem.BindingContext = viewModel;
    //            comboBoxItem.SetBinding(DataFormComboBoxItem.ItemsSourceProperty, nameof(viewModel.StoreValue), BindingMode.TwoWay);
    //        }

    //        if (e.DataFormItem != null && (e.DataFormItem.FieldName == "EmployeeId" || e.DataFormItem.FieldName == "Employee") && e.DataFormItem is DataFormComboBoxItem cbEmp)
    //        {
    //            // e.DataFormItem.LabelText = "Employee";
    //            //cbEmp.DisplayMemberPath = "Value";
    //            //cbEmp.SelectedValuePath = "ID";
    //            // cbEmp.IsEditable = true;

    //            var viewModel = DataForm.BindingContext as AttendanceDFViewModel;
    //            cbEmp.BindingContext = viewModel;
    //            cbEmp.SetBinding(DataFormComboBoxItem.ItemsSourceProperty, nameof(viewModel.EmployeeValue), BindingMode.TwoWay);

    //            //Notify.NotifyVShort(viewModel.Employees.First().ID);
    //        }

    //        if (e.DataFormItem != null)
    //        {
    //            if (e.DataFormItem.FieldName == "IsTailoring")
    //            {
    //                e.DataFormItem.IsVisible = false;
    //            }
    //        }
    //    }

    //    private void OnDataObjectPropertyChanged(object sender, PropertyChangedEventArgs e)

    //    {
    //        if (DataForm != null && !string.IsNullOrEmpty(e.PropertyName))

    //        {
    //            DataForm.UpdateEditor(e.PropertyName);
    //        }
    //    }

    //    protected override void OnDetachingFrom(SfDataForm dataForm)
    //    {
    //        base.OnDetachingFrom(dataForm);

    //        if (dataForm != null)
    //        {
    //            dataForm.GenerateDataFormItem -= this.OnGenerateDataFormItem;
    //            // (dataForm.DataObject as Attendance).PropertyChanged -= OnDataObjectPropertyChanged;
    //        }
    //    }
    //}



    ///// <summary>
    ///// Make this a singleton so It can instantisate once and work ever time
    ///// </summary>
    //public partial class ComboBoxOptionList : ObservableObject, INotifyPropertyChanged
    //{
    //    [ObservableProperty]
    //    private static bool isReady;

    //    //[ObservableProperty]
    //    //private List<SelectOption> stores;

    //    //[ObservableProperty]
    //    //private List<SelectOption> employees;
    //    [ObservableProperty]
    //    private static List<string> employeeId;

    //    [ObservableProperty]
    //    private static List<string> employeeValue;

    //    [ObservableProperty]
    //    private static List<string> storeId;

    //    [ObservableProperty]
    //    private static List<string> storeValue;

    //    public ComboBoxOptionList()
    //    { OnInit(); }

    //    private async void OnInit()
    //    {
    //        storeId = new List<string>();
    //        storeValue = new List<string>();
    //        employeeId = new List<string>(); 
    //        employeeValue = new List<string>();

    //        var x = await GetStoreListAsync();
    //        var y = await GetEmployeeListAsync(CurrentSession.StoreCode);
    //        SetStore();
    //    }
    //    private async void SetStore()
    //    {
    //        do
    //        {
    //            if (StoreId != null && StoreId.Any() && EmployeeId != null && EmployeeId.Any() && !IsReady)
    //            {

    //                IsReady = true;
    //            }
    //            else
    //            {
    //                await Task.Delay(500);
    //            }
    //        }
    //        while (StoreId == null && !StoreId.Any() && EmployeeId == null && !EmployeeId.Any());
    //    }
    //    private void LoadLegders()
    //    {
    //    }

    //    private async Task<List<SelectOption>> GetStoreListAsync()
    //    {
    //        List<SelectOption> storeDetails = await RestService.GetStoreListAsync();
    //        foreach (var item in storeDetails)
    //        {
    //            StoreId.Add(item.ID.Trim().ToString());
    //            StoreValue.Add(item.Value.ToString().Trim());
    //        }
    //        await Task.Delay(500);
    //        return storeDetails;
    //    }

    //    private async Task<List<SelectOption>> GetEmployeeListAsync(string sc)
    //    {
    //        List<SelectOption> storeDetails = await RestService.GetEmployeeListAsync(CurrentSession.StoreCode);

    //        foreach (var item in storeDetails)
    //        {
    //            EmployeeId.Add(item.ID.Trim().ToString());
    //            EmployeeValue.Add(item.Value.ToString().Trim());
    //        }

    //        await Task.Delay(500);

    //        return storeDetails;
    //    }
    //}



}