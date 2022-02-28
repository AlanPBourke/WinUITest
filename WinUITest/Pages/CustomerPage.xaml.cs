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
using WinUITest.ViewModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUITest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerPage : Page
    {
        public MainViewModel ViewModel { get; }
        public CustomerPage()
        {
            this.InitializeComponent();
            ViewModel = new MainViewModel();
        }

        private async void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine($"query submitted:{args.QueryText}");
            System.Diagnostics.Debug.WriteLine($"text changed:{sender.Text}");

            //if (args.Reason != AutoSuggestionBoxTextChangeReason.UserInput || sender.Text == String.Empty)
            //{
            //    return;
            //}

            ViewModel.SearchCustomer(sender.Text);

            if (ViewModel.SearchResults.Count == 1)
            {
                ViewModel.SetCustomer(ViewModel.SearchResults.First().CustomerId);
            }
            else
            {
                if (ViewModel.SearchResults.Count > 1)
                {
                    await SearchResultsDialog.ShowAsync();
                }
                else
                {
                    CustomerSearchBox.Text = "No results found.";
                }
            }
        }

        private void SearchResultsDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (SearchResultsListView.SelectedItem != null)
            {
                var CustomerChosen = SearchResultsListView.SelectedItem as CustomerViewModel;
                ViewModel.SetCustomer(CustomerChosen.CustomerId);
            }
            SearchResultsDialog.Hide();
        }

    }
}
