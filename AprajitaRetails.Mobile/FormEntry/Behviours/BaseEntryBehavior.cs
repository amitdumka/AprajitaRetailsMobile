using AprajitaRetails.Mobile.FormEntry.ViewModels;

using Syncfusion.Maui.DataForm;

namespace AprajitaRetails.Mobile.FormEntry.Behviours
{
    public abstract partial class BaseEntryBehavior<T, VM> : Behavior<ContentPage>, INotifyPropertyChanged
    {
        protected INavigation Navigation;
        protected Button primaryButton, secondaryButton, backButton, cancleButton;
        protected VM viewModel;
        protected SfDataForm DataForm { get; set; }
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            Navigation = bindable.Navigation;
        }

        protected virtual async void OnBackButtonClicked(object? sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        protected abstract void OnCancleButtonClicked(object? sender, EventArgs e);

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);

            if (DataForm != null)
            {
                DataForm.GenerateDataFormItem -= this.OnGenerateDataFormItem;
                // (dataForm.DataObject as Attendance).PropertyChanged -= OnDataObjectPropertyChanged;
            }
        }

        protected virtual void OnGenerateDataFormItem(object sender, GenerateDataFormItemEventArgs e)
        {
            if (e.DataFormItem != null && (e.DataFormItem.FieldName == "StoreId" || e.DataFormItem.FieldName == "Store") && e.DataFormItem is DataFormComboBoxItem comboBoxItem)
            {
                e.DataFormItem.LabelText = "Store";
                comboBoxItem.DisplayMemberPath = "Value";
                comboBoxItem.SelectedValuePath = "ID";

                var viewModel = DataForm.BindingContext as BaseEntryViewModel<T>;
                comboBoxItem.BindingContext = viewModel;
                comboBoxItem.SetBinding(DataFormComboBoxItem.ItemsSourceProperty, nameof(viewModel.Stores), BindingMode.TwoWay);
            }

            if (e.DataFormItem != null && (e.DataFormItem.FieldName == "EmployeeId" || e.DataFormItem.FieldName == "Employee") && e.DataFormItem is DataFormComboBoxItem cbEmp)
            {
                e.DataFormItem.LabelText = "Employee";
                cbEmp.DisplayMemberPath = "Value";
                cbEmp.SelectedValuePath = "ID";

                var viewModel = DataForm.BindingContext as BaseEntryViewModel<T>; ;
                cbEmp.BindingContext = viewModel;
                cbEmp.SetBinding(DataFormComboBoxItem.ItemsSourceProperty, nameof(viewModel.Employees), BindingMode.TwoWay);
            }

            if (e.DataFormItem != null)
            {
                if (e.DataFormItem.FieldName == "IsTailoring")
                {
                    e.DataFormItem.IsVisible = false;
                }
            }
        }

        protected abstract void OnPrimaryButtonClicked(object? sender, EventArgs e);

        protected abstract void OnSecondaryButtonClicked(object? sender, EventArgs e);
    }
}