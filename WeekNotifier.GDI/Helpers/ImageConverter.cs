using System;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WeekNotifier.GDI.Helpers
{
    /// <summary>
    /// Class ImageConverter.
    /// Implements the <see cref="BaseConverter" />
    /// Implements the <see cref="System.Windows.Data.IValueConverter" />
    /// </summary>
    /// <seealso cref="BaseConverter" />
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    [ValueConversion(typeof(Icon), typeof(ImageSource))]
    public class ImageConverter : BaseConverter, IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Icon) value)?.ToBitmap().ToBitmapSource() ?? DependencyProperty.UnsetValue;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // TODO: Implement ImageConverter.ConvertBack

            // For now I only need one way conversion
            return DependencyProperty.UnsetValue;
        }
    }
}
