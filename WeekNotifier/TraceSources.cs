using System.Diagnostics;
using EEVCNA.Common.Utilities.Logging;

namespace WeekNotifier
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

            /// <summary>
            /// The mouse mover trace source
            /// </summary>
            public const string MOUSE_MOVER = "MouseMover";
        }

        /// <summary>
        /// Gets the WeekNumberToast trace source.
        /// </summary>
        /// <value>
        /// The WeekNumber Trace Source.
        /// </value>
        public static TraceSource WeekNumberToast { get; } = Log.Manager.GetSource(Names.WEEK_NUMBER_TOAST);

        /// <summary>
        /// Gets the mouse mover Trace Source.
        /// </summary>
        /// <value>The mouse mover Trace Source.</value>
        public static TraceSource MouseMover { get; } = Log.Manager.GetSource(Names.MOUSE_MOVER);

        /// <summary>
        /// Gets the WeekNumberToast trace source.
        /// </summary>
        /// <param name="manager">The trace source manager.</param>
        /// <returns></returns>
        public static TraceSource AsWeekNumberToast(this TraceSourceManager manager)
        {
            return manager.GetSource(Names.WEEK_NUMBER_TOAST);
        }

        /// <summary>
        /// Gets the mouse mover trace source.
        /// </summary>
        /// <param name="manager">The trace source manager.</param>
        /// <returns>TraceSource.</returns>
        public static TraceSource AsMouseMover(this TraceSourceManager manager)
        {
            return manager.GetSource(Names.MOUSE_MOVER);
        }

    }
}
