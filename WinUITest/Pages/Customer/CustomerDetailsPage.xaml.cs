using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using WinUITest.Data;
using WinUITest.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUITest.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class CustomerDetailsPage : Page
{
    //public ICommand EditCommand => new AsyncRelayCommand(OpenEditDialog);
    public ICommand AddCommand => new RelayCommand(Add);
    public ICommand EditCommand => new RelayCommand(Edit);
    public ICommand SaveCommand => new RelayCommand(Save);
    public ICommand CancelCommand => new RelayCommand(Cancel);
    public CustomerPageViewModel ViewModel { get; }

    public CustomerDetailsPage()
    {
        this.InitializeComponent();
        ViewModel = App.Current.Services.GetService(typeof(CustomerPageViewModel)) as CustomerPageViewModel;
        ViewModel.Load();
        DataContext = ViewModel;
        SetMode("navigate");
    }
    private void Add()
    {
        SetMode("add");
        var newcust = App.Current.Services.GetService(typeof(CustomerViewModel)) as CustomerViewModel;
        newcust.SetCustomer(new Customer());
        ViewModel.SelectedCustomer = newcust;
        ViewModel.SelectedCustomer.BeginEdit();
    }

    private void Edit()
    {
        SetMode("edit");
        ViewModel.SelectedCustomer.BeginEdit();
    }

    private void Save()
    {
        if (ViewModel.SelectedCustomer.HasErrors == false)
        {
            ViewModel.SelectedCustomer.Save();
            ViewModel.SelectedCustomer.EndEdit();
            ViewModel.Load();
            ViewModel.SetCustomer(ViewModel.SelectedCustomer.CustomerId);
            SetMode("navigate");
        }
    }

    private void Cancel()
    {
        if (ViewModel.IsEditing)
        {
            ViewModel.SelectedCustomer.CancelEdit();
        }
        else
        {
            ViewModel.SetCustomer(ViewModel.Customers[0].CustomerId);
        }

        if (ViewModel.SelectedCustomer != null)
        {
            ViewModel.SetCustomer(ViewModel.SelectedCustomer.CustomerId);
        }

        SetMode("navigate");
    }

    private void DeleteConfirmationClick(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {

        //if (InfoViewModel.SelectedCustomer.HasErrors == false)
        //{
        if (ViewModel.CanDelete())
        {
            ViewModel.SelectedCustomer.Delete();
            ViewModel.Load();
            ViewModel.SetFirstCustomer();
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
