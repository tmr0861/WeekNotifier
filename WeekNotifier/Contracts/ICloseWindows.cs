using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekNotifier.Contracts
{
    /// <summary>
    /// Interface ICloseWindows
    /// </summary>
    public interface ICloseWindows
    {
        /// <summary>
        /// Gets or sets the close action.
        /// </summary>
        /// <value>The close action.</value>
        Action Close { get; set; }

        /// <summary>
        /// Determines whether the windows can close.
        /// </summary>
        /// <returns><c>true</c> if the windows can close; otherwise, <c>false</c>.</returns>
        bool CanClose();
    }
}
