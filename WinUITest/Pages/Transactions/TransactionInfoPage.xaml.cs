using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using WinUITest.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUITest.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TransactionInfoPage : Page
    {

        public ICommand AddCommand => new RelayCommand(Add);
        public ICommand EditCommand => new RelayCommand(Edit);
        public ICommand SaveCommand => new RelayCommand(Save);
        public ICommand CancelCommand => new RelayCommand(Cancel);

        public TransactionsPageViewModel ViewModel { get; }

        public TransactionInfoPage()
        {
            this.InitializeComponent();
            ViewModel = App.Current.Services.GetService(typeof(TransactionsPageViewModel)) as TransactionsPageViewModel;
            ViewModel.Load();
            DataContext = ViewModel;
            SetMode("navigate");
        }

        private void SetMode(string mode)
        {
            switch (mode)
            {
                case "navigate":
                default:
                    ViewModel.IsNavigating = true;
                    ViewModel.IsAdding = false;
                    ViewModel.IsEditing = false;
                    break;
                case "add":
                    ViewModel.IsNavigating = false;
                    ViewModel.IsAdding = true;
                    ViewModel.IsEditing = false;
                    break;
                case "edit":
                    ViewModel.IsNavigating = false;
                    ViewModel.IsAdding = false;
                    ViewModel.IsEditing = true;
                    break;
            }
        }
    }
}
