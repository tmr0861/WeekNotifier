using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;

namespace WeekNumberToast
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon _notifyIcon;
        private NotifyIconViewModel _notifyIconViewModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //create the NotifyIcon (it's a resource declared in NotifyIconResources.xaml
            _notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            _notifyIconViewModel = new NotifyIconViewModel();
            
            if (_notifyIcon == null) return;
            _notifyIcon.DataContext = _notifyIconViewModel;
            _notifyIcon.Icon = _notifyIconViewModel.GetIcon(0);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon.Dispose(); //the icon would clean up automatically, but this is cleaner
            base.OnExit(e);
        }
    }
}
