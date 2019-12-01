using System;
using System.Globalization;

namespace WeekNumberToast.Helpers
{
    /// <summary>
    /// Class DateHelper.
    /// </summary>
    public static class DateHelper
    {
        /// <summary>
        /// Gets the ISO8601 week of year.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>System.Int32.</returns>
        public static int GetIso8601WeekOfYear(this DateTime date)
        {
            // Seriously cheat.  
            // If its Monday, Tuesday or Wednesday, then it'll be the same week number as whatever
            // Thursday, Friday or Saturday are, and we always get those right
            var day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                date = date.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
                date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

    }
}
