using SDColor = System.Drawing.Color;
using SWMColor = System.Windows.Media.Color;

namespace WeekNotifier.GDI.Helpers
{
    /// <summary>
    /// Class Color Extension Methods.
    /// </summary>
    public static class ColorExt
    {
        /// <summary>
        /// Converts a System.Drawing.Color to a System.Windows.Media.Color.
        /// </summary>
        /// <param name="color">The System.Drawing.Color.</param>
        /// <returns>System.Windows.Media.Color.</returns>
        public static SWMColor ToSwmColor(this SDColor color) => SWMColor.FromArgb(color.A, color.R, color.G, color.B);

        /// <summary>
        /// Converts to System.Windows.Media.Color to a System.Drawing.Color.
        /// </summary>
        /// <param name="color">The System.Windows.Media.Color.</param>
        /// <returns>System.Drawing.Color.</returns>
        public static SDColor ToSdColor(this SWMColor color) => SDColor.FromArgb(color.A, color.R, color.G, color.B);
    }

}
