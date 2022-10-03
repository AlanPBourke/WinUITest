using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using WinUITest.Data;

// https://github.com/dotnet/aspnetcore/issues/38238
// https://github.com/dotnet/roslyn/issues/50451

// https://github.com/CommunityToolkit/MVVM-Samples/blob/master/docs/mvvm/PuttingThingsTogether.md

namespace WinUITest.ViewModels;
public class CustomerViewModel : ObservableValidator, IEditableObject
{
    private IDataProvider DataProvider;

    public string Errors => string.Join(Environment.NewLine, from ValidationResult e in GetErrors(null) select e.ErrorMessage);

    private Customer _customer;
    private CustomerViewModel _backup;

    private string _name;
    [Required]
    [MinLength(1, ErrorMessage = "Name is required.")]
    [MaxLength(100, ErrorMessage = "Name cannot be > 100.")]
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value, true);
    }

    private string _customercode;
    [Required]
    [MinLength(1, ErrorMessage = "Codeis required.")]
    [MaxLength(16, ErrorMessage = "Code cannot be > 16.")]
    public string CustomerCode
    {
        get => _customercode;
        set => SetProperty(ref _customercode, value, true);
    }

    private int _customerid;
    public int CustomerId
    {
        get => _customerid;
        set => SetProperty(ref _customerid, value, true);
    }

    public Double Balance { get; set; }

    public string BalanceForDisplay
    {
        get => Balance.ToString("0.##");
    }

    public CustomerViewModel(IDataProvider dataprovider)
    {
        DataProvider = dataprovider;
        PropertyChanged += CustomerViewModel_PropertyChanged;
        ErrorsChanged += CustomerViewModel_ErrorsChanged;
    }

    public void SetCustomer(Customer customer)
    {
        _customer = customer;
        CustomerId = _customer.CustomerId;
        CustomerCode = _customer.CustomerCode;
        Name = _customer.Name;
        Balance = _customer.Balance;
    }

    public void Delete()
    {
        DataProvider.Customers.DeleteCustomer(CustomerId);
    }

    public void Save()
    {
        _customer.CustomerCode = CustomerCode;
        _customer.Name = Name;

        DataProvider.Customers.Save(_customer);
    }

    public override string ToString()
    {
        return $"{CustomerCode}\t{Name}";
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

    public ValidationResult CanDeleteCustomer(int id, ValidationContext context)
    {

        if (DataProvider.Customers.CustomerHasTransactions(id))
        {
            return new("The customer has transactions and cannot be deleted.");
        }

        return ValidationResult.Success;
    }

    private void CustomerViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
    {
        OnPropertyChanged(nameof(Errors)); // Update Errors on every Error change, so I can bind to it.
    }

    private void CustomerViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(HasErrors))
        {
            OnPropertyChanged(nameof(HasErrors)); // Update HasErrors on every change, so I can bind to it.
        }
    }

}
