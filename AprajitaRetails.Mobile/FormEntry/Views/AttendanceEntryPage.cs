
using AprajitaRetails.Mobile.FormEntry.Behviours;
using AprajitaRetails.Mobile.FormEntry.Models;
using AprajitaRetails.Mobile.FormEntry.ViewModels;

//using CommunityToolkit.Maui.Markup;
//using static CommunityToolkit.Maui.Markup.GridExtensions;

using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace AprajitaRetails.Mobile.FormEntry.Views;

public class AttendanceEntryPage : EntryPage<AttendanceEM, AttendanceEntryViewModel, AttendanceEntryFormBehavior>
{


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
public class EmployeeEntryPage:EntryPage<EmployeeEM, EmployeeEntryViewModel, EmployeeEntryFormBehavior>
{
    public EmployeeEntryPage()
    {
        viewModel= new EmployeeEntryViewModel();
        bhv = new EmployeeEntryFormBehavior();
        this.Title = viewModel.HeaderText;
        this.BindingContext = viewModel;
        this.Behaviors.Add(bhv);
        entryView.BindingContext = viewModel;
    }
}


public class EntryPage<T, VM, B> : ContentPage where B : BaseEntryBehavior<T, VM>
{
    protected B bhv;
    protected VM viewModel;

    public BaseEntryView entryView;

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
enum Row { FormEntry, ExtraEntry }
enum Column { Description, Input }