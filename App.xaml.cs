using System;
using System.Threading.Tasks;

using Calculator.Activation;
using Calculator.Contracts.Services;
using Calculator.Core.Contracts.Services;
using Calculator.Core.Services;
using Calculator.Models;
using Calculator.Services;
using Calculator.ViewModels;
using Calculator.Views;

using LaunchActivatedEventArgs = Microsoft.UI.Xaml.LaunchActivatedEventArgs;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace Calculator
{
    public partial class App : Application
    {
        public IHost Host
        {
            get;
        }
        public static T GetService<T>()
            where T : class
        {
            if ((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
            {
                throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
            }

            return service;
        }
        public static WindowEx MainWindow { get; } = new MainWindow();

        public static UIElement? AppTitlebar
        {
            get; set;
        }
        public App()
        {
            InitializeComponent();

            Host = Microsoft.Extensions.Hosting.Host.
            CreateDefaultBuilder().
            UseContentRoot(AppContext.BaseDirectory).
            ConfigureServices((context, services) =>
            {
                // Default Activation Handler
                services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

                // Other Activation Handlers

                // Services
                services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
                services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
                services.AddTransient<INavigationViewService, NavigationViewService>();

                services.AddSingleton<IActivationService, ActivationService>();
                services.AddSingleton<IPageService, PageService>();
                services.AddSingleton<INavigationService, NavigationService>();

                // Core Services
                services.AddSingleton<IFileService, FileService>();

                // Views and ViewModels
                services.AddTransient<ApproxViewModel>();
                services.AddTransient<ApproxPage>();
                services.AddTransient<FactorialViewModel>();
                services.AddTransient<FactorialPage>();
                services.AddTransient<SettingsViewModel>();
                services.AddTransient<SettingsPage>();
                services.AddTransient<CIViewModel>();
                services.AddTransient<CIPage>();
                services.AddTransient<SIViewModel>();
                services.AddTransient<SIPage>();
                services.AddTransient<HFViewModel>();
                services.AddTransient<HFPage>();
                services.AddTransient<CalculatorViewModel>();
                services.AddTransient<CalculatorPage>();
                services.AddTransient<ShellPage>();
                services.AddTransient<ShellViewModel>();
                services.AddTransient<SplashPage>();
                services.AddTransient<SplashViewModel>();

                // Configuration
                services.Configure<LocalSettingsOptions>(context.Configuration.GetSection(nameof(LocalSettingsOptions)));
            }).
            Build();

            UnhandledException += App_UnhandledException;
        }
        private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
        {       
        }
        protected async override void OnLaunched(LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);

            // Display the splash screen Page
            var splashScreen = new SplashPage(); // Instantiate your SplashPage
            MainWindow.Content = splashScreen;
            MainWindow.Activate();

            // Wait for the specified delay
            await Task.Delay(1300);

            // Start the fade-out animation
            var fadeOutStoryboard = (Storyboard)splashScreen.Resources["FadeOutStoryboard"];
            fadeOutStoryboard.Begin();

            // Wait for the duration of the fade-out animation
            await Task.Delay(400);

            // Navigate to the main page and activate
            var shellViewModel = App.GetService<ShellViewModel>();
            MainWindow.Content = new ShellPage(shellViewModel);
            await App.GetService<IActivationService>().ActivateAsync(args);
        }
    }
}
