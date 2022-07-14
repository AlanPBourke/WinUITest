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
        // TODO ViewModel.CurrentTransaction.Save();
        ViewModel.SaveTransactionDetail();
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

    private void CustomersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var combo = sender as ComboBox;
        if (combo.SelectedItem != null)
        {
            var i = combo.SelectedItem as Customer;
            ViewModel.SelectedCustomer = i;
        }
    }

    private void CustomersComboBox_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        //ViewModel.LoadCustomerList();
        // Debug.WriteLine($"Codebehind, Customer list loaded, {ViewModel.CustomerList.Count}");
    }

    private void ProductsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        if (ViewModel.SelectedProduct != null)
        {
            ViewModel.SelectedTransactionDetail.Price = ViewModel.SelectedProduct.Price;
        }
    }


}
