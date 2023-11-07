using AprajitaRetails.Mobile.DataModels.Accounting;
using AprajitaRetails.Mobile.FormEntry.Models;
using AprajitaRetails.Mobile.FormEntry.ViewModels;
using AprajitaRetails.Mobile.FormEntry.Views;
using AprajitaRetails.Shared.Models.Vouchers;
using Syncfusion.Maui.DataForm;

namespace AprajitaRetails.Mobile.FormEntry.Behviours
{
    public class CashVoucherEntryFormBehavior : BaseEntryBehavior<CashVoucherEM, CashVoucherEntryViewModel>
    {
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            // Navigation = bindable.Navigation;
            var ev = ((CashVoucherEntryPage)bindable).entryView;

            viewModel = bindable.BindingContext as CashVoucherEntryViewModel;

            var dataForm = ev.FindByName<SfDataForm>("dataForm");

            if (dataForm != null)
            {
                dataForm.ColumnCount = 1;
                DataForm = dataForm;
                dataForm.RegisterEditor(nameof(VoucherEM.StoreId), DataFormEditorType.ComboBox);
                dataForm.RegisterEditor(nameof(VoucherEM.EmployeeId), DataFormEditorType.ComboBox);
                dataForm.RegisterEditor(nameof(VoucherEM.PartyId), DataFormEditorType.ComboBox);
                dataForm.RegisterEditor(nameof(VoucherEM.AccountId), DataFormEditorType.ComboBox);

                viewModel = DataForm.BindingContext as CashVoucherEntryViewModel;
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

            this.DataForm.DataObject = viewModel.Entity = new CashVoucherEM();
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
                    CashVoucherDataModel dataModel = new();
                    //dataModel.Connect();
                    var vch = this.DataForm.DataObject as  CashVoucherEM;

                    //var result = await dataModel.SaveAsync(new CashVoucher
                    //{
                    //    TransactionId = vch.TransactionId,
                    //    OnDate = vch.OnDate,
                    //    MarkedDeleted = false,
                    //    IsReadOnly = false,
                    //    VoucherType = vch.VoucherType,
                    //    VoucherNumber = vch.CashVoucherNumber,
                    //    EntryStatus = EntryStatus.Added,
                    //    UserId = CurrentSession.StoreCode,
                    //    EmployeeId = vch.EmployeeId,
                    //    StoreId = vch.StoreId,
                    //    PartyId = vch.PartyId,
                    //    SlipNumber = vch.SlipNumber,
                    //    Amount = vch.Amount,
                    //    Remarks = vch.Remarks,
                    //    Particulars = vch.Particulars,
                    //    PartyName = vch.PartyName, 
                        
                    //});

                    //if (result != null)
                    //{
                    //    Notify.NotifyShort($" Cash Voucher is added Successful with Voucher Id {result.VoucherId}");
                    //    DataForm.DataObject = viewModel.Entity = new CashVoucherEM();
                    //}
                }
                else
                {
                    Notify.NotifyLong((DataForm.DataObject as CashVoucherEM).StoreId + " Please enter the required details");
                }
            };
        }

        protected override void OnSecondaryButtonClicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}