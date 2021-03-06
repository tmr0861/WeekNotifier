using System;

namespace WeekNotifier.Contracts.Services
{
    /// <summary>
    /// Interface IApplicationInfoService
    /// </summary>
    public interface IApplicationInfoService
    {
        /// <summary>
        /// Gets the application package version.
        /// </summary>
        /// <returns>Version.</returns>
        Version GetVersion();
    }
}
