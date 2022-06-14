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

        private void Add()
        {
            SetMode("add");
            ViewModel.SelectedTransaction = new TransactionViewModel(new Data.Transaction());
            ViewModel.SelectedTransaction.BeginEdit();
        }

        private void Edit()
        {
            SetMode("edit");
            ViewModel.SelectedTransaction.BeginEdit();
        }

        private void Save()
        {
            if (ViewModel.SelectedTransaction.HasErrors == false)
            {
                ViewModel.SelectedTransaction.Save();
                ViewModel.SelectedTransaction.EndEdit();
                ViewModel.Load();
                ViewModel.SetTransaction(ViewModel.SelectedTransaction.TransactionId);
                SetMode("navigate");
            }
        }

        private void Cancel()
        {
            if (ViewModel.IsEditing)
            {
                ViewModel.SelectedTransaction.CancelEdit();
            }
            else
            {
                ViewModel.SetTransaction(ViewModel.Transactions[0].TransactionId);
            }

            if (ViewModel.SelectedTransaction != null)
            {
                ViewModel.SetTransaction(ViewModel.SelectedTransaction.TransactionId);
            }

            SetMode("navigate");
        }

        private void DeleteConfirmationClick(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {

            //if (InfoViewModel.SelectedCustomer.HasErrors == false)
            //{
            if (ViewModel.CanDelete())
            {
                ViewModel.SelectedTransaction.Delete();
                ViewModel.Load();
                ViewModel.SetFirstTransaction();
            }
            else
            {
                UserMaintenanceInAppNotification.Show("This transaction cannot be deleted.", 0);
            }
            DeleteButton.Flyout.Hide();
            SetMode("navigate");
        }

        private void DeleteCancelClick(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.IsAdding = false;
            ViewModel.IsNavigating = true;
            ViewModel.IsEditing = false;
            DeleteButton.Flyout.Hide();
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
