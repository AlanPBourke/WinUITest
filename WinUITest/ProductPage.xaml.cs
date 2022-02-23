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
using WinUITest.ViewModel;
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
                System.Diagnostics.Debug.WriteLine($"SelectionChanged:{ViewModel.SelectedProduct.ProductCode}");
            }
        }

         public bool IsAddingOrEditing()
        {
            bool ret = false;
            if (ViewModel.SelectedProduct != null)
                ret = (ViewModel.IsEditing || ViewModel.IsAdding);
            System.Diagnostics.Debug.WriteLine($"AddOrEdit:{ret}");
            return ret; 
        }

        public async Task OpenAddDialog()
        {
            AddEditDialog.Title = "New Product";
            AddEditDialog.DataContext = new Product();
            ViewModel.IsAdding = true;
            //  makes no difference: AddEditDialog.XamlRoot = this.Content.XamlRoot;
            await AddEditDialog.ShowAsync();
            ViewModel.IsAdding = false;
        }

        public async Task OpenEditDialog()
        {
            AddEditDialog.Title = "Edit Product";
            AddEditDialog.DataContext = ViewModel.SelectedProduct;
            ViewModel.IsEditing = true;
            //  makes no difference: AddEditDialog.XamlRoot = this.Content.XamlRoot;
            await AddEditDialog.ShowAsync();
            ViewModel.IsEditing = false;
        }
    }
}
