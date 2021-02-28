using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;

namespace WeekNotifier.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _weekNumber = string.Empty;
        private ImageSource _calendarImage;
        private ImageSource _drawnImage;

        public MainViewModel()
        {
            var bitmap = new BitmapImage(
                new Uri("pack://application:,,,/Resources/calendar.png", UriKind.Absolute));

            CalendarImage = bitmap;
            WeekNumber = "53";
        }

        public ImageSource CalendarImage
        {
            get => _calendarImage;
            set
            {
                if (_calendarImage != null && _calendarImage.Equals(value)) return;

                Set(ref _calendarImage, value);
                DrawIcon();
            }
        }

        public string WeekNumber
        {
            get => _weekNumber;
            set
            {
                if (_weekNumber == value) return;
                
                Set(ref _weekNumber, value);
                DrawnImage = DrawIcon();
            }
        }

        public ImageSource DrawnImage
        {
            get => _drawnImage;
            set => Set(ref _drawnImage, value);
        }

        public DrawingImage DrawIcon()
        {
            return DrawIcon(CalendarImage, WeekNumber);
        }
        
        public DrawingImage DrawIcon(ImageSource image, string weekNumber)
        {
            var visual = new DrawingVisual();
            
            using (var drawingContext = visual.RenderOpen())
            {
                drawingContext.DrawImage(image, new Rect(0, 0, 50, 50));

                var ft = new FormattedText(
                    weekNumber,
                    CultureInfo.InvariantCulture,
                    FlowDirection.LeftToRight,
                    new Typeface("Segoe UI"),
                    36,
                    Brushes.Red,
                    VisualTreeHelper.GetDpi(visual).PixelsPerDip);

                ft.SetFontWeight(FontWeights.Bold);
                
                drawingContext.DrawText(ft, new Point(4,3));
            }
            
            return new DrawingImage(visual.Drawing);
        }
    }
}
