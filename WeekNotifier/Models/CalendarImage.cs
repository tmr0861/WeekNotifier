using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Richter.Common.Utilities.Extensions;

namespace WeekNotifier.Models
{
    public class CalendarImage : BitmapSource
    {
        /// <summary>
        /// Creates the instance of Calendar Image.
        /// </summary>
        /// <param name="calendarBackground">The calendar background.</param>
        /// <param name="weekNumber">The week number.</param>
        /// <returns>CalendarImage.</returns>
        public static CalendarImage CreateInstance(ImageSource calendarBackground, int weekNumber)
        {
            return new CalendarImage(calendarBackground, weekNumber);
        }

        /// <summary>
        /// Creates the instance of Calendar Image with the current ISO8601 week number.
        /// </summary>
        /// <param name="calendarBackground">The calendar background.</param>
        /// <returns>CalendarImage.</returns>
        public static CalendarImage CreateInstance(ImageSource calendarBackground)
        {
            return new CalendarImage(calendarBackground);
        }

        private readonly ImageSource _calendarBackground;
        private int _weekNumber;
        private Brush _textColor = Brushes.Black;
        private Brush _backgroundColor = Brushes.Beige;
        private int _textSize = 36;

        private CalendarImage(ImageSource calendarBackground) : 
            this(calendarBackground, DateTime.Today.GetIso8601WeekOfYear())
        {
        }

        private CalendarImage(ImageSource calendarBackground, int weekNumber)
        {
            _calendarBackground = calendarBackground;
            WeekNumber = weekNumber;
            Icon = DrawIcon();
        }

        public int WeekNumber
        {
            get => _weekNumber;
            set
            {
                if (_weekNumber == value) return;

                _weekNumber = value;
                Icon = DrawIcon();
            }
        }

        public ImageSource Icon { get; set; }

        public Brush BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                if (value.Equals(_backgroundColor)) return;
                _backgroundColor = value;
                Icon = DrawIcon();
            }
        }

        public Brush TextColor
        {
            get => _textColor;
            set
            {
                if (value.Equals(_textColor)) return;
                _textColor = value;
                Icon = DrawIcon();
            }
        }

        public int TextSize
        {
            get => _textSize;
            set
            {
                if (value == _textSize) return;

                _textSize = value;
                Icon = DrawIcon();
            }
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
                var rect = new Rect(0, 0, 50, 50);

                drawingContext.DrawImage(background, rect);
                drawingContext.DrawRectangle(BackgroundColor, null, rect);

                var ft = new FormattedText(
                    weekNumber.ToString("00"),
                    CultureInfo.InvariantCulture,
                    FlowDirection.LeftToRight,
                    new Typeface("Segoe UI"),
                    TextSize,
                    TextColor,
                    VisualTreeHelper.GetDpi(visual).PixelsPerDip);

                ft.SetFontWeight(FontWeights.Bold);

                drawingContext.DrawText(ft, new Point(4, 3));
            }

            return new DrawingImage(visual.Drawing);
        }

        #region BitmapSource Implementation

        public override double Height => Icon.Height;

        public override double Width => Icon.Width;

        public override ImageMetadata Metadata => Icon.Metadata;

        protected override Freezable CreateInstanceCore()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
