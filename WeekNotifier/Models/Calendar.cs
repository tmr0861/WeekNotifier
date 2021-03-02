using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using Richter.Common.Utilities.Extensions;

namespace WeekNotifier.Models
{
    /// <summary>
    /// Class Calendar.
    /// </summary>
    public class Calendar
    {
        /// <summary>
        /// Creates the instance of Calendar.
        /// </summary>
        /// <param name="calendarBackground">The calendar background.</param>
        /// <param name="weekNumber">The week number.</param>
        /// <returns>Calendar.</returns>
        public static Calendar CreateInstance(ImageSource calendarBackground, int weekNumber)
        {
            return new Calendar(calendarBackground, weekNumber);
        }

        /// <summary>
        /// Creates the instance of Calendar with the current ISO8601 week number.
        /// </summary>
        /// <param name="calendarBackground">The calendar background.</param>
        /// <returns>Calendar.</returns>
        public static Calendar CreateInstance(ImageSource calendarBackground)
        {
            return new Calendar(calendarBackground);
        }

        private readonly ImageSource _calendarBackground;
        private int _weekNumber;
        private Color _textColor = Colors.Purple;
        private Color _backgroundColor = Colors.Beige;
        private int _textSize = 36;

        private Calendar(ImageSource calendarBackground) :
            this(calendarBackground, DateTime.Today.ISO8601WeekOfYear())
        {
        }

        private Calendar(ImageSource calendarBackground, int weekNumber)
        {
            _calendarBackground = calendarBackground;
            WeekNumber = weekNumber;
            Image = DrawIcon();
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

                _textSize = value;
                Image = DrawIcon();
            }
        }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public ImageSource Image { get; set; }

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
                _backgroundColor = value;
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
                _textColor = value;
                Image = DrawIcon();
            }
        }

        /// <summary>
        /// Gets the current week number.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int GetCurrentWeekNumber()
        {
            WeekNumber = DateTime.Today.ISO8601WeekOfYear();
            return WeekNumber;
        }

        private DrawingImage DrawIcon()
        {
            return DrawIcon(_calendarBackground, WeekNumber);
        }

        private DrawingImage DrawIcon(ImageSource background, int weekNumber)
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

                const double imageWidth = 50d;
                const double imageHeight = 50d;
                const int rectX = 1;
                const int rectY = 12;

                // Center the text horizontally
                var textLocationX = (imageWidth / 2) - (text.Width / 2);

                var textLocationY = ((imageHeight / 2) - (text.Height / 2)) + 2;

                drawingContext.DrawImage(background, new Rect(0, 0, imageWidth, imageHeight));
                
                drawingContext.DrawRectangle(new SolidColorBrush(BackgroundColor), null,
                    new Rect(rectX, rectY, imageWidth - rectX * 2, imageHeight - rectY - 2));
                
                drawingContext.DrawText(text, new Point(textLocationX, textLocationY));
            }

            return new DrawingImage(visual.Drawing);
        }

    }
}
