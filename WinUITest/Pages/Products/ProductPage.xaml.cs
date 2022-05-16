//using System;

//using System.Windows.Input;
//using CommunityToolkit.WinUI.UI.Controls;
//using Microsoft.Toolkit.Mvvm.Input;
//using Microsoft.UI.Xaml.Controls;
//using WinUITest.ViewModels;

using System.Windows.Input;
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using WinUITest.Data;
using WinUITest.ViewModels;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUITest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductPage : Page
    {
        public ICommand AddCommand => new RelayCommand(Add);
        public ICommand EditCommand => new RelayCommand(BeginEdit);
        public ICommand SaveCommand => new RelayCommand(Save);
        public ICommand CancelCommand => new RelayCommand(Cancel);

        public ProductPageViewModel ViewModel { get; }
        //private ProductViewModel SelectedProduct { get; set; }
        public ProductPage()
        {
            InitializeComponent();
            ViewModel = App.Current.Services.GetService(typeof(ProductPageViewModel)) as ProductPageViewModel;
            ViewModel.Load();
            SetMode("navigating");
            DataContext = ViewModel;
        }
        private void Page_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (ViewModel.Products.Count > 0)
            {
                ProductGrid.SelectedItem = ViewModel.Products[0];
                ViewModel.SetFirstProduct();
            }
        }

        private void Add()
        {
            SetMode("add");
            ViewModel.SelectedProduct = new ProductViewModel(new Product());
            ViewModel.SelectedProduct.BeginEdit();
        }

        private void BeginEdit()
        {
            SetMode("edit");
            ViewModel.SelectedProduct.BeginEdit();
        }
        private void Save()
        {
            if (ViewModel.SelectedProduct.HasErrors == false)
            {
                ViewModel.SelectedProduct.Save();
                ViewModel.SelectedProduct.EndEdit();

                ViewModel.IsAdding = false;
                ViewModel.IsEditing = false;
                ViewModel.Load();
                ViewModel.SetProduct(ViewModel.SelectedProduct.ProductId);
                SetMode("navigate");
            }
        }

        private void Cancel()
        {
            if (ViewModel.IsEditing)
            {
                ViewModel.SelectedProduct.CancelEdit();
            }
            else
            {
                ViewModel.SetProduct(ViewModel.Products[0].ProductId);
            }

            if (ViewModel.SelectedProduct != null)
            {
                ViewModel.SetProduct(ViewModel.SelectedProduct.ProductId);
            }

            SetMode("navigate");
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid g = sender as DataGrid;
            if (g != null && g.SelectedItem != null)
            {
                var product = g.SelectedItem as ProductViewModel;
                ViewModel.SetProduct(product.ProductId);
            }
        }

        //public async Task OpenAddDialog()
        //{
        //    ProductContentDialog NewProductDialog = new ProductContentDialog();
        //    NewProductDialog.Title = "New Product";
        //    NewProductDialog.DataContext = new ProductViewModel(new Product());
        //    NewProductDialog.XamlRoot = this.Content.XamlRoot;
        //    ViewModel.IsAdding = true;
        //    await NewProductDialog.ShowAsync();
        //    ViewModel.IsAdding = false;
        //    ViewModel.SelectedProduct = NewProductDialog.DataContext as ProductViewModel;
        //    ViewModel.SelectedProduct.Save();
        //    ViewModel.Load();

        //}

        //public async Task OpenEditDialog()
        //{
        //    if (ViewModel.SelectedProduct != null)
        //    {
        //        ProductContentDialog EditProductDialog = new ProductContentDialog();
        //        EditProductDialog.Title = "Edit Product";
        //        EditProductDialog.DataContext = ViewModel.SelectedProduct;
        //        EditProductDialog.XamlRoot = this.Content.XamlRoot;
        //        ViewModel.IsEditing = true;
        //        await EditProductDialog.ShowAsync();
        //        ViewModel.IsEditing = false;
        //        ViewModel.SelectedProduct.Save();
        //    }
        //}

        private void DeleteConfirmationClick(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {

            //if (InfoViewModel.SelectedCustomer.HasErrors == false)
            //{
            if (ViewModel.CanDelete())
            {
                ViewModel.SelectedProduct.Delete();
                ViewModel.Load();
                ViewModel.SetFirstProduct();
            }
            else
            {
                ProductMaintenanceInAppNotification.Show("This customer has transactions and cannot be deleted.", 0);
            }
            DeleteButton.Flyout.Hide();
            SetMode("navigate");
        }

        private void DeleteCancelClick(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.IsAdding = false;
            ViewModel.IsNavigating = true;
            ViewModel.IsEditing = false;
            DeleteButton.Flyout.Hide();
            SetMode("navigate");
        }

        private void SetMode(string mode)
        {
            switch (mode)
            {
                case "navigate":
                default:
                    ViewModel.IsNavigating = true;
                    ViewModel.IsAdding = false;
                    ViewModel.IsEditing = false;
                    break;
                case "add":
                    ViewModel.IsNavigating = false;
                    ViewModel.IsAdding = true;
                    ViewModel.IsEditing = false;
                    break;
                case "edit":
                    ViewModel.IsNavigating = false;
                    ViewModel.IsAdding = false;
                    ViewModel.IsEditing = true;
                    break;
            }
        }

    }
}
