using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml.Controls;
using WinUITest.Data;
using WinUITest.Enums;
using WinUITest.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUITest.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class EditTransactionPage : Page
{
    private EditType TransactionEditType { get; set; }
    private EditTransactionWindowViewModel ViewModel;

    public ICommand AddDetailLineCommand => new RelayCommand(AddTransactionDetail);
    public ICommand SaveDetailLineCommand => new RelayCommand(SaveTransactionDetail);
    public ICommand CancelDetailLineCommand => new RelayCommand(CancelTransactionDetail);

    public EditTransactionPage()
    {
        this.InitializeComponent();
        ViewModel = App.Current.Services.GetService(typeof(EditTransactionWindowViewModel)) as EditTransactionWindowViewModel;
        ViewModel.Load();



        //switch (TransactionEditType)
        //{
        //    case EditType.Add:
        //        NewTransaction();
        //        break;
        //    case EditType.Edit:
        //        break;
        //    default:
        //        break;
        //}
    }

    public void SetEditMode(EditType edittype)
    {
        TransactionEditType = edittype;
    }

    private void NewTransaction()
    {
        //SetMode("add");
        //var newtxnvm = App.Current.Services.GetService(typeof(EditTransactionWindowViewModel)) as EditTransactionWindowViewModel;
        var newtxn = App.Current.Services.GetService(typeof(Transaction)) as Transaction;
        ViewModel.SetTransaction(newtxn);
        ViewModel.IsAdding = false;
        ViewModel.IsEditing = false;
        ViewModel.IsNavigating = true;
    }

    private void AddTransactionDetail()
    {
        //var newtxndetailvm = App.Current.Services.GetService(typeof(TransactionDetailViewModel)) as TransactionDetailViewModel;
        ViewModel.AddTransactionDetail();
    }


    private void SaveTransactionDetail()
    {
        if (ViewModel.SelectedTransactionDetail.HasErrors == false)
        {
            ViewModel.SaveTransactionDetail();
            ViewModel.SearchProductCode = string.Empty;
            TransactionDetailsDataGrid.SelectedIndex = -1;      // Otherwise SelectionChanged won't fire when there is only 1 item.
        }

    }

    public void CancelTransactionDetail()
    {
        ViewModel.IsAdding = false;
        ViewModel.IsEditing = false;
        ViewModel.IsNavigating = true;
    }

    private void DeleteConfirmationClick(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (ViewModel.SelectedTransactionDetail != null)
        {
            ViewModel.DeleteTransactionDetail();
            DeleteButton.Flyout.Hide();
        }
        //SetMode("navigate");
    }

    private void DeleteCancelClick(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        ViewModel.IsAdding = false;
        ViewModel.IsNavigating = true;
        ViewModel.IsEditing = false;
        DeleteButton.Flyout.Hide();
        //SetMode("navigate");
    }

    private void TransactionDetailsDataGrid_SelectionChanged(object sender, Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs e)
    {
        DataGrid g = sender as DataGrid;
        if (g != null && g.SelectedItem != null)
        {
            var txn = g.SelectedItem as TransactionDetailViewModel;
            ViewModel.IsAdding = false;
            ViewModel.IsEditing = true;
            ViewModel.IsNavigating = false;
            ViewModel.EditTransactionDetail(txn);


        }

    }

    private void Page_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        switch (TransactionEditType)
        {
            case EditType.Add:
                NewTransaction();
                break;
            case EditType.Edit:
                break;
            default:
                break;
        }
    }


    private void ProductSearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        var found = false;
        Product foundProduct = new();

        if (args.ChosenSuggestion != null && args.ChosenSuggestion is Product)
        {
            //User selected an item, take an action
            foundProduct = args.ChosenSuggestion as Product;
            found = true;

        }
        else if (!string.IsNullOrEmpty(args.QueryText))
        {
            //Do a fuzzy search based on the text
            var suggestions = ViewModel.SearchProducts(sender.Text);
            if (suggestions.Count > 0)
            {
                foundProduct = suggestions.FirstOrDefault();
                found = true;
            }
        }

        if (found)
        {
            Debug.WriteLine($"QuerySubmitted found: {foundProduct.ProductCode} {ViewModel.SelectedTransactionDetail.ProductCode}");
            //sender.Text = foundProduct.ProductCode;
            ViewModel.SearchProductCode = foundProduct.ProductCode;
            //ViewModel.SetSelectedProduct(foundProduct);
            ViewModel.SetSelectedProduct2(foundProduct);
        }
        else
        {
            Debug.WriteLine($"QuerySubmitted found nothing.");
            ViewModel.SetSelectedProduct2(new Product());
        }

    }

    private void ProductSearchBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
    {

        if (args.SelectedItem is Product product)
        {
            Debug.WriteLine($"SuggestionChosen found: {product.ProductCode} {ViewModel.SelectedTransactionDetail.ProductCode}");
            //sender.Text = product.ProductCode;
            ViewModel.SearchProductCode = product.ProductCode;

            ViewModel.SetSelectedProduct2(product);
            //ViewModel.SetSelectedProduct(args.SelectedItem as Product);
            //ViewModel.SelectedTransactionDetail.Price = product.Price;
            //ViewModel.SelectedTransactionDetail.ProductCode = product.ProductCode;
        }
        else
        {
            Debug.WriteLine($"SuggestionChosen found nothing.");
            ViewModel.SetSelectedProduct2(new Product());
        }
    }

    private void ProductSearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
        {
            var suggestions = ViewModel.SearchProducts(sender.Text);

            //          if (suggestions.Count > 0)
            //        {
            sender.ItemsSource = suggestions;
            //       }
        }
    }

    private void CustomerSearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        if (args.ChosenSuggestion != null && args.ChosenSuggestion is Customer)
        {
            //User selected an item, take an action
            ViewModel.SetSelectedCustomer(args.ChosenSuggestion as Customer);

        }
        else if (!string.IsNullOrEmpty(args.QueryText))
        {
            //Do a fuzzy search based on the text
            var suggestions = ViewModel.SearchCustomers(sender.Text);
            if (suggestions.Count > 0)
            {
                ViewModel.SetSelectedCustomer(suggestions.FirstOrDefault());
            }
        }
    }

    private void CustomerSearchBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
    {
        if (args.SelectedItem is Customer customer)
        {
            sender.Text = customer.CustomerCode;
            ViewModel.SetSelectedCustomer(args.SelectedItem as Customer);
        }
    }

    private void CustomerSearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
        {
            var suggestions = ViewModel.SearchCustomers(sender.Text);

            if (suggestions.Count > 0)
                sender.ItemsSource = suggestions;
            else
                sender.Text = "";
        }
    }
}
