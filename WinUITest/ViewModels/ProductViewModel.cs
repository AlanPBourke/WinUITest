using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using WinUITest.Data;

namespace WinUITest.ViewModels;

public class ProductViewModel : ObservableValidator, IEditableObject
{
    public string Errors => string.Join(Environment.NewLine, from ValidationResult e in GetErrors(null) select e.ErrorMessage);
    private readonly Product _product;
    private ProductViewModel _backup;

    private int _productid;
    public int ProductId
    {
        get => _productid;
        set => SetProperty(ref _productid, value, true);
    }

    private string _productname;
    [Required]
    [MinLength(1, ErrorMessage = "Name is required.")]
    [MaxLength(100, ErrorMessage = "Name cannot be > 100.")]
    public string ProductName
    {
        get => _productname;
        set => SetProperty(ref _productname, value, true);
    }

    private string _productcode;
    [Required]
    [MinLength(1, ErrorMessage = "Code is required.")]
    [MaxLength(16, ErrorMessage = "Code cannot be > 16.")]
    public string ProductCode
    {
        get => _productcode;
        set => SetProperty(ref _productcode, value, true);
    }

    private double _price;
    [Required]
    [Range(0.01, 999999999.99, ErrorMessage = $"Price must be nonzero and less than 999999999.99")]
    public double Price
    {
        get => _price;
        set => SetProperty(ref _price, value, true);
    }

    private string _pricestring;
    public string PriceString
    {
        get => _pricestring;
        set => SetProperty(ref _pricestring, value, true);
    }

    public ProductViewModel(Product product)
    {
        _product = product;
        ProductCode = _product.ProductCode;
        ProductName = _product.ProductName;
        ProductId = _product.ProductId;
        Price = _product.Price;
        PropertyChanged += ProductViewModel_PropertyChanged;
        ErrorsChanged += ProductViewModel_ErrorsChanged;
    }

    public void Save()
    {
        _product.ProductCode = ProductCode;
        _product.ProductName = ProductName;
        _product.Price = Price;
        App.DataProvider.Products.Save(_product);
    }

    public void Delete()
    {
        App.DataProvider.Products.Delete(_product.ProductId);
    }

    public void BeginEdit()
    {
        _backup = this.MemberwiseClone() as ProductViewModel;
    }

    public void CancelEdit()
    {
        ProductCode = _backup.ProductCode;
        ProductName = _backup.ProductName;
        Price = _backup.Price;
    }

    public void EndEdit()
    {

    }

    private void ProductViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
    {
        OnPropertyChanged(nameof(Errors)); // Update Errors on every Error change, so I can bind to it.
    }

    private void ProductViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(HasErrors))
        {
            OnPropertyChanged(nameof(HasErrors)); // Update HasErrors on every change, so I can bind to it.
        }
    }
}
