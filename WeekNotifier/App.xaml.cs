// ***********************************************************************
// Assembly         : WeekNotifier
// Author           : Tom Richter
// Created          : 02-21-2021
//
// Last Modified By : Tom Richter
// Last Modified On : 02-21-2021
// ***********************************************************************
// <copyright file="App.xaml.cs" company="Tom Richter">
//     Copyright (c) 2005-2021
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WeekNotifier.ViewModels;

namespace WeekNotifier
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Gets the locator.
        /// </summary>
        /// <value>The locator.</value>
        public ViewModelLocator Locator => Resources["Locator"] as ViewModelLocator;
    }
}
