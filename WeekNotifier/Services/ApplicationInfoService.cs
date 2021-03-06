using System;
using System.Diagnostics;
using System.Reflection;
using WeekNotifier.Contracts.Services;

namespace WeekNotifier.Services
{
    /// <summary>
    /// Class ApplicationInfoService.
    /// Implements the <see cref="WeekNotifier.Contracts.Services.IApplicationInfoService" />
    /// </summary>
    /// <seealso cref="WeekNotifier.Contracts.Services.IApplicationInfoService" />
    public class ApplicationInfoService : IApplicationInfoService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationInfoService"/> class.
        /// </summary>
        public ApplicationInfoService()
        {
        }

        /// <summary>
        /// Gets the application package version.
        /// </summary>
        /// <returns>Version.</returns>
        public Version GetVersion()
        {
            // Set the app version in WeekNotifier > Properties > Package > PackageVersion
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var version = FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion;
            return new Version(version ?? string.Empty);
        }
    }
}
