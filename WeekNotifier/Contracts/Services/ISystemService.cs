namespace WeekNotifier.Contracts.Services
{
    /// <summary>
    /// Interface ISystemService
    /// </summary>
    public interface ISystemService
    {
        /// <summary>
        /// Opens the in WebBrowser.
        /// </summary>
        /// <param name="url">The URL.</param>
        void OpenInWebBrowser(string url);
    }
}
