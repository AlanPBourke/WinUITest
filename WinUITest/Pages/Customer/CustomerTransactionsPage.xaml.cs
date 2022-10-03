using Microsoft.UI.Xaml.Controls;
using WinUITest.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUITest.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class CustomerTransactionsPage : Page
{
    public CustomerPageViewModel ViewModel { get; }

    public CustomerTransactionsPage()
    {
        this.InitializeComponent();
        ViewModel = App.Current.Services.GetService(typeof(CustomerPageViewModel)) as CustomerPageViewModel;
        ViewModel.Load();
        DataContext = ViewModel;
    }
}


//using Microsoft.UI.Xaml;
//using Microsoft.UI.Xaml.Controls;
//using WinUITest.ViewModels;

//// To learn more about WinUI, the WinUI project structure,
//// and more about our project templates, see: http://aka.ms/winui-project-info.

//namespace WinUITest.Pages;

///// <summary>
///// An empty page that can be used on its own or navigated to within a Frame.
///// </summary>
//public sealed partial class CustomerTransactionsPage : Page
//{
//    public static readonly DependencyProperty TransactionsViewModelProperty
//        = DependencyProperty.Register(nameof(TransactionsViewModel),
//            typeof(CustomerPageViewModel),
//            typeof(CustomerTransactionsPage),
//            new PropertyMetadata(null));

//    public CustomerPageViewModel TransactionsViewModel
//    {
//        get { return (CustomerPageViewModel)GetValue(TransactionsViewModelProperty); }
//        set { SetValue(TransactionsViewModelProperty, value); }
//    }
//    public CustomerTransactionsPage()
//    {
//        this.InitializeComponent();
//    }
//}
