using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WinUITest.ViewModels
{
    public class ProductPageViewModel : ObservableObject
    {
        public ObservableCollection<ProductViewModel> Products { get; } = new();
        public bool IsProductSelected => _selectedProduct != null;
        private ProductViewModel _selectedProduct;
        public ProductViewModel SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                if (_selectedProduct != value)
                {
                    _selectedProduct = value;
                    OnPropertyChanged(nameof(SelectedProduct));
                    OnPropertyChanged(nameof(IsProductSelected));
                }
            }
        }

        public void SetProduct(int productId)
        {
            var product = App.DataProvider.Products.Get(productId);
            if (product != null)
            {
                SelectedProduct = new ProductViewModel(product);
                OnPropertyChanged(nameof(SelectedProduct));
                OnPropertyChanged(nameof(IsProductSelected));
            }
        }

        private bool _isEditing { get; set; } = false;
        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                _isEditing = value;
                OnPropertyChanged(nameof(IsEditing));
                OnPropertyChanged(nameof(IsAddingOrEditing));
            }
        }

        private bool _isNavigating { get; set; } = true;
        public bool IsNavigating
        {
            get => _isNavigating;
            set
            {
                _isNavigating = value;
                OnPropertyChanged(nameof(IsNavigating));
            }
        }

        private bool _isAdding { get; set; } = false;
        public bool IsAdding
        {
            get => _isAdding;
            set
            {
                _isAdding = value;
                OnPropertyChanged(nameof(IsAdding));
                OnPropertyChanged(nameof(IsAddingOrEditing));
            }
        }

        private bool _isAddingOrEditing { get; } = false;
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
    }
}
