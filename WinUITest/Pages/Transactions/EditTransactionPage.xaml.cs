using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
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
        ViewModel.SaveTransactionDetail();
        ProductSearchBox.Text = string.Empty;
    }

    public void CancelTransactionDetail()
    {
        ViewModel.IsAdding = false;
        ViewModel.IsEditing = false;
        ViewModel.IsNavigating = true;
    }

    private void TransactionDetailsDataGrid_SelectionChanged(object sender, Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs e)
    {

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

        if (args.ChosenSuggestion != null && args.ChosenSuggestion is Product)
        {
            //User selected an item, take an action
            ViewModel.SetSelectedProduct(args.ChosenSuggestion as Product);

        }
        else if (!string.IsNullOrEmpty(args.QueryText))
        {
            //Do a fuzzy search based on the text
            var suggestions = ViewModel.SearchProducts(sender.Text);
            if (suggestions.Count > 0)
            {
                ViewModel.SetSelectedProduct(suggestions.FirstOrDefault());
            }
        }

    }

    private void ProductSearchBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
    {
        if (args.SelectedItem is Product product)
        {
            sender.Text = product.ProductCode;
            ViewModel.SetSelectedProduct(args.SelectedItem as Product);
            ViewModel.SelectedTransactionDetail.Price = ViewModel.SelectedProduct.Price;
        }
    }

    private void ProductSearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
        {
            var suggestions = ViewModel.SearchProducts(sender.Text);

            if (suggestions.Count > 0)
                sender.ItemsSource = suggestions;
            else
                sender.ItemsSource = new string[] { "No results found" };
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
        var suggestions = ViewModel.SearchCustomers(sender.Text);

        if (suggestions.Count > 0)
            sender.ItemsSource = suggestions;
        else
            sender.ItemsSource = new string[] { "No results found" };
    }
}
