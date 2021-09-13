#nullable enable
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using JetBrains.Annotations;
using Richter.Common.Utilities.Extensions;
using Richter.Common.Utilities.Logging;
using static System.Windows.Media.ColorConverter;

namespace WeekNotifier.Models
{
    /// <summary>
    /// Class Calendar.
    /// </summary>
    public class Calendar : INotifyPropertyChanged
    {
        private const int IMAGE_WIDTH = 50;
        private const int IMAGE_HEIGHT = 50;
        private const double UPDATE_TIMER_MINUTES = 1d;
        private static readonly BitmapSource DefaultBackground;

        /// <summary>
        /// Creates a blank bitmapsource image in case no background is given.
        /// </summary>
        static Calendar()
        {
            // Define parameters used to create the BitmapSource.
            var pf = PixelFormats.Bgr32;
            var rawStride = (IMAGE_WIDTH * pf.BitsPerPixel + 7) / 8;
            var rawImage = new byte[rawStride * IMAGE_HEIGHT];

            // Create a blank BitmapSource for the background.
            DefaultBackground = BitmapSource.Create(IMAGE_WIDTH, IMAGE_HEIGHT,
                96, 96, pf, null, rawImage, rawStride);
        }

        /// <summary>
        /// Creates the instance of Calendar with default image and week.
        /// </summary>
        /// <returns>WpfPrismApp.Models.Calendar.</returns>
        public static Calendar CreateInstance()
        {
            return new();
        }

        /// <summary>
        /// Creates the instance of Calendar with the current ISO8601 week number.
        /// </summary>
        /// <param name="calendarBackground">The calendar background.</param>
        /// <returns>Calendar.</returns>
        public static Calendar CreateInstance(ImageSource calendarBackground)
        {
            return new(calendarBackground);
        }

        /// <summary>
        /// Creates the instance of Calendar.
        /// </summary>
        /// <param name="calendarBackground">The calendar background.</param>
        /// <param name="weekNumber">The week number.</param>
        /// <param name="autoUpdate">Optional enable automatic update of week number.</param>
        /// <returns>Calendar.</returns>
        public static Calendar CreateInstance(ImageSource calendarBackground, int weekNumber, bool autoUpdate = false)
        {
            return new(calendarBackground, weekNumber, autoUpdate);
        }

        private readonly TraceSource _logger = Log.Manager.AsWeekNotifier();
        private readonly DispatcherTimer _updateTimer = new();

        private int _weekNumber;
        private int _textSize = 40;
        private Color _textColor = Colors.Black;
        private Color _backgroundColor = Colors.White;
        private BitmapSource _image = null!;

        private Calendar() :
            this(DefaultBackground)
        {
        }

        private Calendar(ImageSource backgroundImage) :
            this(backgroundImage, DateTime.Today.ISO8601WeekOfYear(), false)
        {
        }

        private Calendar(ImageSource backgroundImage, int weekNumber, bool autoUpdate)
        {
            BackgroundImage = backgroundImage;
            WeekNumber = weekNumber;
            Image = DrawIcon();

            _updateTimer.Tick += UpdateTimer_Tick;
            _updateTimer.Interval = TimeSpan.FromMinutes(UPDATE_TIMER_MINUTES);
            AutoUpdate = autoUpdate;
        }

        /// <summary>
        /// Gets the background image.
        /// </summary>
        /// <value>The background image.</value>
        public ImageSource BackgroundImage { get; }

        /// <summary>
        /// Gets or sets the calendar image.
        /// </summary>
        /// <value>The calendar image.</value>
        public BitmapSource Image
        {
            get => _image;
            private set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the week number.
        /// </summary>
        /// <value>The week number.</value>
        public int WeekNumber
        {
            get => _weekNumber;
            set
            {
                if (_weekNumber == value) return;

                _logger.LogVerbose($"Week number changed to {value}");
                _weekNumber = value;
                Image = DrawIcon();
            }
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
                if (value == _textSize) return;

                _logger.LogVerbose($"Text size changed to {value}");
                _textSize = value;
                Image = DrawIcon();
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
                if (value.Equals(_textColor)) return;

                _logger.LogVerbose($"Text color changed to {value}");
                _textColor = value;
                Image = DrawIcon();
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
                if (value.Equals(_backgroundColor)) return;

                _logger.LogVerbose($"Background color changed to {value}");
                _backgroundColor = value;
                Image = DrawIcon();
            }
        }

