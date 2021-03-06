using System.Diagnostics;
using WeekNotifier.Contracts.Services;

namespace WeekNotifier.Services
{
    /// <summary>
    /// Class SystemService.
    /// Implements the <see cref="WeekNotifier.Contracts.Services.ISystemService" />
    /// </summary>
    /// <seealso cref="WeekNotifier.Contracts.Services.ISystemService" />
    public class SystemService : ISystemService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemService"/> class.
        /// </summary>
        public SystemService()
        {
        }

        /// <summary>
        /// Opens the in WebBrowser.
        /// </summary>
        /// <param name="url">The URL.</param>
        public void OpenInWebBrowser(string url)
        {
            // For more info see https://github.com/dotnet/corefx/issues/10361
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
