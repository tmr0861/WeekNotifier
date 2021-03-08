using System;
using System.Collections;
using System.IO;
using System.Windows;
using Richter.Common.Utilities.Contracts.Services;
using WeekNotifier.Contracts.Services;
using WeekNotifier.Models;

namespace WeekNotifier.Services
{
    /// <summary>
    /// Class PersistAndRestoreService.
    /// Implements the <see cref="WeekNotifier.Contracts.Services.IPersistAndRestoreService" />
    /// </summary>
    /// <seealso cref="WeekNotifier.Contracts.Services.IPersistAndRestoreService" />
    public class PersistAndRestoreService : IPersistAndRestoreService
    {
        private readonly IFileService _fileService;
        private readonly AppConfig _appConfig;
        private readonly string _localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistAndRestoreService"/> class.
        /// </summary>
        /// <param name="fileService">The file service.</param>
        /// <param name="appConfig">The application configuration.</param>
        public PersistAndRestoreService(IFileService fileService, AppConfig appConfig)
        {
            _fileService = fileService;
            _appConfig = appConfig;
        }

        /// <summary>
        /// Persists the data.
        /// </summary>
        public void PersistData()
        {
            var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);
            var fileName = _appConfig.AppPropertiesFileName;
            _fileService.Save(folderPath, fileName, Application.Current.Properties);
        }

        /// <summary>
        /// Restores the data.
        /// </summary>
        public void RestoreData()
        {
            var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);
            var fileName = _appConfig.AppPropertiesFileName;
            var properties = _fileService.Read<IDictionary>(folderPath, fileName);
            if (properties == null) return;
            
            foreach (DictionaryEntry property in properties)
            {
                Application.Current.Properties.Add(property.Key, property.Value);
            }
        }
    }
}
