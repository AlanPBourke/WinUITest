using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinUITest.Data;

namespace WinUITest.ViewModel
{ 
    public class ProductViewModel : ViewModelBase
    {
        private readonly Product _product;

        public int ProductId
        {
            get => _product.ProductId; 
            set
            {
                if (_product.ProductId != value)
                {
                    _product.ProductId = value;
                    RaisePropertyChanged(nameof(ProductId));
                }
            }
        }

        public string ProductName
        {
            get => _product.ProductName;
            set
            {
                if (_product.ProductName != value)
                {
                    _product.ProductName = value;
                    RaisePropertyChanged(nameof(ProductName));
                }
            }
        }

        public string ProductCode
        {
            get => _product.ProductCode;
            set
            {
                if (_product.ProductCode != value)
                {
                    _product.ProductCode = value;
                    RaisePropertyChanged(nameof(ProductCode));
                }
            }
        }
        public decimal Price
        {
            get => _product.Price;
            set
            {
                if (_product.Price != value)
                {
                    _product.Price = value;
                    RaisePropertyChanged(nameof(Price));
                }
            }
        }

        public ProductViewModel(Product product)
        {
            _product = product;
        }

        public void Save()
        {
            App.DataProvider.Products.Save(_product);
        }
    }
}
