using Microsoft.Toolkit.Mvvm.Input;
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
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinUITest.Data;
using WinUITest.UserControls;
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
        public ICommand EditCommand => new RelayCommand(BeginEdit);
        public ICommand SaveCommand => new RelayCommand(SaveChanges);
        public ICommand CancelCommand => new RelayCommand(CancelChanges);
        public ICommand DeleteCommand => new AsyncRelayCommand(DeleteCustomer);

        public CustomerMaintenanceViewModel InfoViewModel { get;}

        public CustomerInfoPage()
        {
            this.InitializeComponent();
            InfoViewModel = App.Current.Services.GetService(typeof(CustomerMaintenanceViewModel)) as CustomerMaintenanceViewModel;
            InfoViewModel.Load();
            DataContext = InfoViewModel;
        }

        private void BeginEdit()
        {
            //EditInfoViewModel = InfoViewModel.SelectedCustomer;
            SaveButton.Visibility = Visibility.Visible;
            CancelButton.Visibility = Visibility.Visible;
            EditButton.Visibility = Visibility.Collapsed;
            DeleteButton.Visibility = Visibility.Collapsed;
            InfoViewModel.IsEditing = true;
            InfoViewModel.SelectedCustomer.BeginEdit();
        }

        private void SaveChanges()
        {
            InfoViewModel.SelectedCustomer.Save();
            InfoViewModel.SelectedCustomer.EndEdit();
            InfoViewModel.IsEditing = false;
            ResetToolbar();
        }

        private void CancelChanges()
        {
            InfoViewModel.IsEditing = false;
            InfoViewModel.SelectedCustomer.CancelEdit();
            ResetToolbar();
        }

        private void ResetToolbar()
        {
            SaveButton.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = Visibility.Collapsed;
            EditButton.Visibility = Visibility.Visible;
            DeleteButton.Visibility = Visibility.Visible;
        }

        private Task DeleteCustomer()
        {
            throw new NotImplementedException();
        }

    }
}
