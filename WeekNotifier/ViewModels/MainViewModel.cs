using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Prism.Commands;
using Prism.Mvvm;
using Richter.Common.Utilities.Logging;
using WeekNotifier.Contracts;
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
        private readonly Calendar _currentCalendar;
        private readonly Calendar _sampleCalendar;

        private DelegateCommand _saveSettingsCommand;
        private DelegateCommand _cancelSettingsCommand;
        private BitmapSource _currentImage;
        private BitmapSource _sampleImage;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            _sampleCalendar = DesignerProperties.GetIsInDesignMode(new DependencyObject()) 
                ? Calendar.CreateInstance() 
                : Calendar.CreateInstance(_currentCalendar.BackgroundImage);

            SampleImage = _sampleCalendar.Image;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel(Calendar currentCalendar)
        {
            _logger.LogVerbose("MainViewModel Construction");

            _currentCalendar = currentCalendar;
            _currentImage = _currentCalendar.Image;

            _sampleCalendar = Calendar.CreateInstance(_currentCalendar.BackgroundImage);

            TextSize = _currentCalendar.TextSize;
            BackgroundColor = _currentCalendar.BackgroundColor;
            TextColor = _currentCalendar.TextColor;
            SampleImage = _sampleCalendar.Image;
        }

        /// <summary>
        /// Gets the save settings command.
        /// </summary>
        /// <value>The save settings command.</value>
        public DelegateCommand SaveSettingsCommand => _saveSettingsCommand ??= new DelegateCommand(() =>
        {
            _logger.LogInformation("Saving calendar properties");

            // Update the current Calendar values
            _currentCalendar.BackgroundColor = BackgroundColor;
            _currentCalendar.TextColor = TextColor;
            _currentCalendar.TextSize = TextSize;
            CurrentImage = _currentCalendar.Image;

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
        public DelegateCommand CancelSettingsCommand => _cancelSettingsCommand ??= new DelegateCommand(() =>
        {
            _logger.LogInformation("Restoring calendar properties");

            // Restore the initial settings
            BackgroundColor = _currentCalendar.BackgroundColor;
            TextColor = _currentCalendar.TextColor;
            TextSize = _currentCalendar.TextSize;

            // Close the window
            //Close?.Invoke();
        });

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
        public BitmapSource CurrentImage
        {
            get => _currentImage;
            set => SetProperty(ref _currentImage, value);
        }

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
