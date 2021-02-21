using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Xml;

namespace WeekNotifier
{
    /// <summary>
    /// Interaction logic for AboutBox.xaml
    /// </summary>
    public partial class AboutBox
    {
        /// <summary>
        /// Default constructor is protected so callers must use one with a parent.
        /// </summary>
        public AboutBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor that takes a parent for this AboutBox dialog.
        /// </summary>
        /// <param name="parent">Parent window for this dialog.</param>
        public AboutBox(Window parent)
            : this()
        {
            this.Owner = parent;
        }

        /// <summary>
        /// Handles click navigation on the hyperlink in the About dialog.
        /// </summary>
        /// <param name="sender">Object the sent the event.</param>
        /// <param name="e">Navigation events arguments.</param>
        private void hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            if (e.Uri == null || string.IsNullOrEmpty(e.Uri.OriginalString)) return;

            var uri = e.Uri.AbsoluteUri;
            Process.Start(new ProcessStartInfo(uri));
            e.Handled = true;
        }

        #region AboutData Provider
        #region Member data
        private XmlDocument _xmlDoc = null;

        private const string PROPERTY_NAME_TITLE = "Title";
        private const string PROPERTY_NAME_DESCRIPTION = "Description";
        private const string PROPERTY_NAME_PRODUCT = "Product";
        private const string PROPERTY_NAME_COPYRIGHT = "Copyright";
        private const string PROPERTY_NAME_COMPANY = "Company";
        private const string X_PATH_ROOT = "ApplicationInfo/";
        private const string X_PATH_TITLE = X_PATH_ROOT + PROPERTY_NAME_TITLE;
        private const string X_PATH_VERSION = X_PATH_ROOT + "Version";
        private const string X_PATH_DESCRIPTION = X_PATH_ROOT + PROPERTY_NAME_DESCRIPTION;
        private const string X_PATH_PRODUCT = X_PATH_ROOT + PROPERTY_NAME_PRODUCT;
        private const string X_PATH_COPYRIGHT = X_PATH_ROOT + PROPERTY_NAME_COPYRIGHT;
        private const string X_PATH_COMPANY = X_PATH_ROOT + PROPERTY_NAME_COMPANY;
        private const string X_PATH_LINK = X_PATH_ROOT + "Link";
        private const string X_PATH_LINK_URI = X_PATH_ROOT + "Link/@Uri";
        #endregion

        #region Properties
        /// <summary>
        /// Gets the title property, which is display in the About dialogs window title.
        /// </summary>
        public string ProductTitle
        {
            get
            {
                var result = CalculatePropertyValue<AssemblyTitleAttribute>(PROPERTY_NAME_TITLE, X_PATH_TITLE);
                if (string.IsNullOrEmpty(result))
                {
                    // otherwise, just get the name of the assembly itself.
                    result = Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
                }
                return $"About {result}";
            }
        }

        /// <summary>
        /// Gets the application's version information to show.
        /// </summary>
        public string Version
        {
            get
            {
                string result;
                // first, try to get the version string from the assembly.
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                result = version != null ? version.ToString() : GetLogicalResourceString(X_PATH_VERSION);
                return result;
            }
        }

        /// <summary>
        /// Gets the description about the application.
        /// </summary>
        public string Description => CalculatePropertyValue<AssemblyDescriptionAttribute>(PROPERTY_NAME_DESCRIPTION, X_PATH_DESCRIPTION);

        /// <summary>
        ///  Gets the product's full name.
        /// </summary>
        public string Product => CalculatePropertyValue<AssemblyProductAttribute>(PROPERTY_NAME_PRODUCT, X_PATH_PRODUCT);

        /// <summary>
        /// Gets the copyright information for the product.
        /// </summary>
        public string Copyright => CalculatePropertyValue<AssemblyCopyrightAttribute>(PROPERTY_NAME_COPYRIGHT, X_PATH_COPYRIGHT);

        /// <summary>
        /// Gets the product's company name.
        /// </summary>
        public string Company => CalculatePropertyValue<AssemblyCompanyAttribute>(PROPERTY_NAME_COMPANY, X_PATH_COMPANY);

        /// <summary>
        /// Gets the link text to display in the About dialog.
        /// </summary>
        public string LinkText => GetLogicalResourceString(X_PATH_LINK);

        /// <summary>
        /// Gets the link uri that is the navigation target of the link.
        /// </summary>
        public string LinkUri => GetLogicalResourceString(X_PATH_LINK_URI);

        #endregion

        #region Resource location methods
        /// <summary>
        /// Gets the specified property value either from a specific attribute, or from a resource dictionary.
        /// </summary>
        /// <typeparam name="T">Attribute type that we're trying to retrieve.</typeparam>
        /// <param name="propertyName">Property name to use on the attribute.</param>
        /// <param name="xpathQuery">XPath to the element in the XML data resource.</param>
        /// <returns>The resulting string to use for a property.
        /// Returns null if no data could be retrieved.</returns>
        private string CalculatePropertyValue<T>(string propertyName, string xpathQuery)
        {
            var result = string.Empty;
            // first, try to get the property value from an attribute.
            var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(T), false);
            if (attributes.Length > 0)
            {
                var attribute = (T)attributes[0];
                var property = attribute.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
                if (property != null)
                {
                    result = property.GetValue(attributes[0], null) as string;
                }
            }

            // if the attribute wasn't found or it did not have a value, then look in an xml resource.
            if (result == string.Empty)
            {
                // if that fails, try to get it from a resource.
                result = GetLogicalResourceString(xpathQuery);
            }
            return result;
        }

        /// <summary>
        /// Gets the XmlDataProvider's document from the resource dictionary.
        /// </summary>
        protected virtual XmlDocument ResourceXmlDocument
        {
            get
            {
                if (_xmlDoc != null) return _xmlDoc;

                // if we haven't already found the resource XmlDocument, then try to find it.
                if (this.TryFindResource("AboutProvider") is XmlDataProvider provider)
                {
                    // save away the XmlDocument, so we don't have to get it multiple times.
                    _xmlDoc = provider.Document;
                }
                return _xmlDoc;
            }
        }

        /// <summary>
        /// Gets the specified data element from the XmlDataProvider in the resource dictionary.
        /// </summary>
        /// <param name="xpathQuery">An XPath query to the XML element to retrieve.</param>
        /// <returns>The resulting string value for the specified XML element. 
        /// Returns empty string if resource element couldn't be found.</returns>
        protected virtual string GetLogicalResourceString(string xpathQuery)
        {
            var result = string.Empty;
            // get the About xml information from the resources.
            var doc = this.ResourceXmlDocument;
            if (doc == null) return result;

            // if we found the XmlDocument, then look for the specified data. 
            var node = doc.SelectSingleNode(xpathQuery);
            if (node == null) return result;

            result = node is XmlAttribute ? node.Value : node.InnerText;
            return result;
        }
        #endregion
        #endregion

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
