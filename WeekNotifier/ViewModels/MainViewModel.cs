using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Richter.Common.Utilities.Contracts.Services;
using Richter.Common.Utilities.Logging;
using Richter.Common.WpfUtils.Contracts;
using WeekNotifier.Models;

namespace WeekNotifier.ViewModels
{
    /// <summary>
    /// Class MainViewModel.
    /// Implements the <see cref="Prism.Mvvm.BindableBase" />
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public class MainViewModel : BindableBase, ICloseWindows
    {
        private readonly Calendar _calendar;

        private ICommand _saveSettingsCommand;
        private ICommand _cancelSettingsCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            _calendar = DesignerProperties.GetIsInDesignMode(new DependencyObject())
                ? Calendar.CreateInstance()
                : Calendar.CreateInstance(_calendar.BackgroundImage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel" /> class.
        /// </summary>
        /// <param name="calendar">The icon calendar.</param>
        /// <param name="applicationInfoService">The application information service.</param>
        public MainViewModel(Calendar calendar, IApplicationInfoService applicationInfoService)
        {
            Log.Manager.AsWeekNotifier().LogVerbose("Creating MainVM");
            AppTitle = applicationInfoService.GetProduct();
            
            _calendar = calendar;
        }

        #region Bindable Commands

        /// <summary>
        /// Gets the save settings command.
        /// </summary>
        /// <value>The save settings command.</value>
        public ICommand SaveSettingsCommand => _saveSettingsCommand
            ??= new DelegateCommand(() =>
            {
                // Save the settings and close the window
                _calendar.SaveSettings();
                Close?.Invoke();
            });

        /// <summary>
        /// Gets the cancel settings command.
        /// </summary>
        /// <value>The cancel settings command.</value>
        public ICommand CancelSettingsCommand => _cancelSettingsCommand
            ??= new DelegateCommand(() =>
            {
                Close?.Invoke();
            });

        #endregion

        /// <summary>
        /// Gets the application title.
        /// </summary>
        /// <value>The application title.</value>
        public string AppTitle { get; }
        
        /// <summary>
        /// Gets or sets the close action.
        /// </summary>
        /// <value>The close action.</value>
        public Action Close { get; set; }

        /// <summary>
        /// Determines whether the windows can close.
        /// </summary>
        /// <returns><c>true</c> if the windows can close; otherwise, <c>false</c>.</returns>
        public bool CanClose()
        {
            return true;
        }

        /// <summary>
        /// Restores the settings.
        /// </summary>
        public void RestoreSettings()
        {
            _calendar.RestoreSettings();
        }
    }
}