        /// <summary>
        /// Gets or sets whether automatic update of week number is enabled.
        /// </summary>
        /// <value>Automatic update of week number enabled or disabled.</value>
        public bool AutoUpdate
        {
            get => _updateTimer.IsEnabled;
            set
            {
                if (value == _updateTimer.IsEnabled) return;

                _logger.LogInformation($"Week Number Automatic Update {(value ? "Enabled" : "Disabled")}");
                _updateTimer.IsEnabled = value;
            }
        }

        /// <summary>
        /// Refreshes icon image.
        /// </summary>
        public void Refresh()
        {
            Image = DrawIcon();
        }

        /// <summary>
        /// Restores the saved settings.
        /// </summary>
        public void RestoreSettings()
        {
            _logger.LogInformation("Restoring calendar properties");

            if (Application.Current.Properties.Contains(nameof(TextSize)))
            {
                if (int.TryParse(Application.Current.Properties[nameof(TextSize)]?.ToString(), out var ts))
                {
                    TextSize = ts;
                }
            }

            if (Application.Current.Properties.Contains(nameof(TextColor)))
            {
                try
                {
                    var color = ConvertFromString(
                        Application.Current.Properties[nameof(TextColor)]?.ToString());
                    if (color != null) TextColor = (Color)color;
                }
                catch (Exception e)
                {
                    _logger.LogWarning($"Unable to parse text color: {e.Message}");
                }
            }

            if (!Application.Current.Properties.Contains(nameof(BackgroundColor))) return;
            try
            {
                var color = ConvertFromString(
                    Application.Current.Properties[nameof(BackgroundColor)]?.ToString());
                if (color != null) BackgroundColor = (Color)color;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Unable to parse background color: {e.Message}");
            }
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void SaveSettings()
        {
            _logger.LogInformation("Saving calendar properties");

            // Save the settings
            Application.Current.Properties[nameof(TextSize)] = TextSize;
            Application.Current.Properties[nameof(TextColor)] = TextColor;
            Application.Current.Properties[nameof(BackgroundColor)] = BackgroundColor;
        }

        private BitmapImage DrawIcon()
        {
            return DrawIcon(BackgroundImage, WeekNumber);
        }

        private BitmapImage DrawIcon(ImageSource background, int weekNumber)
        {
            var visual = new DrawingVisual();

            using (var drawingContext = visual.RenderOpen())
            {
                var text = new FormattedText(
                    weekNumber.ToString(),
                    CultureInfo.InvariantCulture,
                    FlowDirection.LeftToRight,
                    new Typeface("Segoe UI"),
                    TextSize,
                    new SolidColorBrush(TextColor),
                    VisualTreeHelper.GetDpi(visual).PixelsPerDip);

                text.SetFontWeight(FontWeights.Bold);


                // Center the text horizontally and vertically
                var textLocationX = (IMAGE_WIDTH / 2d) - (text.Width / 2);
                var textLocationY = (IMAGE_HEIGHT / 2d) - (text.Height / 2) + 2;

                const int rectOffsetX = 1;
                const int rectOffsetY = 12;

                // Now draw the background, a colored rectangle and the text
                drawingContext.DrawImage(background, new Rect(0, 0, IMAGE_WIDTH, IMAGE_HEIGHT));

                drawingContext.DrawRectangle(new SolidColorBrush(BackgroundColor), null,
                    new Rect(rectOffsetX, rectOffsetY,
                        IMAGE_WIDTH - rectOffsetX * 2, IMAGE_HEIGHT - rectOffsetY - 2));

                drawingContext.DrawText(text, new Point(textLocationX, textLocationY));
            }

            // Save the visual as a BitmapImage (is there a better way?)
            var renderTargetBitmap = new RenderTargetBitmap(IMAGE_WIDTH, IMAGE_HEIGHT, 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(visual);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            using var stream = new MemoryStream();

            encoder.Save(stream);
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = stream;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();
            return bitmapImage;
        }

        private void UpdateTimer_Tick(object? sender, EventArgs e)
        {
            _logger.LogVerbose("Update Timer fired...");
            WeekNumber = DateTime.Now.ISO8601WeekOfYear();
        }

        #region INotifyPropertyChanged Implementation

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            _logger.LogVerbose($"OnPropertyChanged: {propertyName}");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
