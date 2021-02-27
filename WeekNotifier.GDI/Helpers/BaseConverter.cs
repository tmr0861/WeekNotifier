using System;
using System.Windows.Markup;

namespace WeekNotifier.GDI.Helpers
{
    /// <inheritdoc />
    public abstract class BaseConverter : MarkupExtension
    {
        /// <inheritdoc />
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
