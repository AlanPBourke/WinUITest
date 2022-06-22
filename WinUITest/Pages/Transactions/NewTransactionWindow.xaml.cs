// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using Microsoft.UI.Xaml;
using WinUITest.ViewModels;

namespace WinUITest.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class NewTransactionWindow : Window
{
    private NewTransactionViewModel ViewModel;
    public NewTransactionWindow()
    {
        this.InitializeComponent();
        ViewModel = App.Current.Services.GetService(typeof(NewTransactionViewModel)) as NewTransactionViewModel;
        ViewModel.Load();
    }

    private void TransactionDetailsDataGrid_SelectionChanged(object sender, Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs e)
    {

    }
}

