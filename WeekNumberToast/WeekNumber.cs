using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekNumberToast
{
    public class WeekNumber
    {
        public int Week { get; private set; }

        /// <summary>
        /// Gets the iso8601 week of year.
        /// This presumes that weeks start with Monday and week 1 is the 1st week of the year with a Thursday in it.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
    }
}
