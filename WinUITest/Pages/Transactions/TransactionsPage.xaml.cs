using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
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
        EditTransactionWindow NewWindow = new EditTransactionWindow();

        NewWindow.Activate();
    }
}

