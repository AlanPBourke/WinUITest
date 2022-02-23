using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WinUITest.Data;
using WinUITest.ViewModel;

namespace WinUITest.ViewModel
{
    public class ProductMaintenanceViewModel : ViewModelBase
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
                    RaisePropertyChanged(nameof(SelectedProduct));
                    RaisePropertyChanged(nameof(IsProductSelected));
                }
            }
        }

        private bool _isEditing { get; set; } = false;
        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                _isEditing = value;
                RaisePropertyChanged(nameof(IsEditing));
            }
        }

        private bool _isAdding { get; set; } = false;
        public bool IsAdding
        {
            get => _isAdding;
            set
            {
                _isAdding = value;
                RaisePropertyChanged(nameof(IsAdding));
            }
        }

        public bool IsAddingOrEditing
        {
            get => _isAdding || _isEditing;
        }

        public ProductMaintenanceViewModel()
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
