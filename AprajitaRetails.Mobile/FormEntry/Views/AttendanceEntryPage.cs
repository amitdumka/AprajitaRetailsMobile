
using AprajitaRetails.Mobile.FormEntry.Behviours;
using AprajitaRetails.Mobile.FormEntry.Models;
using AprajitaRetails.Mobile.FormEntry.ViewModels;

using CommunityToolkit.Maui.Markup;
using static CommunityToolkit.Maui.Markup.GridExtensions;

using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace AprajitaRetails.Mobile.FormEntry.Views;

public class AttendanceEntryPage : EntryPage<AttendanceEM, AttendanceEntryViewModel, AttendanceEntryFormBehavior>
{


    public AttendanceEntryPage()
    {
        entryView = new BaseEntryView();


        viewModel = new AttendanceEntryViewModel();
        bhv = new AttendanceEntryFormBehavior();
        this.Title = viewModel.HeaderText;
        this.BindingContext = viewModel;
        this.Behaviors.Add(bhv);
        
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
        Content = new Grid
        {
            RowDefinitions = Rows.Define((Row.TextEntry, 36)),ColumnDefinitions = Columns.Define((Column.Description, Star),(Column.Input, Stars(2))),

            Children =
            {
                new BaseEntryView().Row(1).DynamicResource(BaseEntryView.AutomationIdProperty, "entryView"),
        new Label().Text("Code:").Row( Row.TextEntry).Column(Column.Description),
                new Entry     {                    Keyboard = Keyboard.Numeric,                    BackgroundColor = Colors.AliceBlue,}.Row(Row.TextEntry).Column(Column.Input)
                 .FontSize(15)     .Placeholder("Enter number")                 .TextColor(Colors.Black)                 .Height(44)                 .Margin(5, 5)
                 .Bind(Entry.TextProperty, static (ViewModel vm) vm => vm.RegistrationCode)
            }
        };


    }

}
enum Row { TextEntry }
enum Column { Description, Input }