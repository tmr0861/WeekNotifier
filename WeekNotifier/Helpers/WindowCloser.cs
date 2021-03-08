using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WeekNotifier.Contracts;

namespace WeekNotifier.Helpers
{
    /// <summary>
    /// Class WindowCloser.
    /// </summary>
    public class WindowCloser
    {
        /// <summary>
        /// Gets the enable window closing.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>bool.</returns>
        public static bool GetEnableWindowClosing(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableWindowClosingProperty);
        }

        /// <summary>
        /// Sets the enable window closing.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetEnableWindowClosing(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableWindowClosingProperty, value);
        }

        /// <summary>
        /// The enable window closing property
        /// </summary>
        public static readonly DependencyProperty EnableWindowClosingProperty =
            DependencyProperty.RegisterAttached("EnableWindowClosing", typeof(bool), typeof(WindowCloser), new PropertyMetadata(false, OnEnableWindowClosingChanged));

        /// <summary>
        /// Handles the <see cref="E:EnableWindowClosingChanged" /> event.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="_">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnEnableWindowClosingChanged(DependencyObject d, DependencyPropertyChangedEventArgs _)
        {
            if (d is not Window window) return;
            
            window.Loaded += (_, _) =>
            {
                if (window.DataContext is not ICloseWindows vm) return;
                    
                vm.Close += () =>
                {
                    window.Close();
                };

                window.Closing += (_, e) =>
                {
                    e.Cancel = !vm.CanClose();
                };
            };
        }
    }
}
