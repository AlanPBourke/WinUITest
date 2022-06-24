// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using WinUITest.Enums;
using WinUITest.ViewModels;

namespace WinUITest.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class EditTransactionWindow : Window
{
    private EditType TransactionEditType { get; set; }
    private EditTransactionWindowViewModel ViewModel;
    public ICommand AddCommand => new RelayCommand(Add);
    //public ICommand EditCommand => new RelayCommand(Edit);
    //public ICommand SaveCommand => new RelayCommand(Save);
    //public ICommand CancelCommand => new RelayCommand(Cancel);

    public EditTransactionWindow()
    {
        this.InitializeComponent();
        ViewModel = App.Current.Services.GetService(typeof(EditTransactionWindowViewModel)) as EditTransactionWindowViewModel;
    }

    public void SetEditMode(EditType edittype)
    {
        TransactionEditType = edittype;
    }

    private void Window_Activated(object sender, WindowActivatedEventArgs args)
    {
        switch (TransactionEditType)
        {
            case EditType.Add:
                Add();
                break;
            case EditType.Edit:
                break;
            default:
                break;
        }
    }

    private void TransactionDetailsDataGrid_SelectionChanged(object sender, Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs e)
    {

    }

    private void Add()
    {
        //SetMode("add");
        var newtxnvm = App.Current.Services.GetService(typeof(EditTransactionWindowViewModel)) as EditTransactionWindowViewModel;
        var newtxn = App.Current.Services.GetService(typeof(TransactionViewModel)) as TransactionViewModel;
        newtxn.SetTransaction(new Data.Transaction());
        newtxnvm.SetTransaction(newtxn);
        ViewModel = newtxnvm;
        ViewModel.IsAdding = true;
        ViewModel.IsEditing = false;


    }

    //private void Edit()
    //{
    //    SetMode("edit");
    //    ViewModel.SelectedCustomer.BeginEdit();
    //}

    //private void Save()
    //{
    //    if (ViewModel.SelectedCustomer.HasErrors == false)
    //    {
    //        ViewModel.SelectedCustomer.Save();
    //        ViewModel.SelectedCustomer.EndEdit();
    //        ViewModel.Load();
    //        ViewModel.SetCustomer(ViewModel.SelectedCustomer.CustomerId);
    //        SetMode("navigate");
    //    }
    //}

    //private void Cancel()
    //{
    //    if (ViewModel.IsEditing)
    //    {
    //        ViewModel.SelectedCustomer.CancelEdit();
    //    }
    //    else
    //    {
    //        ViewModel.SetCustomer(ViewModel.Customers[0].CustomerId);
    //    }

    //    if (ViewModel.SelectedCustomer != null)
    //    {
    //        ViewModel.SetCustomer(ViewModel.SelectedCustomer.CustomerId);
    //    }

    //    SetMode("navigate");
    //}

    //private void SetMode(string mode)
    //{
    //    switch (mode)
    //    {
    //        case "navigate":
    //        default:
    //            ViewModel.IsNavigating = true;
    //            ViewModel.IsAdding = false;
    //            ViewModel.IsEditing = false;
    //            break;
    //        case "add":
    //            ViewModel.IsNavigating = false;
    //            ViewModel.IsAdding = true;
    //            ViewModel.IsEditing = false;
    //            break;
    //        case "edit":
    //            ViewModel.IsNavigating = false;
    //            ViewModel.IsAdding = false;
    //            ViewModel.IsEditing = true;
    //            break;
    //    }
    //}


}

