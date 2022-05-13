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
using WinUITest.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUITest.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerTransactionsPage : Page
    {
        public static readonly DependencyProperty TransactionsViewModelProperty
            = DependencyProperty.Register(nameof(TransactionsViewModel), 
                typeof(CustomerPageViewModel), 
                typeof(CustomerTransactionsPage), 
                new PropertyMetadata(null));
        
        public CustomerPageViewModel TransactionsViewModel
        {
            get { return (CustomerPageViewModel)GetValue(TransactionsViewModelProperty); } 
            set { SetValue(TransactionsViewModelProperty, value); }
        }
        public CustomerTransactionsPage()
        {
            this.InitializeComponent();
        }
    }
}
