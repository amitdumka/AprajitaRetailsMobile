using AprajitaRetails.Mobile.DataModels.Accounting;
using AprajitaRetails.Mobile.FormEntry.Models;
using AprajitaRetails.Mobile.FormEntry.ViewModels;
using AprajitaRetails.Mobile.FormEntry.Views;
using Syncfusion.Maui.DataForm;

namespace AprajitaRetails.Mobile.FormEntry.Behviours
{
    public class VoucherEntryFormBehavior : BaseEntryBehavior<VoucherEM, VoucherEntryViewModel>
    {
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            // Navigation = bindable.Navigation;
            var ev = ((VoucherEntryPage)bindable).entryView;

            viewModel = bindable.BindingContext as VoucherEntryViewModel;

            var dataForm = ev.FindByName<SfDataForm>("dataForm");

            if (dataForm != null)
            {
#if ANDROID
                dataForm.ColumnCount = 2;
#elif WINDOWS
                  dataForm.ColumnCount = 3;
#elif IOS
                dataForm.ColumnCount = 1;
#endif
                DataForm = dataForm;
                dataForm.RegisterEditor(nameof(VoucherEM.StoreId), DataFormEditorType.ComboBox);
                dataForm.RegisterEditor(nameof(VoucherEM.EmployeeId), DataFormEditorType.ComboBox);
                dataForm.RegisterEditor(nameof(VoucherEM.PartyId), DataFormEditorType.ComboBox);
                dataForm.RegisterEditor(nameof(VoucherEM.AccountId), DataFormEditorType.ComboBox);

                viewModel = DataForm.BindingContext as VoucherEntryViewModel;
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

            this.DataForm.DataObject = viewModel.Entity = new VoucherEM();
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
                    Notify.NotifyShort($" Please Wait while Saving new Voucher...");
                    VoucherDataModel dataModel = new();
                    //dataModel.Connect();
                    var vch = this.DataForm.DataObject as VoucherEM;
                    //var result = await dataModel.SaveAsync(new Voucher
                    //{
                    //    AccountId = vch.AccountId, 
                    //    OnDate = vch.OnDate,
                    //    MarkedDeleted = false,
                    //    IsReadOnly = false, VoucherType= vch.VoucherType, VoucherNumber=vch.VoucherNumber,
                    //    EntryStatus = EntryStatus.Added, UserId=CurrentSession.StoreCode,
                    //    EmployeeId = vch.EmployeeId, StoreId = vch.StoreId,
                    //    PartyId = vch.PartyId, SlipNumber = vch.SlipNumber,
                    //    Amount = vch.Amount, PaymentMode=vch.PaymentMode, Remarks = vch.Remarks,
                    //    Particulars = vch.Particulars, PartyName = vch.PartyName, PaymentDetails = vch.PaymentDetails,

                    //});

                    //if (result != null)
                    //{
                    //    Notify.NotifyShort($" Voucher is added Successful with Voucher Id {result.VoucherId}");
                    //    DataForm.DataObject = viewModel.Entity = new VoucherEM();
                    //}
                }
                else
                {
                    Notify.NotifyLong((DataForm.DataObject as VoucherEM).StoreId + " Please enter the required details");
                }
            };
        }

        protected override void OnSecondaryButtonClicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}