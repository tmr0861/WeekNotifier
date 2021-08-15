using System.Linq;
using System.Reflection.Emit;
using Prism.Ioc;
using Prism.Regions;
using WeekNotifier.ViewModels;

namespace WeekNotifier.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainView" /> class.
        /// </summary>
        public MainView(IContainerExtension container)
        {
            InitializeComponent();

            var mainViewModel = container.Resolve<MainViewModel>();

            // Save will get done first.  This way no matter how you close the window, it restores settings.
            Closing += (_, _) => mainViewModel.RestoreSettings();
        }
    }
}
