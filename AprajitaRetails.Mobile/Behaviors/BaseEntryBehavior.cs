using Syncfusion.Maui.DataForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprajitaRetails.Mobile.Behaviors
{
    internal class BaseEntryBehavior <T>: Behavior<ContentPage>
    {
        /// <summary>
        /// Holds the data form object.
        /// </summary>
        private SfDataForm? dataForm;

        /// <summary>
        /// Holds the submit or Primary button instance.
        /// </summary>
        private Button? primaryButton;
        /// <summary>
        /// Holds the secondary button instance.
        /// </summary>
        private Button? secondaryButton;
        /// <summary>
        /// Holds the cancle button instance.
        /// </summary>
        private Button? cancleButton;
        /// <summary>
        /// Holds the close button instance.
        /// </summary>
        private Button? closeButton;


        /// <summary>
        /// Invokes on data form item generation.
        /// </summary>
        /// <param name="sender">The DataForm</param>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnGeneratingDataFormItem(object? sender, GenerateDataFormItemEventArgs e)
        {
            if (e.DataFormItem != null)
            {
                if (e.DataFormItem.FieldName.EndsWith("Id") && e.DataFormItem is not DataFormComboBoxItem) 
                { 
                    e.DataFormItem.IsVisible = false;
                }
                

                //if (e.DataFormItem.FieldName == nameof(PaymentFormModel.CVV) && e.DataFormItem is DataFormMaskedTextItem cvv)
                //{
                //    cvv.Padding = new Thickness(0, 5, 10, 5);
                //    cvv.Mask = "###";
                //    cvv.PromptChar = '*';
                //    cvv.Keyboard = Keyboard.Numeric;
                //}
                //else if ((e.DataFormItem.FieldName == nameof(PaymentFormModel.Month) || e.DataFormItem.FieldName == nameof(PaymentFormModel.Year)) && e.DataFormItem is DataFormComboBoxItem comboBoxItem)
                //{
                //    comboBoxItem.MaxDropDownHeight = 200;
                //}
                //else if (e.DataFormItem.FieldName == nameof(PaymentFormModel.Amount) && e.DataFormItem is DataFormMaskedTextItem amount)
                //{
                //    amount.Mask = "$#####";
                //    amount.PromptChar = ' ';
                //    amount.ValueMaskFormat = MaskedEditorMaskFormat.ExcludePromptAndLiterals;
                //    amount.Keyboard = Keyboard.Numeric;
                //}
                //else if (e.DataFormItem.FieldName == nameof(PaymentFormModel.CardNumber) && e.DataFormItem is DataFormMaskedTextItem cardNumber)
                //{
                //    cardNumber.Mask = "####-####-####-####";
                //    cardNumber.Keyboard = Keyboard.Numeric;
                //}
            }
        }



        /// <summary>
        /// Invokes on submit button click.
        /// </summary>
        /// <param name="sender">The submit button.</param>
        /// <param name="e">The event arguments.</param>
        protected virtual async void OnPrimaryButtonClicked(object? sender, EventArgs e)
        {
            if (this.dataForm != null && App.Current?.MainPage != null)
            {
                if (this.dataForm.Validate())
                {
                    // TODO: Move to Desired Page or Navigation after operation
                    await App.Current.MainPage.DisplayAlert("", "Payment Successful", "OK");
                }
                else
                {

                    await App.Current.MainPage.DisplayAlert("", "Please enter the required details", "OK");
                }
            }
        }

        /// <summary>
        /// Invokes on submit button click.
        /// </summary>
        /// <param name="sender">The submit button.</param>
        /// <param name="e">The event arguments.</param>
        protected virtual async void OnCloseButtonClicked(object? sender, EventArgs e)
        {
            if (this.dataForm != null && App.Current?.MainPage != null)
            {
                if (this.dataForm.Validate())
                {
                    await App.Current.MainPage.DisplayAlert("", "Payment Successful", "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("", "Please enter the required details", "OK");
                }
            }
        }

        /// <summary>
        /// Invokes on submit button click.
        /// </summary>
        /// <param name="sender">The submit button.</param>
        /// <param name="e">The event arguments.</param>
        protected virtual async void OnSecondaryButtonClicked(object? sender, EventArgs e)
        {
            if (this.dataForm != null && App.Current?.MainPage != null)
            {
                if (this.dataForm.Validate())
                {
                    await App.Current.MainPage.DisplayAlert("", "Payment Successful", "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("", "Please enter the required details", "OK");
                }
            }
        }
        /// <summary>
        /// Invokes on submit button click.
        /// </summary>
        /// <param name="sender">The submit button.</param>
        /// <param name="e">The event arguments.</param>
        protected virtual async void OnCancleButtonClicked(object? sender, EventArgs e)
        {
            if (this.dataForm != null && App.Current?.MainPage != null)
            {
                if (this.dataForm.Validate())
                {
                    await App.Current.MainPage.DisplayAlert("", "Payment Successful", "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("", "Please enter the required details", "OK");
                }
            }
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);
            if (this.dataForm != null)
            {
                this.dataForm.GenerateDataFormItem -= this.OnGeneratingDataFormItem;
            }

            if (this.primaryButton != null)
            {
                this.primaryButton.Clicked -= OnPrimaryButtonClicked;
            }
            if (this.secondaryButton != null)
            {
                this.secondaryButton.Clicked -= OnSecondaryButtonClicked;
            }
            if (this.cancleButton != null)
            {
                this.cancleButton.Clicked -= OnCancleButtonClicked;
            }
            if (this.closeButton != null)
            {
                this.closeButton.Clicked -= OnCloseButtonClicked;
            }
        }

    }
}
