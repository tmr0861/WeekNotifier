using System;
using System.Windows.Markup;

namespace WeekNotifier.Helpers
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
