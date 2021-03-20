using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
        private readonly TraceSource _logger = Log.Manager.AsWeekNotifier();
        private readonly IApplicationInfoService _applicationInfoService;

        private readonly Calendar _iconCalendar;
        private readonly Calendar _sampleCalendar;

        private ICommand _saveSettingsCommand;
        private ICommand _cancelSettingsCommand;
        
        private BitmapSource _iconImage;
        private BitmapSource _sampleImage;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            _sampleCalendar = DesignerProperties.GetIsInDesignMode(new DependencyObject())
                ? Calendar.CreateInstance()
                : Calendar.CreateInstance(_iconCalendar.BackgroundImage);

            SampleImage = _sampleCalendar.Image;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="iconCalendar">The icon calendar.</param>
        /// <param name="applicationInfoService">The application information service.</param>
        public MainViewModel(Calendar iconCalendar, IApplicationInfoService applicationInfoService)
        {
            _logger.LogVerbose("MainViewModel Construction");

            _applicationInfoService = applicationInfoService;
            
            _iconCalendar = iconCalendar;
            _iconImage = _iconCalendar.Image;

            _sampleCalendar = Calendar.CreateInstance(_iconCalendar.BackgroundImage);

            TextSize = _iconCalendar.TextSize;
            BackgroundColor = _iconCalendar.BackgroundColor;
            TextColor = _iconCalendar.TextColor;
            SampleImage = _sampleCalendar.Image;
        }

        #region Bindable Commands

        /// <summary>
        /// Gets the save settings command.
        /// </summary>
        /// <value>The save settings command.</value>
        public ICommand SaveSettingsCommand => _saveSettingsCommand
            ??= new DelegateCommand(() =>
            {
                _logger.LogInformation("Saving calendar properties");

                // Update the current Calendar values
                _iconCalendar.BackgroundColor = BackgroundColor;
                _iconCalendar.TextColor = TextColor;
                _iconCalendar.TextSize = TextSize;
                IconImage = _iconCalendar.Image;

                // Save the settings
                Application.Current.Properties[nameof(TextSize)] = TextSize;
                Application.Current.Properties[nameof(TextColor)] = TextColor;
                Application.Current.Properties[nameof(BackgroundColor)] = BackgroundColor;

                // Close the window
                //Close?.Invoke();
            });

        /// <summary>
        /// Gets the cancel settings command.
        /// </summary>
        /// <value>The cancel settings command.</value>
        public ICommand CancelSettingsCommand => _cancelSettingsCommand
            ??= new DelegateCommand(() =>
            {
                _logger.LogInformation("Restoring calendar properties");

                // Restore the initial settings
                BackgroundColor = _iconCalendar.BackgroundColor;
                TextColor = _iconCalendar.TextColor;
                TextSize = _iconCalendar.TextSize;

                // Close the window
                //Close?.Invoke();
            });
        
        #endregion
        
        /// <summary>
        /// Gets or sets the sample week number.
        /// </summary>
        /// <value>The sample week number.</value>
        public int WeekNumber
        {
            get => _sampleCalendar?.WeekNumber ?? 53;
            set
            {
                if (_sampleCalendar.WeekNumber == value) return;

                _sampleCalendar.WeekNumber = value;
                SampleImage = _sampleCalendar.Image;
            }
        }

        /// <summary>
        /// Gets or sets the size of the text.
        /// </summary>
        /// <value>The size of the text.</value>
        public int TextSize
        {
            get => _sampleCalendar?.TextSize ?? 42;
            set
            {
                if (value == _sampleCalendar.TextSize) return;

                _sampleCalendar.TextSize = value;
                RaisePropertyChanged();

                SampleImage = _sampleCalendar.Image;
            }
        }

        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
        public Color BackgroundColor
        {
            get => _sampleCalendar.BackgroundColor;
            set
            {
                if (value.Equals(_sampleCalendar.BackgroundColor)) return;

                _sampleCalendar.BackgroundColor = value;
                RaisePropertyChanged();

                SampleImage = _sampleCalendar.Image;
            }
        }

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        public Color TextColor
        {
            get => _sampleCalendar.TextColor;
            set
            {
                if (value.Equals(_sampleCalendar.TextColor)) return;

                _sampleCalendar.TextColor = value;
                RaisePropertyChanged();

                SampleImage = _sampleCalendar.Image;
            }
        }

        /// <summary>
        /// Gets or sets the sample image.
        /// </summary>
        /// <value>The sample image.</value>
        public BitmapSource SampleImage
        {
            get => _sampleImage;
            set => SetProperty(ref _sampleImage, value);
        }

        /// <summary>
        /// Gets or sets the current image.
        /// </summary>
        /// <value>The current image.</value>
        public BitmapSource IconImage
        {
            get => _iconImage;
            set => SetProperty(ref _iconImage, value);
        }

        #region About Information Properties

        /// <summary>
        /// Gets the application title.
        /// </summary>
        /// <value>The application title.</value>
        public string AppTitle => _applicationInfoService.GetProduct();
        
        /// <summary>
        /// Gets or sets the version description.
        /// </summary>
        /// <value>The version description.</value>
        public string VersionDescription => $"Version: {_applicationInfoService.GetVersion()}";

        /// <summary>
        /// Gets or sets the application description.
        /// </summary>
        /// <value>The application description.</value>
        public string AppDescription => _applicationInfoService.GetDescription();

        /// <summary>
        /// Gets or sets the copyright statement.
        /// </summary>
        /// <value>The copyright statement.</value>
        public string Copyright
        {
            get
            {
                var sb = new StringBuilder(_applicationInfoService.GetCopyright());

                sb.Append($" {_applicationInfoService.GetCompany()} All Rights Reserved.");

                return sb.ToString();
            }
        }

        #endregion

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

    }
}
