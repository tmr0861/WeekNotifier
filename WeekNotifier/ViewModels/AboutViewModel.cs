using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Richter.Common.Utilities.Contracts.Services;
using Richter.Common.Utilities.Services;

namespace WeekNotifier.ViewModels
{
    /// <summary>
    /// Class AboutViewModel.
    /// </summary>
    public class AboutViewModel
    {
        private readonly IApplicationInfoService _applicationInfoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutViewModel"/> class.
        /// </summary>
        public AboutViewModel() : this(new ApplicationInfoService())
        {
            
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutViewModel"/> class.
        /// </summary>
        /// <param name="applicationInfoService">The application information service.</param>
        public AboutViewModel(IApplicationInfoService applicationInfoService)
        {
            _applicationInfoService = applicationInfoService;
        }

        /// <summary>
        /// Gets the application title.
        /// </summary>
        /// <value>The application title.</value>
        public string AppTitle => _applicationInfoService.GetProduct();

        /// <summary>
        /// Gets or sets the version description.
        /// </summary>
        /// <value>The version description.</value>
        public string VersionDescription => $"Version: {_applicationInfoService.GetVersion()}";

        /// <summary>
        /// Gets or sets the application description.
        /// </summary>
        /// <value>The application description.</value>
        public string AppDescription => _applicationInfoService.GetDescription();

        /// <summary>
        /// Gets or sets the copyright statement.
        /// </summary>
        /// <value>The copyright statement.</value>
        public string Copyright
        {
            get
            {
                var sb = new StringBuilder(_applicationInfoService.GetCopyright());

                sb.Append($" {_applicationInfoService.GetCompany()} All Rights Reserved.");

                return sb.ToString();
            }
        }

    }
}
