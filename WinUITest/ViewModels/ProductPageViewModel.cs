using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using WinUITest.Data;

namespace WinUITest.ViewModels;
public class ProductPageViewModel : ObservableObject
{
    public ObservableCollection<ProductViewModel> Products { get; } = new();
    private IDataProvider DataProvider;

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
            IsProductSelected = true;
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

    public ProductPageViewModel(IDataProvider dataprovider)
    {
        DataProvider = dataprovider;
        SelectedProduct = App.Current.Services.GetService<ProductViewModel>();
    }

    public void Load()
    {
        var products = DataProvider.Products.GetAll();
        Products.Clear();

        foreach (var product in products)
        {
            ProductViewModel newproductmodel = App.Current.Services.GetService<ProductViewModel>();
            newproductmodel.SetProduct(product);
            Products.Add(newproductmodel);
        }
    }

    public void SetProduct(int productId)
    {
        var product = DataProvider.Products.Get(productId);
        if (product != null)
        {
            ProductViewModel newproductmodel = App.Current.Services.GetService<ProductViewModel>();
            newproductmodel.SetProduct(product);
            SelectedProduct = newproductmodel;
            //OnPropertyChanged(nameof(SelectedProduct));
            //OnPropertyChanged(nameof(IsProductSelected));
        }
    }

    public bool CanDelete()
    {
        return DataProvider.Products.ProductInUse(SelectedProduct.ProductId) == false;
    }

    public void SetFirstProduct()
    {
        if (Products.Count > 0)
        {
            SetProduct(Products[0].ProductId);
        }
    }
}
