using System.Diagnostics;
using EEVCNA.Common.Utilities.Logging;

namespace WeekNumberToast
{
    /// <summary>
    /// Trace Sources for Windows Service component
    /// </summary>
    public static class TraceSources
    {
        /// <summary>
        /// TraceSource Names used in this assembly
        /// </summary>
        public static class Names
        {
            /// <summary>
            /// The WeekNumberToast trace source
            /// </summary>
            public const string WEEK_NUMBER_TOAST = "WeekNumberToast";
        }

        /// <summary>
        /// Gets the WeekNumberToast trace source.
        /// </summary>
        /// <value>
        /// The managed service Trace Source.
        /// </value>
        public static TraceSource WeekNumberToast { get; } = Log.Manager.GetSource(Names.WEEK_NUMBER_TOAST);

        /// <summary>
        /// Gets the WeekNumberToast trace source.
        /// </summary>
        /// <param name="manager">The trace source manager.</param>
        /// <returns></returns>
        public static TraceSource AsWeekNumberToast(this TraceSourceManager manager)
        {
            return manager.GetSource(Names.WEEK_NUMBER_TOAST);
        }

    }
}
