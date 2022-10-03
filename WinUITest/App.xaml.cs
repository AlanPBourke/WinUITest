using System;
using System.Globalization;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using WinUITest.Data;
using WinUITest.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUITest;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
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
        //App.Current.RequestedTheme = ApplicationTheme.Dark;

        Services = ConfigureServices();
        CultureInfo.DefaultThreadCurrentCulture = Thread.CurrentThread.CurrentCulture;
        this.InitializeComponent();
        UnhandledException += App_UnhandledException;
    }

    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"Unhandled: {e.Exception.Message}");

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

        // -- As long is this is registered here then it does not need 
        // -- to be passed to ViewModel constructors.
        // -- See https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection
        services.AddSingleton<IDataProvider, SqliteDataProvider>();

        // Singletons to facilitate use of e.g. the custmerpageviewmodel on the customer details and transactions
        // tabs.
        services.AddSingleton<CustomerPageViewModel>();
        services.AddSingleton<ProductPageViewModel>();
        services.AddSingleton<TransactionsPageViewModel>();

        services.AddTransient<Transaction>();
        services.AddTransient<TransactionDetail>();
        services.AddTransient<CustomerViewModel>();
        services.AddTransient<ProductViewModel>();
        services.AddTransient<TransactionViewModel>();
        services.AddTransient<TransactionDetailViewModel>();
        services.AddTransient<EditTransactionWindowViewModel>();

        //services.AddSingleton(new ProductViewModel(new Product()));
        //services.AddSingleton(new SqliteDataProvider());


        return services.BuildServiceProvider();
    }
}
