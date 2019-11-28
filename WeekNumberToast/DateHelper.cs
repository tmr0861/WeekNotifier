using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekNumberToast
{
    public static class DateHelper
    {
        public static int GetIso8601WeekOfYear(this DateTime date)
        {
            // Seriously cheat.  
            // If its Monday, Tuesday or Wednesday, then it'll be the same week number as whatever Thursday, Friday or Saturday are, and we always get those right
            //var day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
            //if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            //{
            //    date = date.AddDays(3);
            //}

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
                date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

    }
}
