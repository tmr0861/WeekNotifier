// ***********************************************************************
// Assembly         : WeekNotifier
// Author           : Tom Richter
// Created          : 03-20-2021
//
// Last Modified By : Tom Richter
// Last Modified On : 08-15-2021
// ***********************************************************************
// <copyright file="NotifyIconViewModel.cs" company="Tom Richter">
//     Copyright (c) 2005-2021
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Windows;
using System.Windows.Media.Imaging;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Richter.Common.Utilities.Logging;
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
        private bool _canShowSettings = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyIconViewModel" /> class.
        /// </summary>
        /// <param name="calendar">The calendar.</param>
        /// <param name="container">T
        /// ]\\\he container.</param>
        public NotifyIconViewModel(Calendar calendar, IContainerExtension container)
        {
            Log.Manager.AsWeekNotifier().LogVerbose("Creating NotifyIconVM");
            _container = container;

            IconImage = calendar.Image;
            calendar.PropertyChanged += (_, _) => IconImage = calendar.Image;
            
            ExitCommand = new DelegateCommand(ExitApp);
            LoadSettingsCommand = new DelegateCommand(LoadSettings).ObservesCanExecute(() => CanShowSettings);
            CloseSettingsCommand = new DelegateCommand(CloseSettings, CanCloseSettings);

            calendar.AutoUpdate = true;
        }

        /// <summary>
        /// Gets the load settings window command.
        /// </summary>
        /// <value>The load settings window command.</value>
        public DelegateCommand LoadSettingsCommand { get; }

        /// <summary>
        /// Gets the close settings window command.
        /// </summary>
        /// <value>The close settings window command.</value>
        public DelegateCommand CloseSettingsCommand { get; }

        /// <summary>
        /// Gets the exit command.
        /// </summary>
        /// <value>The exit command.</value>
        public DelegateCommand ExitCommand { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can show window.
        /// </summary>
        /// <value><c>true</c> if this instance can show window; otherwise, <c>false</c>.</value>
        public bool CanShowSettings
        {
            get => _canShowSettings;
            set
            {
                SetProperty(ref _canShowSettings, value);
                CloseSettingsCommand.RaiseCanExecuteChanged();
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
        public string ToolTipText => "Double-click for Settings window, right-click for menu";

        /// <summary>
        /// Gets the load settings menu text.
        /// </summary>
        /// <value>The load settings text.</value>
        public string LoadSettingsMenuText => "Show Settings";

        /// <summary>
        /// Gets the load settings menu tool tip text.
        /// </summary>
        /// <value>The load settings menu tool tip text.</value>
        public string LoadSettingsMenuToolTipText => "Show the settings window";

        /// <summary>
        /// Gets the close settings menu text.
        /// </summary>
        /// <value>The close settings menu text.</value>
        public string CloseSettingsMenuText => "Close Settings";

        /// <summary>
        /// Gets the close settings menu tool tip text.
        /// </summary>
        /// <value>The close settings menu tool tip text.</value>
        public string CloseSettingsMenuToolTipText => "Close the settings window";

        /// <summary>
        /// Gets the exit application menu text.
        /// </summary>
        /// <value>The exit application menu text.</value>
        public string ExitAppMenuText => "Exit";

        /// <summary>
        /// Gets the exit application menu tool tip text.
        /// </summary>
        /// <value>The exit application menu tool tip text.</value>
        public string ExitAppMenuToolTipText => "Exit the Application";

        private void LoadSettings()
        {
            _mainView = _container.Resolve<MainView>();
            _mainView.IsVisibleChanged += (o, args) => CanShowSettings = !(bool) args.NewValue;
            _mainView?.Show();
        }

        private void CloseSettings()
        {
            _mainView?.Close();
        }

        private void ExitApp()
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseSettings()
        {
            return !CanShowSettings;
        }

    }
}
