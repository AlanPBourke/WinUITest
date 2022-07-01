﻿using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using WinUITest.Enums;
using WinUITest.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUITest.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class EditTransactionPage : Page
{
    private EditType TransactionEditType { get; set; }
    private EditTransactionWindowViewModel ViewModel;

    public ICommand AddCommand => new RelayCommand(NewTransactionDetail);
    public ICommand SaveCommand => new RelayCommand(Save);

    public EditTransactionPage()
    {
        this.InitializeComponent();
        ViewModel = App.Current.Services.GetService(typeof(EditTransactionWindowViewModel)) as EditTransactionWindowViewModel;
        ViewModel.Load();
    }

    public void SetEditMode(EditType edittype)
    {
        TransactionEditType = edittype;

        switch (TransactionEditType)
        {
            case EditType.Add:
                NewTransaction();
                break;
            case EditType.Edit:
                break;
            default:
                break;
        }
    }

    private void NewTransaction()
    {
        //SetMode("add");
        var newtxnvm = App.Current.Services.GetService(typeof(EditTransactionWindowViewModel)) as EditTransactionWindowViewModel;
        var newtxn = App.Current.Services.GetService(typeof(TransactionViewModel)) as TransactionViewModel;
        newtxn.SetTransaction(new Data.Transaction());
        newtxnvm.SetTransaction(newtxn);
        ViewModel = newtxnvm;
        ViewModel.IsAdding = false;
        ViewModel.IsEditing = false;
        ViewModel.IsNavigating = true;
    }

    private void NewTransactionDetail()
    {
        var newtxndetailvm = App.Current.Services.GetService(typeof(TransactionDetailViewModel)) as TransactionDetailViewModel;
        ViewModel.AddTransactionDetail();

    }

    private void Save()
    {
        ViewModel.Transaction.Save();
        ViewModel.IsEditing = false;
        ViewModel.IsAdding = false;
        ViewModel.IsNavigating = true;
    }

    private void TransactionDetailsDataGrid_SelectionChanged(object sender, Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs e)
    {

    }

    private void Page_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        //foreach (var c in ViewModel.CustomerList)
        //{
        //    var i = new ComboBoxItem { AccessKey = c.CustomerCode, Content = c.Name };
        //    CustomersComboBox.Items.Add(i);
        //}
    }

    private void CustomersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var vombo = sender as ComboBox;
        var i = vombo.SelectedItem as CustomerViewModel;
        Debug.WriteLine($"{i.CustomerCode}");
    }
}
