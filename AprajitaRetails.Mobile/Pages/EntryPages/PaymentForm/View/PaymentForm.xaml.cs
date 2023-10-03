#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace AprajitaRetails.Mobile.DataForm
{
    using AprajitaRetails.Mobile;

    public partial class PaymentForm : BaseView
    {
        public PaymentForm()
        {
            InitializeComponent();
            DataFormViewModel viewModel = new DataFormViewModel();
            this.BindingContext = viewModel;
            this.Behaviors.Add( new  PaymentFormBehavior());
        }
    }
}