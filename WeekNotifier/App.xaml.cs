using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.Extensions.Configuration;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Richter.Common.Utilities.Contracts.Services;
using Richter.Common.Utilities.Logging;
using Richter.Common.Utilities.Services;
using WeekNotifier.Contracts.Services;
using WeekNotifier.Models;
using WeekNotifier.Services;
using WeekNotifier.Views;

namespace WeekNotifier
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly TraceSource _logger = Log.Manager.AsWeekNotifier();
        private string[] _startUpArgs;

        /// <summary>
        /// Creates the shell or main window of the application.
        /// </summary>
        /// <returns>The shell of the application.</returns>
        protected override Window CreateShell()
        {
            //Need to do this before MainViewModel is constructed
            var persistAndRestoreService = Container.Resolve<IPersistAndRestoreService>();
            persistAndRestoreService.RestoreData();
            
            var calendar = Container.Resolve<Calendar>();
            calendar.RestoreSavedSettings();
            
            return Container.Resolve<MainView>();
        }

        ///// <summary>
        ///// Contains actions that should occur last.
        ///// </summary>
        //protected override async void OnInitialized()
        //{
        //    base.OnInitialized();
        //    await Task.CompletedTask;
        //}

        /// <summary>
        /// Raises the System.Windows.Application.Startup event.
        /// </summary>
        /// <param name="e">A System.Windows.StartupEventArgs that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            _startUpArgs = e.Args;
            _logger.Switch.Level = SourceLevels.All;
            base.OnStartup(e);
        }

        /// <summary>
        /// Used to register types with the container that will be used by your application.
        /// </summary>
        /// <param name="containerRegistry">The container registry.</param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var bitmap = new BitmapImage(
                new Uri("pack://application:,,,/Resources/calendar.png", UriKind.Absolute));
            containerRegistry.RegisterSingleton<Calendar>(()=> Calendar.CreateInstance(bitmap));

            // Core Services
            containerRegistry.Register<IFileService, FileService>();

            // App Services
            containerRegistry.Register<IApplicationInfoService, ApplicationInfoService>();
            containerRegistry.Register<ISystemService, SystemService>();
            containerRegistry.Register<IPersistAndRestoreService, PersistAndRestoreService>();

            // Configuration
            var configuration = BuildConfiguration();
            var appConfig = configuration
                .GetSection(nameof(AppConfig))
                .Get<AppConfig>();

            // Register configurations to IoC
            containerRegistry.RegisterInstance(configuration);
            containerRegistry.RegisterInstance(appConfig);
        }

        private IConfiguration BuildConfiguration()
        {
            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
            return new ConfigurationBuilder()
                .SetBasePath(appLocation)
                .AddJsonFile("appsettings.json")
                .AddCommandLine(_startUpArgs)
                .Build();
        }
        
        private void OnExit(object sender, ExitEventArgs e)
        {
            var persistAndRestoreService = Container.Resolve<IPersistAndRestoreService>();
            persistAndRestoreService.PersistData();
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // TODO WTS: Please log and handle the exception as appropriate to your scenario
            // For more info see https://docs.microsoft.com/dotnet/api/system.windows.application.dispatcherunhandledexception?view=netcore-3.0

            _logger.LogError(e.Exception);
            
        }
        

    }
}
