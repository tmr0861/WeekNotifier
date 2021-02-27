using System.Diagnostics;
using System.Windows;
using EEVCNA.Common.Utilities.Logging;
using Hardcodet.Wpf.TaskbarNotification;

namespace WeekNotifier.GDI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon _notifyIcon;
        private NotifyIconViewModel _notifyIconViewModel;

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Log.Manager.AsWeekNotifier().Switch.Level = SourceLevels.All;

            //create the NotifyIcon (it's a resource declared in NotifyIconResources.xaml
            _notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            
            if (_notifyIcon == null) return;

            _notifyIconViewModel = new NotifyIconViewModel();
            _notifyIcon.DataContext = _notifyIconViewModel;

            // Since I can't figure out how to convert Icon to an ImageSource to data bind it
            // I need to have an event raised when the week number changes to refresh the icon
            _notifyIconViewModel.RefreshIcon += NotifyIconViewModel_RefreshIcon;
            _notifyIconViewModel.Start();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Exit" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.Windows.ExitEventArgs" /> that contains the event data.</param>
        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon?.Dispose(); //the icon would clean up automatically, but this is cleaner
            base.OnExit(e);
        }

        private void NotifyIconViewModel_RefreshIcon()
        {
            _notifyIcon.Icon = _notifyIconViewModel?.GetIcon();
        }

    }
}
