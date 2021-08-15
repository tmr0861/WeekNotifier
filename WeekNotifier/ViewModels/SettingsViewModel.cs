using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Prism.Mvvm;
using Richter.Common.Utilities.Logging;
using WeekNotifier.Models;

namespace WeekNotifier.ViewModels
{
    /// <summary>
    /// Class SettingsViewModel.
    /// Implements the <see cref="Prism.Mvvm.BindableBase" />
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public class SettingsViewModel : BindableBase
    {
        private readonly Calendar _calendar;
        private readonly Calendar _sampleCalendar;
        private BitmapSource _sampleImage;
        private int _textSize;
        private Color _backgroundColor;
        private Color _textColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        public SettingsViewModel() : this(Calendar.CreateInstance())
        {
            
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        /// <param name="calendar">The calendar.</param>
        public SettingsViewModel(Calendar calendar)
        {
            Log.Manager.AsWeekNotifier().LogVerbose("Creating SettingsVM");
            _calendar = calendar;
            
            _sampleCalendar = Calendar.CreateInstance(_calendar.BackgroundImage, 53);
            _sampleCalendar.RestoreSettings();
            SampleImage = _sampleCalendar.Image;
            _sampleCalendar.PropertyChanged += (sender, args) => SampleImage = _sampleCalendar.Image;
            
            TextSize = _calendar.TextSize;
            BackgroundColor = _calendar.BackgroundColor;
            TextColor = _calendar.TextColor;
        }

        /// <summary>
        /// Gets or sets the sample week number.
        /// </summary>
        /// <value>The sample week number.</value>
        public int WeekNumber
        {
            get => _sampleCalendar?.WeekNumber ?? 53;
            set => _sampleCalendar.WeekNumber = value;
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
        /// Gets or sets the size of the text.
        /// </summary>
        /// <value>The size of the text.</value>
        public int TextSize
        {
            get => _textSize;
            set
            {
                SetProperty(ref _textSize, value);
                _calendar.TextSize = value;
                _sampleCalendar.TextSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                SetProperty(ref _backgroundColor, value);
                _calendar.BackgroundColor = value;
                _sampleCalendar.BackgroundColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        public Color TextColor
        {
            get => _textColor;
            set
            {
                SetProperty(ref _textColor, value);
                _calendar.TextColor = value;
                _sampleCalendar.TextColor = value;
            }
        }
    }
}
