using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using WeekNotifier.Views;

namespace WeekNotifier.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel
            => SimpleIoc.Default.GetInstance<MainViewModel>();

        public ViewModelLocator()
        {
            // Pages
            Register<MainViewModel, MainView>();
        }

        private void Register<VM, V>()
            where VM : ViewModelBase
            where V : Window
        {
            SimpleIoc.Default.Register<VM>();
            SimpleIoc.Default.Register<V>();
        }

    }
}
