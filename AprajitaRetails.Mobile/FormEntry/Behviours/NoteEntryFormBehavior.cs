using AprajitaRetails.Mobile.DataModels.Accounting;
using AprajitaRetails.Mobile.FormEntry.Models;
using AprajitaRetails.Mobile.FormEntry.ViewModels;
using AprajitaRetails.Mobile.FormEntry.Views;
using Syncfusion.Maui.DataForm;

namespace AprajitaRetails.Mobile.FormEntry.Behviours
{
    public class NoteEntryFormBehavior : BaseEntryBehavior<NoteEM, NoteEntryViewModel>
    {
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            // Navigation = bindable.Navigation;
            var ev = ((NoteEntryPage)bindable).entryView;

            viewModel = bindable.BindingContext as NoteEntryViewModel;

            var dataForm = ev.FindByName<SfDataForm>("dataForm");

            if (dataForm != null)
            {
                dataForm.ColumnCount = 1;
                DataForm = dataForm;
                dataForm.RegisterEditor(nameof(NoteEM.StoreId), DataFormEditorType.ComboBox);
                dataForm.RegisterEditor(nameof(NoteEM.StoreId), DataFormEditorType.ComboBox);
                dataForm.RegisterEditor(nameof(NoteEM.NotesType), DataFormEditorType.ComboBox);

                viewModel = DataForm.BindingContext as NoteEntryViewModel;
                dataForm.Commit();
                dataForm.GenerateDataFormItem += OnGenerateDataFormItem;

                this.primaryButton = ev.FindByName<Button>("PrimaryButton");
                if (this.primaryButton != null)
                {
                    this.primaryButton.Clicked += OnPrimaryButtonClicked;
                }
                backButton = ev.FindByName<Button>("BackButton");
                if (this.backButton != null)
                {
                    this.backButton.Clicked += OnBackButtonClicked;
                }
                cancleButton = ev.FindByName<Button>("CancleButton");
                if (this.cancleButton != null)
                {
                    this.cancleButton.Clicked += OnCancleButtonClicked;
                }
            }
        }

        protected override void OnCancleButtonClicked(object sender, EventArgs e)
        {
            this.DataForm.DataObject = viewModel.Entity = new NoteEM();
        }

        protected override void OnGenerateDataFormItem(object sender, GenerateDataFormItemEventArgs e)
        {
            base.OnGenerateDataFormItem(sender, e);
        }

        protected override async void OnPrimaryButtonClicked(object sender, EventArgs e)
        {
            if (this.DataForm != null)
            {
                this.DataForm.Commit();
                if (this.DataForm.Validate())
                {
                    Notify.NotifyShort($" Please Wait while Saving new Note...");
                    NoteDataModel dataModel = new();
                    //dataModel.Connect();
                    var vch = this.DataForm.DataObject as NoteEM;
                    //var result = await dataModel.SaveAsync(new Note
                    //{
                    //    OnDate = vch.OnDate, NotesType=vch.NotesType, Reason=vch.Reason, WithGST=vch.WithGST,
                    //    TaxRate=vch.TaxRate,
                    //    MarkedDeleted = false,
                    //    IsReadOnly = false,

                    //    NoteNumber = vch.NoteNumber,
                    //    EntryStatus = EntryStatus.Added,
                    //    UserId = CurrentSession.StoreCode,

                    //    StoreId = vch.StoreId,
                    //    PartyId = vch.PartyId,

                    //    Amount = vch.Amount,

                    //    Remarks = vch.Remarks,

                    //    PartyName = vch.PartyName,

                    //});

                    //if (result != null)
                    //{
                    //    Notify.NotifyShort($" Note is added Successful with Note Id {result.NoteId}");
                    //    DataForm.DataObject = viewModel.Entity = new NoteEM();
                    //}
                }
                else
                {
                    Notify.NotifyLong((DataForm.DataObject as NoteEM).StoreId + " Please enter the required details");
                }
            };
        }

        protected override void OnSecondaryButtonClicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}