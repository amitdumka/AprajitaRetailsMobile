using AprajitaRetails.Mobile.FormEntry.Behviours;
using AprajitaRetails.Mobile.FormEntry.Models;
using AprajitaRetails.Mobile.FormEntry.ViewModels;

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
    }
}


public class EntryPage<T, VM, B> : ContentPage where B : BaseEntryBehavior<T, VM>
{
    protected VM viewModel;
   protected B bhv;
    
    protected BaseEntryView entryView;
	public EntryPage()
	{
       
        entryView= new BaseEntryView();
        
        Content = new Grid
        {
            Children = {
                entryView
            }
            
        };
       
       
    }

}