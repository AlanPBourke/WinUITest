using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using WinUITest.Data;

// https://github.com/dotnet/aspnetcore/issues/38238
// https://github.com/dotnet/roslyn/issues/50451

// https://github.com/CommunityToolkit/MVVM-Samples/blob/master/docs/mvvm/PuttingThingsTogether.md

namespace WinUITest.ViewModels
{

    public class CustomerViewModel : ObservableValidator, IEditableObject
    {
        public string Errors => string.Join(Environment.NewLine, from ValidationResult e in GetErrors(null) select e.ErrorMessage);
        public bool CanSave => (string.IsNullOrEmpty(Name) == false);
        private readonly Customer _customer;
        private CustomerViewModel _backup;

        private string _name;
        private string _customercode;

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value, true);
        }

        [Required]
        [MinLength(1, ErrorMessage = "Customer Code cannot be empty sir.")]
        [MaxLength(16, ErrorMessage = "Customer Code cannot be > 16.")]
        public string CustomerCode
        {
            get => _customercode;
            set => SetProperty(ref _customercode, value, true);
        }

        private int _customerid;
        [CustomValidation(typeof(CustomerViewModel), nameof(CanDeleteCustomer))]
        public int CustomerId
        {
            get => _customerid;
            set => SetProperty(ref _customerid, value, true);
        }

        public Decimal Balance { get; set; }

        public CustomerViewModel(Customer customer)
        {
            _customer = customer;
            CustomerCode = _customer.CustomerCode;
            Name = _customer.Name;
            Balance = _customer.Balance;
            PropertyChanged += CustomerViewModel_PropertyChanged;
            ErrorsChanged += CustomerViewModel_ErrorsChanged;
        }

        public void Delete()
        {
            ValidateProperty(nameof(CustomerCode));
            if (HasErrors == false)
            {
                Debug.WriteLine("Deleting");
            }
        }

        public void Save()
        {
            _customer.CustomerCode = CustomerCode;
            _customer.Name = Name;

            App.DataProvider.Customers.Save(_customer);
        }

        public override string ToString()
        {
            return $"{CustomerCode}\t{Name}\t{CanSave}";
        }

        public void BeginEdit()
        {
            _backup = this.MemberwiseClone() as CustomerViewModel;
        }

        public void CancelEdit()
        {
            CustomerCode = _backup.CustomerCode;
            Name = _backup.Name;
        }

        public void EndEdit()
        {

        }

        public static ValidationResult CanDeleteCustomer(int id, ValidationContext context)
        {

            if (App.DataProvider.Customers.CustomerHasTransactions(id))
            {
                return new("The customer has transactions and cannot be deleted.");
            }

            return ValidationResult.Success;
        }

        private void CustomerViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Errors)); // Update Errors on every Error change, so I can bind to it.
            Debug.WriteLine($"Errors:{Errors}");
        }

        private void CustomerViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(HasErrors))
            {
                OnPropertyChanged(nameof(HasErrors)); // Update HasErrors on every change, so I can bind to it.
            }
            Debug.WriteLine($"HasErrors:{HasErrors}");
        }

    }
}
