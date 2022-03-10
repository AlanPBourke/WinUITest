using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinUITest.DataProvider;
using WinUITest.ViewModels;
using CommunityToolkit.WinUI.UI.Controls;
using WinUITest.Data;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUITest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductPage : Page
    {
        public ICommand AddCommand => new AsyncRelayCommand(OpenAddDialog);
        public ICommand EditCommand => new AsyncRelayCommand(OpenEditDialog);
        public ICommand DeleteCommand => new AsyncRelayCommand(DeleteProduct);

        public ProductMaintenanceViewModel ViewModel;
        //private ProductViewModel SelectedProduct { get; set; }
        public ProductPage()
        {
            ViewModel = new ProductMaintenanceViewModel();
            ViewModel.Load();
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid g = sender as DataGrid;
            if (g != null)
            {
                ViewModel.SelectedProduct = g.SelectedItem as ProductViewModel;
            }
        }

        public async Task OpenAddDialog()
        {
            ProductContentDialog NewProductDialog = new ProductContentDialog(ViewModel);
            NewProductDialog.Title = "New Product";
            NewProductDialog.DataContext = new ProductViewModel(new Product());
            NewProductDialog.XamlRoot = this.Content.XamlRoot;
            ViewModel.IsAdding = true;
            await NewProductDialog.ShowAsync();
            ViewModel.IsAdding = false;
            ViewModel.SelectedProduct = NewProductDialog.DataContext as ProductViewModel;
            ViewModel.SelectedProduct.Save();
            ViewModel.Load();

        }

        public async Task OpenEditDialog()
        {
            if (ViewModel.SelectedProduct != null)
            {
                ProductContentDialog EditProductDialog = new ProductContentDialog(ViewModel);
                EditProductDialog.Title = "Edit Product";
                EditProductDialog.DataContext = ViewModel.SelectedProduct;
                EditProductDialog.XamlRoot = this.Content.XamlRoot;
                ViewModel.IsEditing = true;
                await EditProductDialog.ShowAsync();
                ViewModel.IsEditing = false;
                ViewModel.SelectedProduct.Save();
            }
        }

        public async Task DeleteProduct()
        {
            if (ViewModel.SelectedProduct != null)
            {
                ContentDialog ConfirmDialog = new ContentDialog();
                ConfirmDialog.Title = $"Delete {ViewModel.SelectedProduct.ProductCode} ?";
                ConfirmDialog.PrimaryButtonText = "Delete";
                ConfirmDialog.CloseButtonText = "Cancel";
                ConfirmDialog.DefaultButton = ContentDialogButton.Secondary;
                ConfirmDialog.XamlRoot = this.Content.XamlRoot;

                var result = await ConfirmDialog.ShowAsync();
                
                if (result== ContentDialogResult.Primary)
                {
                    ViewModel.SelectedProduct.Delete();
                    ViewModel.Load();
                }
            }
        }

    }
}
