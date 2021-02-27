using System;
using System.Windows;
using System.Windows.Data;
using SDColor = System.Drawing.Color;
using SWMColor = System.Windows.Media.Color;


namespace WeekNotifier.GDI.Helpers
{
    /// <summary>
    /// Class ColorConverter.
    /// Implements the <see cref="BaseConverter" />
    /// Implements the <see cref="System.Windows.Data.IValueConverter" />
    /// </summary>
    /// <seealso cref="BaseConverter" />
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    [ValueConversion(typeof(SDColor), typeof(SWMColor))]
    public class ColorConverter : BaseConverter, IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return ((SDColor?) value)?.ToSwmColor() ?? DependencyProperty.UnsetValue;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter,
                System.Globalization.CultureInfo culture)
        {
            return ((SWMColor?) value)?.ToSdColor() ?? DependencyProperty.UnsetValue;
        }
    }
}
