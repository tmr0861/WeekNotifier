using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WeekNotifier.Helpers
{
    /// <summary>
    /// Class ImageHelper.
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// Converts a System.Drawing.Bitmap to a System.Windows.Media.Imaging.BitmapSource.
        /// </summary>
        /// <param name="bitmap">The Bitmap to convert.</param>
        /// <returns>BitmapSource.</returns>
        public static BitmapSource ToBitmapSource(this Bitmap bitmap)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }
    }
}
