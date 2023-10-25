using AprajitaRetails.Shared.Models.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Maui.Controls;
using CommunityToolkit.Maui.Converters;
using Switch = Microsoft.Maui.Controls.Switch;
using System.ComponentModel.DataAnnotations;

namespace AprajitaRetails.Mobile.Features.Test
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
         [Display(Name = "Attndance")]
        public bool IsEmployed { get; set; }
        public GenderType Gender { get; set; }
        [EmailAddress]
        public string SelectedOption { get; set; } // For ComboBox-like selection
    }

    public enum GenderType
    {
        Male,
        Female,
        Other
    }
    public class PersonViewModel : INotifyPropertyChanged
    {
        public Person Model { get; set; }

        public PersonViewModel()
        {
            Model = new Person();
        }

        // Implement INotifyPropertyChanged interface to handle property changes
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormPage : ContentPage
    {
        private PersonViewModel viewModel;
        private VerticalStackLayout ContentLayout;
        public FormPage()
        {
           // InitializeComponent();

            ContentLayout = new VerticalStackLayout();
            Content = new Grid
            {
                Children =
                {
                    ContentLayout
                }
            };

            viewModel = new PersonViewModel();
            BindingContext = viewModel;

            GenerateForm();
        }

        private void GenerateForm()
        {
            var type = viewModel.Model.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                var label = new Label
                {
                    Text = property.Name,
                };

                //if(property.Attributes!=null)
                //{
                    
                //    Notify.NotifyLong(property.Attributes.ToString());
                //}
                if (property.CustomAttributes != null)
                {

                    Notify.NotifyLong(property.Name+" has " +property.CustomAttributes.ToList().Count+" Att");
                }

                if (property.PropertyType == typeof(bool))
                {
                    var switchControl = new Microsoft.Maui.Controls.Switch();
                    switchControl.SetBinding(Switch.IsToggledProperty, property.Name);
                    switchControl.BindingContext = viewModel;
                    ContentLayout.Children.Add(label);
                    ContentLayout.Children.Add(switchControl);
                }
                else if (property.PropertyType == typeof(DateTime))
                {
                    var datePicker = new DatePicker();
                    datePicker.SetBinding(DatePicker.DateProperty, property.Name);
                    datePicker.BindingContext = viewModel;
                    ContentLayout.Children.Add(label);
                    ContentLayout.Children.Add(datePicker);
                }
                else if (property.PropertyType.IsEnum)
                {
                    var picker = new Picker();
                    var enumValues = Enum.GetValues(property.PropertyType);
                    foreach (var enumValue in enumValues)
                    {
                        picker.Items.Add(enumValue.ToString());
                    }
                    picker.SetBinding(Picker.SelectedIndexProperty, property.Name, converter: new EnumToIntConverter());
                    picker.BindingContext = viewModel;
                    ContentLayout.Children.Add(label);
                    ContentLayout.Children.Add(picker);
                }
                else
                {
                    var entry = new Entry();
                    entry.SetBinding(Entry.TextProperty, property.Name);
                    entry.BindingContext = viewModel;
                    ContentLayout.Children.Add(label);
                    ContentLayout.Children.Add(entry);
                }
            }
        }
    }

    public class AutoFormPage<TModel> : ContentPage where TModel : new()
    {
        public AutoFormPage()
        {
            var viewModel = new AutoFormViewModel<TModel>();
            BindingContext = viewModel;

            var stackLayout = new StackLayout();

            foreach (var property in typeof(TModel).GetProperties())
            {
                if (property.PropertyType == typeof(string) || property.PropertyType == typeof(int))
                {
                    var entry = new Entry();
                    entry.SetBinding(Entry.TextProperty, property.Name);
                    stackLayout.Children.Add(entry);
                }
            }

            var saveButton = new Button { Text = "Save" };
            saveButton.Clicked += (sender, e) => Save();
            stackLayout.Children.Add(saveButton);

            Content = stackLayout;

            async void Save()
            {
                // Access the model and perform the save operation here
                var model = viewModel.Model;

                // Example: Save the model to a database or perform other actions
                // You should add error handling and validation as needed

                await DisplayAlert("Success", "Data saved!", "OK");
            }
        }
    }
    public class AutoFormViewModel<TModel> : INotifyPropertyChanged where TModel : new()
    {
        private TModel model;

        public TModel Model
        {
            get => model;
            set
            {
                model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        public AutoFormViewModel()
        {
            Model = new TModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public partial class TestMainPage : ContentPage
    {
        public TestMainPage()
        {
           // InitializeComponent();
        }

        private void OnCreateFormClicked(object sender, EventArgs e)
        {
            var autoFormPage = new AutoFormPage<Person>();
            Navigation.PushAsync(autoFormPage);
        }
    }
}
