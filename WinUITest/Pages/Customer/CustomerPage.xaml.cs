using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinUITest.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUITest.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class CustomerPage : Page
{
    //public ICommand AddCommand => new AsyncRelayCommand(OpenAddDialog);

    public CustomerPageViewModel ViewModel { get; }
    public CustomerPage()
    {
        this.InitializeComponent();
        ViewModel = App.Current.Services.GetService(typeof(CustomerPageViewModel)) as CustomerPageViewModel;
        ViewModel.Load();
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        if (ViewModel.Customers.Count > 0)
        {
            //CustomersGrid.SelectedItem = ViewModel.Customers[0].CustomerId;
            CustomersDataGrid.SelectedItem = ViewModel.Customers[0];
            ViewModel.SetFirstCustomer();

        }
        //CustomerContentFrame.NavigateToType(typeof(CustomerInfoPage), null, new FrameNavigationOptions { IsNavigationStackEnabled = true });
        //CustomerInfoTransactionsNavigationView.SelectedItem = 1;
    }

    private void CustomersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        DataGrid g = sender as DataGrid;
        if (g != null && g.SelectedItem != null)
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

    //private void TransactionDetailsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //{

    //}

    //private async Task OpenAddDialog()
    //{
    //    CustomerContentDialog NewCustomerDialog = new CustomerContentDialog(ViewModel);
    //    NewCustomerDialog.Title = "New Customer";
    //    NewCustomerDialog.DataContext = new CustomerViewModel(new Customer());
    //    NewCustomerDialog.XamlRoot = this.Content.XamlRoot;
    //    ViewModel.IsAdding = true;
    //    await NewCustomerDialog.ShowAsync();
    //    ViewModel.IsAdding = false;
    //    ViewModel.SelectedCustomer = NewCustomerDialog.DataContext as CustomerViewModel;
    //    ViewModel.SelectedCustomer.Save();
    //    ViewModel.Load();
    //}


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
