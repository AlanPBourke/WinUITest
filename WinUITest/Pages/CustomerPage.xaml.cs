using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.Toolkit.Mvvm.Input;
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
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinUITest.Data;
using WinUITest.UserControls;
using WinUITest.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUITest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerPage : Page
    {
        public ICommand AddCommand => new AsyncRelayCommand(OpenAddDialog);

        public ICommand EditCommand => new AsyncRelayCommand(OpenEditDialog);

        public ICommand DeleteCommand => new AsyncRelayCommand(DeleteCustomer);

        private Task DeleteCustomer()
        {
            throw new NotImplementedException();
        }

        public CustomerMaintenanceViewModel ViewModel { get; }
        public CustomerPage()
        {
            this.InitializeComponent();
            ViewModel = new CustomerMaintenanceViewModel();
            ViewModel.Load();
        }

        private void CustomersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid g = sender as DataGrid;
            if (g != null && g.SelectedItem!=null)
            {
                var cust = g.SelectedItem as CustomerViewModel;
                ViewModel.SetCustomer(cust.CustomerId);
            }
        }

        private void TransactionsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid g = sender as DataGrid;
            if (g != null && g.SelectedItem != null)
            {
                var txn = g.SelectedItem as TransactionViewModel;
                ViewModel.SetTransaction(txn.TransactionId);
            }
        }


        private void TransactionDetailsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async Task OpenAddDialog()
        {
            CustomerContentDialog NewCustomerDialog = new CustomerContentDialog(ViewModel);
            NewCustomerDialog.Title = "New Customer";
            NewCustomerDialog.DataContext = new CustomerViewModel(new Customer());
            NewCustomerDialog.XamlRoot = this.Content.XamlRoot;
            ViewModel.IsAdding = true;
            await NewCustomerDialog.ShowAsync();
            ViewModel.IsAdding = false;
            ViewModel.SelectedCustomer = NewCustomerDialog.DataContext as CustomerViewModel;
            ViewModel.SelectedCustomer.Save();
            ViewModel.Load();
        }

        private async Task OpenEditDialog()
        {
            if (ViewModel.SelectedCustomer != null)
            {
                CustomerContentDialog EditCustomerDialog = new CustomerContentDialog(ViewModel);
                EditCustomerDialog.Title = "Edit Customer";
                EditCustomerDialog.DataContext = ViewModel.SelectedCustomer;
                EditCustomerDialog.XamlRoot = this.Content.XamlRoot;
                ViewModel.IsEditing = true;
                await EditCustomerDialog.ShowAsync();
                ViewModel.IsEditing = false;
                ViewModel.SelectedCustomer.Save();
                ViewModel.Load();
            }
        }

        //private async void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        //{
        //    System.Diagnostics.Debug.WriteLine($"query submitted:{args.QueryText}");
        //    System.Diagnostics.Debug.WriteLine($"text changed:{sender.Text}");

        //    ViewModel.SearchCustomer(sender.Text);

        //    if (ViewModel.SearchResults.Count == 1)
        //    {
        //        ViewModel.SetCustomer(ViewModel.SearchResults.First().CustomerId);
        //    }
        //    else
        //    {
        //        if (ViewModel.SearchResults.Count > 1)
        //        {
        //            await SearchResultsDialog.ShowAsync();
        //        }
        //        else
        //        {
        //            CustomerSearchBox.Text = "No results found.";
        //        }
        //    }
        //}

        //private void SearchResultsDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        //{
        //    if (SearchResultsListView.SelectedItem != null)
        //    {
        //        var CustomerChosen = SearchResultsListView.SelectedItem as CustomerViewModel;
        //        ViewModel.SetCustomer(CustomerChosen.CustomerId);
        //    }
        //    SearchResultsDialog.Hide();
        //}

    }
}
