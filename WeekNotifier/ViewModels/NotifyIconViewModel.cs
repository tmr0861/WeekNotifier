using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Accessibility;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using WeekNotifier.Models;
using WeekNotifier.Views;

namespace WeekNotifier.ViewModels
{
    /// <summary>
    /// Class NotifyIconViewModel.
    /// Implements the <see cref="Prism.Mvvm.BindableBase" />
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public class NotifyIconViewModel : BindableBase
    {
        private readonly IContainerExtension _container;
        private BitmapSource _iconImage;
        private Window _mainView;
        private bool _canShowWindow = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyIconViewModel" /> class.
        /// </summary>
        /// <param name="calendar">The calendar.</param>
        /// <param name="container">T
        /// ]\\\he container.</param>
        public NotifyIconViewModel(Calendar calendar, IContainerExtension container)
        {
            _container = container;

            IconImage = calendar.Image;
            calendar.PropertyChanged += (_, _) => IconImage = calendar.Image;
            
            ExitCommand = new DelegateCommand(ExitApp);
            ShowWindowCommand = new DelegateCommand(ShowWindow).ObservesCanExecute(() => CanShowWindow);
            HideWindowCommand = new DelegateCommand(HideWindow, CanHideWindow);
        }

        /// <summary>
        /// Gets the show window command.
        /// </summary>
        /// <value>The show window command.</value>
        public DelegateCommand ShowWindowCommand { get; }

        /// <summary>
        /// Gets the hide window command.
        /// </summary>
        /// <value>The hide window command.</value>
        public DelegateCommand HideWindowCommand { get; }

        /// <summary>
        /// Gets the exit command.
        /// </summary>
        /// <value>The exit command.</value>
        public DelegateCommand ExitCommand { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can show window.
        /// </summary>
        /// <value><c>true</c> if this instance can show window; otherwise, <c>false</c>.</value>
        public bool CanShowWindow
        {
            get => _canShowWindow;
            set
            {
                SetProperty(ref _canShowWindow, value);
                HideWindowCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets the icon image.
        /// </summary>
        /// <value>The icon image.</value>
        public BitmapSource IconImage
        {
            get => _iconImage;
            set => SetProperty(ref _iconImage, value);
        }

        /// <summary>
        /// Gets the tool tip text.
        /// </summary>
        /// <value>The tool tip text.</value>
        public string ToolTipText => "Double-click for window, right-click for menu";

        private void ShowWindow()
        {
            _mainView = _container.Resolve<MainView>();
            _mainView.Loaded += (sender, args) => CanShowWindow = false;
            _mainView.Unloaded += (sender, args) => CanShowWindow = true;
            _mainView?.Show();
        }

        private void HideWindow()
        {
            _mainView?.Close();
        }

        private void ExitApp()
        {
            Application.Current.Shutdown();
        }

        private bool CanHideWindow()
        {
            return !CanShowWindow;
        }

    }
}
