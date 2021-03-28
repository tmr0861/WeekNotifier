using Prism.Regions;

namespace WeekNotifier.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainView"/> class.
        /// </summary>
        /// <param name="regionManager"></param>
        public MainView(IRegionManager regionManager)
        {
            InitializeComponent();

            regionManager.RegisterViewWithRegion(RegionNames.SETTINGS_REGION, typeof(SettingsView));
            regionManager.RegisterViewWithRegion(RegionNames.ABOUT_REGION, typeof(AboutView));
        }
    }
}
