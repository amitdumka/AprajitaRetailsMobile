using AprajitaRetails.Mobile.FormEntry.Behviours;
using AprajitaRetails.Mobile.FormEntry.Models;
using AprajitaRetails.Mobile.FormEntry.ViewModels;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace AprajitaRetails.Mobile.FormEntry.Views;


public class CashVoucherEntryPage : EntryPage<CashVoucherEM, CashVoucherEntryViewModel, CashVoucherEntryFormBehavior>
{
    public CashVoucherEntryPage()
    {
        viewModel = new CashVoucherEntryViewModel();
        bhv = new CashVoucherEntryFormBehavior();
        this.Title = viewModel.HeaderText;
        this.BindingContext = viewModel;
        this.Behaviors.Add(bhv);
        entryView.BindingContext = viewModel;
    }
}
public class VoucherEntryPage : EntryPage<VoucherEM, VoucherEntryViewModel, VoucherEntryFormBehavior>
{
    public VoucherEntryPage()
    {
        viewModel = new VoucherEntryViewModel();
        bhv = new VoucherEntryFormBehavior();
        this.Title = viewModel.HeaderText;
        this.BindingContext = viewModel;
        this.Behaviors.Add(bhv);
        entryView.BindingContext = viewModel;
    }
}

public class NoteEntryPage : EntryPage<NoteEM, NoteEntryViewModel, NoteEntryFormBehavior>
{
    public NoteEntryPage()
    {
        viewModel = new NoteEntryViewModel();
        bhv = new NoteEntryFormBehavior();
        this.Title = viewModel.HeaderText;
        this.BindingContext = viewModel;
        this.Behaviors.Add(bhv);
        entryView.BindingContext = viewModel;
    }
}



public class AttendanceEntryPage : EntryPage<AttendanceEM, AttendanceEntryViewModel, AttendanceEntryFormBehavior>
{

    public AttendanceEntryPage(Attendance obj)
    {
        viewModel.Entity = new AttendanceEM { AttendanceId = obj.AttendanceId , EmployeeId=obj.EmployeeId, EntryTime=obj.EntryTime, OnDate=obj.OnDate, Remarks=obj.Remarks, Status=obj.Status, StoreId=obj.StoreId};
    }
    public AttendanceEntryPage()
    {
        viewModel = new AttendanceEntryViewModel();
        bhv = new AttendanceEntryFormBehavior();
        this.Title = viewModel.HeaderText;
        this.BindingContext = viewModel;
        this.Behaviors.Add(bhv);
        entryView.BindingContext = viewModel;
    }
}

public class EmployeeEntryPage : EntryPage<EmployeeEM, EmployeeEntryViewModel, EmployeeEntryFormBehavior>
{
    public EmployeeEntryPage()
    {
        viewModel = new EmployeeEntryViewModel();
        bhv = new EmployeeEntryFormBehavior();
        this.Title = viewModel.HeaderText;
        this.BindingContext = viewModel;
        this.Behaviors.Add(bhv);
        entryView.BindingContext = viewModel;
    }
}

public class EntryPage<T, VM, B> : ContentPage where B : BaseEntryBehavior<T, VM>
{
    public BaseEntryView entryView;
    protected B bhv;
    protected VM viewModel;

     

    public EntryPage()
    {
        entryView = new BaseEntryView();
        entryView.DataForm.ColumnCount = 2;

        Content = new Grid
        {
            BackgroundColor = Colors.LightBlue,
            Children = { new Grid {
                RowDefinitions = Rows.Define((Row.FormEntry, Auto),(Row.ExtraEntry,Auto)),
                BackgroundColor=Colors.GhostWhite,
            Children={entryView}
            } }
        };
    }
}