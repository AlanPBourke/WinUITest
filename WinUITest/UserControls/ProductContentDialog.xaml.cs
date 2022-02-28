﻿using Microsoft.UI.Xaml;
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
using WinUITest.ViewModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUITest
{
    public sealed partial class ProductContentDialog : ContentDialog
    {
        public ProductMaintenanceViewModel ViewModel;
        public ProductContentDialog(ProductMaintenanceViewModel viewModel)
        {
            ViewModel = viewModel;
            this.InitializeComponent();
        }

        private void OKHandler(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            BindingExpression bindingExpressionCode = ProductCodeTextBox.GetBindingExpression(TextBox.TextProperty);
            bindingExpressionCode.UpdateSource();
            BindingExpression bindingExpressionName = ProductNameTextBox.GetBindingExpression(TextBox.TextProperty);
            bindingExpressionName.UpdateSource();
            BindingExpression bindingExpressionPrice = ProductNameTextBox.GetBindingExpression(TextBox.TextProperty);
            bindingExpressionPrice.UpdateSource();
        }
    }
}