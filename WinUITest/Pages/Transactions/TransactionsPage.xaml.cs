using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinUITest.Enums;
using WinUITest.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUITest.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class TransactionsPage : Page
{
    public ICommand AddCommand => new RelayCommand(Add);
    public TransactionsPageViewModel ViewModel { get; }
    public TransactionsPage()
    {
        this.InitializeComponent();
        ViewModel = App.Current.Services.GetService(typeof(TransactionsPageViewModel)) as TransactionsPageViewModel;
        ViewModel.Load();
    }

    private void Page_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
    }

    private void Add()
    {
        Window editTransactionWindow = new Window();
        EditTransactionPage editTransactionPage = new EditTransactionPage();
        editTransactionWindow.Content = editTransactionPage;
        editTransactionPage.SetEditMode(EditType.Add);
        editTransactionWindow.Activate();
    }

    private void TransactionsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        DataGrid g = sender as DataGrid;
        if (g != null && g.SelectedItem != null)
        {
            var txn = g.SelectedItem as TransactionViewModel;
            ViewModel.SetTransaction2(txn);
        }
        //var selectedtransaction = sender as TransactionViewModel;
        // ViewModel.SetTransaction(selectedtransaction.TransactionId);
    }
}
