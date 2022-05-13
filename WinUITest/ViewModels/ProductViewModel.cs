using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using WinUITest.Data;

namespace WinUITest.ViewModels
{
    public class ProductViewModel : ObservableValidator, IEditableObject
    {
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

        private decimal _price;
        [Required]
        [Range(0, 999999999.99, ErrorMessage = $"Price must be nonzero and less than 999999999.99")]
        public string Price
        {
            get => _productname;
            set => SetProperty(ref _productname, value, true);
        }

        public string PriceString
        {
            get => _product.Price.ToString();
            set
            {
                if (_product.Price != Convert.ToDecimal(value))
                {
                    _product.Price = Convert.ToDecimal(value);
                    OnPropertyChanged(nameof(Price));
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
