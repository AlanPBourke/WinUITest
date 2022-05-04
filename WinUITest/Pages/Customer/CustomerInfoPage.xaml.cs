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
            InfoViewModel.CustomerCodeError = false;
            InfoViewModel.CustomerNameError = false;
            InfoViewModel.IsAdding = true;
            InfoViewModel.IsNavigating = false;
            InfoViewModel.IsEditing = false;
            InfoViewModel.SelectedCustomer = new CustomerViewModel(new Data.Customer());
            InfoViewModel.SelectedCustomer.BeginEdit();
        }

        private void Edit()
        {
            InfoViewModel.IsAdding = false;
            InfoViewModel.IsNavigating = false;
            InfoViewModel.IsEditing = true;
            InfoViewModel.CustomerCodeError = false;
            InfoViewModel.CustomerNameError = false;
            InfoViewModel.SelectedCustomer.BeginEdit();
        }

        private void SaveChanges()
        {
            if (InfoViewModel.Validate())
            {
                InfoViewModel.IsAdding = false;
                InfoViewModel.IsNavigating = true;
                InfoViewModel.IsEditing = false;
                InfoViewModel.SelectedCustomer.Save();
                InfoViewModel.SelectedCustomer.EndEdit();
                InfoViewModel.Load();
                InfoViewModel.SetCustomer(InfoViewModel.SelectedCustomer.CustomerId);
                //ResetToolbar();
                InfoViewModel.CustomerCodeError = false;
                InfoViewModel.CustomerNameError = false;
            }
        }

        private void CancelChanges()
        {
            InfoViewModel.IsAdding = false;
            InfoViewModel.IsNavigating = true;
            InfoViewModel.IsEditing = false;
            InfoViewModel.CustomerCodeError = false;
            InfoViewModel.CustomerNameError = false;
            //InfoViewModel.IsAdding = false;

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

            // ResetToolbar();
        }

        //private void ResetToolbar()
        //{
        //    SaveButton.Visibility = Visibility.Collapsed;
        //    CancelButton.Visibility = Visibility.Collapsed;
        //    //EditButton.Visibility = Visibility.Visible;
        //    DeleteButton.Visibility = Visibility.Visible;
        //}

        private void DeleteConfirmationClick(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            InfoViewModel.IsAdding = false;
            InfoViewModel.IsNavigating = true;
            InfoViewModel.IsEditing = false;

            if (InfoViewModel.CanDelete())
            {
                InfoViewModel.DeleteCustomer();
                InfoViewModel.Load();
                InfoViewModel.SetFirstCustomer();
            }
            DeleteButton.Flyout.Hide();
        }

        private void DeleteCancelClick(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            InfoViewModel.IsAdding = false;
            InfoViewModel.IsNavigating = true;
            InfoViewModel.IsEditing = false;
            DeleteButton.Flyout.Hide();
        }
    }
}
