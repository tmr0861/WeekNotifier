// ***********************************************************************
// Assembly         : WeekNotifier
// Author           : Tom Richter
// Created          : 02-21-2021
//
// Last Modified By : Tom Richter
// Last Modified On : 02-21-2021
// ***********************************************************************
// <copyright file="ViewModelLocator.cs" company="Tom Richter">
//     Copyright (c) 2005-2021
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using WeekNotifier.Views;

namespace WeekNotifier.ViewModels
{
    /// <summary>
    /// Class ViewModelLocator.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Gets the main view model.
        /// </summary>
        /// <value>The main view model.</value>
        public MainViewModel MainViewModel
            => SimpleIoc.Default.GetInstance<MainViewModel>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelLocator"/> class.
        /// </summary>
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
