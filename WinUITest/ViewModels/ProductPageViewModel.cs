using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;


namespace WinUITest.ViewModels;

public class ProductPageViewModel : ObservableObject
{
    public ObservableCollection<ProductViewModel> Products { get; } = new();

    private bool _isproductselected;
    public bool IsProductSelected
    {
        get => _isproductselected;
        set => SetProperty(ref _isproductselected, value);
    }

    private ProductViewModel _selectedProduct;
    public ProductViewModel SelectedProduct
    {
        get => _selectedProduct;
        set
        {
            SetProperty(ref _selectedProduct, value);
            OnPropertyChanged(nameof(IsProductSelected));
        }

    }

    public void SetProduct(int productId)
    {
        var product = App.DataProvider.Products.Get(productId);
        if (product != null)
        {
            SelectedProduct = new ProductViewModel(product);
            //OnPropertyChanged(nameof(SelectedProduct));
            //OnPropertyChanged(nameof(IsProductSelected));
        }
    }

    private bool _isEditing;
    public bool IsEditing
    {
        get => _isEditing;
        set
        {
            SetProperty(ref _isEditing, value);
            OnPropertyChanged(nameof(IsAddingOrEditing));
        }
    }

    private bool _isNavigating;
    public bool IsNavigating
    {
        get => _isNavigating;
        set
        {
            SetProperty(ref _isNavigating, value);
        }
    }

    private bool _isAdding;
    public bool IsAdding
    {
        get => _isAdding;
        set
        {
            SetProperty(ref _isAdding, value);
            OnPropertyChanged(nameof(IsAddingOrEditing));
        }
    }

    public bool IsAddingOrEditing
    {
        get => IsAdding || IsEditing;
    }

    public ProductPageViewModel()
    {
    }

    public void Load()
    {
        var products = App.DataProvider.Products.GetAll();
        Products.Clear();

        foreach (var product in products)
        {
            Products.Add(new ProductViewModel(product));
        }
    }

    public bool CanDelete()
    {
        return App.DataProvider.Products.ProductInUse(SelectedProduct.ProductId) == false;
    }

    public void SetFirstProduct()
    {
        if (Products.Count > 0)
        {
            SelectedProduct = Products[0];
        }
    }
}
