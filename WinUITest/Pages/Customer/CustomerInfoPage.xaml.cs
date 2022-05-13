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
        public CustomerMaintenanceViewModel CustomerInfoViewModel { get; }

        public CustomerInfoPage()
        {
            this.InitializeComponent();
            CustomerInfoViewModel = App.Current.Services.GetService(typeof(CustomerMaintenanceViewModel)) as CustomerMaintenanceViewModel;
            CustomerInfoViewModel.Load();
            DataContext = CustomerInfoViewModel;
        }
        private void Add()
        {
            SetMode("add");
            CustomerInfoViewModel.SelectedCustomer = new CustomerViewModel(new Data.Customer());
            CustomerInfoViewModel.SelectedCustomer.BeginEdit();
        }

        private void Edit()
        {
            SetMode("edit");
            CustomerInfoViewModel.SelectedCustomer.BeginEdit();
        }

        private void SaveChanges()
        {
            if (CustomerInfoViewModel.SelectedCustomer.HasErrors == false)
            {
                CustomerInfoViewModel.SelectedCustomer.Save();
                CustomerInfoViewModel.SelectedCustomer.EndEdit();
                CustomerInfoViewModel.Load();
                CustomerInfoViewModel.SetCustomer(CustomerInfoViewModel.SelectedCustomer.CustomerId);
                SetMode("navigate");
            }
        }

        private void CancelChanges()
        {
            if (CustomerInfoViewModel.IsEditing)
            {
                CustomerInfoViewModel.SelectedCustomer.CancelEdit();
            }
            else
            {
                CustomerInfoViewModel.SetCustomer(CustomerInfoViewModel.Customers[0].CustomerId);
            }

            if (CustomerInfoViewModel.SelectedCustomer != null)
            {
                CustomerInfoViewModel.SetCustomer(CustomerInfoViewModel.SelectedCustomer.CustomerId);
            }

            SetMode("navigate");
        }

        private void DeleteConfirmationClick(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {

            //if (InfoViewModel.SelectedCustomer.HasErrors == false)
            //{
            if (CustomerInfoViewModel.CanDelete())
            {
                CustomerInfoViewModel.SelectedCustomer.Delete();
                CustomerInfoViewModel.Load();
                CustomerInfoViewModel.SetFirstCustomer();
            }
            else
            {
                UserMaintenanceInAppNotification.Show("This customer has transactions and cannot be deleted.", 0);
            }
            DeleteButton.Flyout.Hide();
            SetMode("navigate");
        }

        private void DeleteCancelClick(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            CustomerInfoViewModel.IsAdding = false;
            CustomerInfoViewModel.IsNavigating = true;
            CustomerInfoViewModel.IsEditing = false;
            DeleteButton.Flyout.Hide();
            SetMode("navigate");
        }

        private void SetMode(string mode)
        {
            switch (mode)
            {
                case "navigate":
                default:
                    CustomerInfoViewModel.IsNavigating = true;
                    CustomerInfoViewModel.IsAdding = false;
                    CustomerInfoViewModel.IsEditing = false;
                    break;
                case "add":
                    CustomerInfoViewModel.IsNavigating = false;
                    CustomerInfoViewModel.IsAdding = true;
                    CustomerInfoViewModel.IsEditing = false;
                    break;
                case "edit":
                    CustomerInfoViewModel.IsNavigating = false;
                    CustomerInfoViewModel.IsAdding = false;
                    CustomerInfoViewModel.IsEditing = true;
                    break;
            }
        }
    }
}
