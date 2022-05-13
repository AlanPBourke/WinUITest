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
using WinUITest.Data;
using WinUITest.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUITest
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
           ContentFrame.NavigateToType(typeof(CustomerPage), null, new FrameNavigationOptions { IsNavigationStackEnabled = true });
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            FrameNavigationOptions navOptions = new FrameNavigationOptions();
            navOptions.TransitionInfoOverride = args.RecommendedNavigationTransitionInfo;

            Type pageType;
            string tag = args.InvokedItemContainer.Tag.ToString();

            switch (tag)
            {
                case "customers":
                default:
                    pageType = typeof(CustomerPage);
                    break;
                case "invoicing":
                    pageType = typeof(TransactionsPage);
                    break;
                case "products":
                    pageType = typeof(ProductPage);
                    break;
            }

            ContentFrame.NavigateToType(pageType, null, navOptions);
        }
    }
}
