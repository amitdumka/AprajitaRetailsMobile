using AprajitaRetails.Mobile.DataModels.Payroll;
using AprajitaRetails.Mobile.FormEntry.Models;
using AprajitaRetails.Mobile.FormEntry.ViewModels;
using AprajitaRetails.Mobile.FormEntry.Views;

using Syncfusion.Maui.DataForm;

namespace AprajitaRetails.Mobile.FormEntry.Behviours
{
    public class EmployeeEntryFormBehavior : BaseEntryBehavior<EmployeeEM, EmployeeEntryViewModel>
    {
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            // Navigation = bindable.Navigation;
            var ev = ((EmployeeEntryPage)bindable).entryView;

            viewModel = bindable.BindingContext as EmployeeEntryViewModel;

            var dataForm = ev.FindByName<SfDataForm>("dataForm");

            if (dataForm != null)
            {
                dataForm.ColumnCount = 1;
                DataForm = dataForm;
                dataForm.RegisterEditor(nameof(EmployeeEM.StoreId), DataFormEditorType.ComboBox);
                dataForm.RegisterEditor(nameof(EmployeeEM.Title), DataFormEditorType.ComboBox);
                dataForm.RegisterEditor("IsWorking", DataFormEditorType.Switch);

                viewModel = DataForm.BindingContext as EmployeeEntryViewModel;
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
            
            this.DataForm.DataObject = viewModel.Entity = new EmployeeEM();
        }

        protected override void OnGenerateDataFormItem(object sender, GenerateDataFormItemEventArgs e)
        {
            base.OnGenerateDataFormItem(sender, e);

            if (e.DataFormItem != null && (e.DataFormItem.FieldName == "Title" || e.DataFormItem.FieldName == "Title") && e.DataFormItem is DataFormComboBoxItem cbTitles)
            {
                e.DataFormItem.LabelText = "Tile";
                var tiles = new List<string> { "Mr", "Mrs.", "Ms", "Master" };
                cbTitles.ItemsSource = tiles;
                cbTitles.MaxDropDownHeight = 100;
            }
            if (e.DataFormItem != null && e.DataFormItem.FieldName == "IsWorking")
            {
                e.DataFormItem.LabelText = "Working";
            }

            if (e.DataFormItem != null && (e.DataFormItem.FieldName == "IsActive" || e.DataFormItem.FieldName == "HasErrors"))
            {
                e.DataFormItem.IsVisible = false;
            }
            if (e.DataFormGroupItem != null)
            {
                if (e.DataFormGroupItem.Name == "Name")
                {
                    e.DataFormGroupItem.ColumnCount = 4;
                }
                else if (e.DataFormGroupItem.Name == "Address")
                {
                    e.DataFormGroupItem.ColumnCount = 4;
                }
                else if (e.DataFormGroupItem.Name == "Contact")
                {
                    e.DataFormGroupItem.ColumnCount = 3;
                }
            }
        }

        protected override async void OnPrimaryButtonClicked(object sender, EventArgs e)
        {
            if (this.DataForm != null)
            {
                this.DataForm.Commit();
                if (this.DataForm.Validate())
                {
                    Notify.NotifyShort($" Please Wait while Saving new Employee...");
                    EmployeeDataModel dataModel = new EmployeeDataModel();
                    //dataModel.Connect();
                    var emp = this.DataForm.DataObject as EmployeeEM;
                    var result = await dataModel.SaveAsync(new Employee
                    {
                        AddressLine = emp.Address,
                        DOB = emp.BirthDate,
                        FirstName = emp.FirstName,
                        LastName = emp.LastName,
                        Gender = emp.Gender,
                        IsTailors = false,
                        IsWorking = emp.IsWorking,
                        JoiningDate = emp.JoiningDate,
                        LeavingDate = null,//DateTime.Now.AddYears(-10),
                        MarkedDeleted = false,
                        State = emp.State,
                        StoreId = emp.StoreId,
                        StreetName = emp.StreetName,
                        Title = emp.Title,
                        ZipCode = emp.ZipCode,
                        Category = emp.Category,
                        City = emp.City,
                        Country = emp.Country,
                        EmployeeId = "",
                    });

                    if (result != null)
                    {
                        Notify.NotifyShort($" Employee is added Successful with Employee Id {result.EmployeeId}");
                        DataForm.DataObject = viewModel.Entity = new EmployeeEM();
                    }
                }
                else
                {
                    Notify.NotifyLong((DataForm.DataObject as EmployeeEM).StoreId + " Please enter the required details");
                }
            };
        }

        protected override void OnSecondaryButtonClicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}