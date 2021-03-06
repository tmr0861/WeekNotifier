namespace WeekNotifier.Contracts.Services
{
    /// <summary>
    /// Interface IPersistAndRestoreService
    /// </summary>
    public interface IPersistAndRestoreService
    {
        /// <summary>
        /// Restores the data.
        /// </summary>
        void RestoreData();

        /// <summary>
        /// Persists the data.
        /// </summary>
        void PersistData();
    }
}
