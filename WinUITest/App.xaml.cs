﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using WinUITest.Data;
using WinUITest.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUITest
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public static IDataProvider DataProvider { get; set; }
        public IServiceProvider Services { get; }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            // Can't see textbox caret unless Dark in App SDK 1.1 !
            App.Current.RequestedTheme = ApplicationTheme.Dark;
            DataProvider = new SqliteDataProvider();
            Services = ConfigureServices();
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }

        private Window m_window;

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<CustomerPageViewModel>();
            services.AddSingleton<TransactionsPageViewModel>();
            services.AddSingleton(new CustomerViewModel(new Customer()));
            services.AddSingleton<ProductPageViewModel>();
            services.AddSingleton(new ProductViewModel(new Product()));
            return services.BuildServiceProvider();
        }
    }
}
