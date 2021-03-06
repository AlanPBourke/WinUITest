using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinUITest.Data;

namespace WinUITest.ViewModels
{ 
    public class ProductViewModel : ViewModelBase, IEditableObject
    {
        private readonly Product _product;
        private ProductViewModel _backup;

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

        public string PriceString
        {
            get => _product.Price.ToString();
            set
            {
                if (_product.Price != Convert.ToDecimal(value))
                {
                    _product.Price = Convert.ToDecimal(value);
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
            this.ProductCode = _backup.ProductCode;
            this.ProductName = _backup.ProductName;
            this.Price = _backup.Price; 
        }

        public void EndEdit()
        {
            throw new NotImplementedException();
        }
    }
}
