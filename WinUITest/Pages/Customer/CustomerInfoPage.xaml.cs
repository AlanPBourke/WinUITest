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
    public sealed partial class CustomerInfoPage : Page
    {
        //public ICommand EditCommand => new AsyncRelayCommand(OpenEditDialog);
        public ICommand AddCommand => new RelayCommand(Add);
        public ICommand EditCommand => new RelayCommand(Edit);
        public ICommand SaveCommand => new RelayCommand(SaveChanges);
        public ICommand CancelCommand => new RelayCommand(CancelChanges);
        public CustomerMaintenanceViewModel InfoViewModel { get; }

        public CustomerInfoPage()
        {
            this.InitializeComponent();
            InfoViewModel = App.Current.Services.GetService(typeof(CustomerMaintenanceViewModel)) as CustomerMaintenanceViewModel;
            InfoViewModel.Load();
            DataContext = InfoViewModel;
        }
        private void Add()
        {
            SetMode("add");
            InfoViewModel.SelectedCustomer = new CustomerViewModel(new Data.Customer());
            InfoViewModel.SelectedCustomer.BeginEdit();
        }

        private void Edit()
        {
            SetMode("edit");
            InfoViewModel.SelectedCustomer.BeginEdit();
        }

        private void SaveChanges()
        {
            if (InfoViewModel.Validate())
            {
                InfoViewModel.SelectedCustomer.Save();
                InfoViewModel.SelectedCustomer.EndEdit();
                InfoViewModel.Load();
                InfoViewModel.SetCustomer(InfoViewModel.SelectedCustomer.CustomerId);
                SetMode("navigate");
            }
        }

        private void CancelChanges()
        {
            if (InfoViewModel.IsEditing)
            {
                InfoViewModel.SelectedCustomer.CancelEdit();
            }
            else
            {
                InfoViewModel.SetCustomer(InfoViewModel.Customers[0].CustomerId);
            }

            if (InfoViewModel.SelectedCustomer != null)
            {
                InfoViewModel.SetCustomer(InfoViewModel.SelectedCustomer.CustomerId);
            }

            SetMode("navigate");
        }

        private void DeleteConfirmationClick(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (InfoViewModel.CanDelete())
            {
                InfoViewModel.DeleteCustomer();
                InfoViewModel.Load();
                InfoViewModel.SetFirstCustomer();
            }
            DeleteButton.Flyout.Hide();
            SetMode("navigate");
        }

        private void DeleteCancelClick(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            InfoViewModel.IsAdding = false;
            InfoViewModel.IsNavigating = true;
            InfoViewModel.IsEditing = false;
            DeleteButton.Flyout.Hide();
            SetMode("navigate");
        }

        private void SetMode(string mode)
        {
            switch (mode)
            {
                case "navigate":
                default:
                    InfoViewModel.IsNavigating = true;
                    InfoViewModel.IsAdding = false;
                    InfoViewModel.IsEditing = false;
                    break;
                case "add":
                    InfoViewModel.IsNavigating = false;
                    InfoViewModel.IsAdding = true;
                    InfoViewModel.IsEditing = false;
                    break;
                case "edit":
                    InfoViewModel.IsNavigating = false;
                    InfoViewModel.IsAdding = false;
                    InfoViewModel.IsEditing = true;
                    break;
            }

            InfoViewModel.CustomerCodeError = false;
            InfoViewModel.CustomerNameError = false;

        }
    }
}
