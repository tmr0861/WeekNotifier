// ***********************************************************************
// Assembly         : WeekNotifier
// Author           : Tom Richter
// Created          : 02-21-2021
//
// Last Modified By : Tom Richter
// Last Modified On : 02-21-2021
// ***********************************************************************
// <copyright file="MainViewModel.cs" company="Tom Richter">
//     Copyright (c) 2005-2021
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WeekNotifier.Models;
using Calendar = WeekNotifier.Models.Calendar;

namespace WeekNotifier.ViewModels
{
    /// <summary>
    /// Class MainViewModel.
    /// Implements the <see cref="GalaSoft.MvvmLight.ViewModelBase" />
    /// </summary>
    /// <seealso cref="GalaSoft.MvvmLight.ViewModelBase" />
    public class MainViewModel : ViewModelBase
    {
        private readonly Models.Calendar _currentCalendar;
        private readonly Models.Calendar _sampleCalendar;
        
        private ICommand _saveSettingsCommand;
        private ICommand _cancelSettingsCommand;
        private ImageSource _currentImage;
        private ImageSource _sampleImage;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            var bitmap = new BitmapImage(
                new Uri("pack://application:,,,/Resources/calendar.png", UriKind.Absolute));

            _currentCalendar = Calendar.CreateInstance(bitmap);
            _currentImage = _currentCalendar.Image;
            
            _sampleCalendar = Calendar.CreateInstance(bitmap, WeekNumber);

            WeekNumber = 53;
            TextSize = 40;
            BackgroundColor = Colors.Yellow;
            TextColor = Colors.Blue;
        }

        /// <summary>
        /// Gets the save settings command.
        /// </summary>
        /// <value>The save settings command.</value>
        public ICommand SaveSettingsCommand => _saveSettingsCommand ??= new RelayCommand(() =>
        {
            // Save the settings
            _currentCalendar.BackgroundColor = BackgroundColor;
            _currentCalendar.TextColor = TextColor;
            _currentCalendar.TextSize = TextSize;
            CurrentImage = _currentCalendar.Image;

            // TODO: Close the window
        });

        /// <summary>
        /// Gets the cancel settings command.
        /// </summary>
        /// <value>The cancel settings command.</value>
        public ICommand CancelSettingsCommand => _cancelSettingsCommand ??= new RelayCommand(() =>
        {
            // Restore the initial settings
            BackgroundColor = _currentCalendar.BackgroundColor;
            TextColor = _currentCalendar.TextColor;
            TextSize = _currentCalendar.TextSize;
            
            // TODO: Close the window
        });

        /// <summary>
        /// Gets or sets the sample week number.
        /// </summary>
        /// <value>The sample week number.</value>
        public int WeekNumber
        {
            get => _sampleCalendar?.WeekNumber ?? 0;
            set
            {
                if (_sampleCalendar.WeekNumber == value) return;
                
                _sampleCalendar.WeekNumber = value;
                SampleImage = _sampleCalendar.Image;
            }
        }

        /// <summary>
        /// Gets or sets the size of the text.
        /// </summary>
        /// <value>The size of the text.</value>
        public int TextSize
        {
            get => _sampleCalendar?.TextSize ?? 36;
            set
            {
                if (value == _sampleCalendar.TextSize) return;

                _sampleCalendar.TextSize = value;
                RaisePropertyChanged();
                
                SampleImage = _sampleCalendar.Image;
            }
        }
        
        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
        public Color BackgroundColor
        {
            get => _sampleCalendar.BackgroundColor;
            set
            {
                if (value.Equals(_sampleCalendar.BackgroundColor)) return;
                   
                _sampleCalendar.BackgroundColor = value;
                RaisePropertyChanged();

                SampleImage = _sampleCalendar.Image;
            }
        }

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        public Color TextColor
        {
            get => _sampleCalendar.TextColor;
            set
            {
                if (value.Equals(_sampleCalendar.TextColor)) return;
                   
                _sampleCalendar.TextColor = value;
                RaisePropertyChanged();

                SampleImage = _sampleCalendar.Image;
            }
        }

        /// <summary>
        /// Gets or sets the sample image.
        /// </summary>
        /// <value>The sample image.</value>
        public ImageSource SampleImage
        {
            get => _sampleImage;
            set => Set(ref _sampleImage, value);
        }

        /// <summary>
        /// Gets or sets the current image.
        /// </summary>
        /// <value>The current image.</value>
        public ImageSource CurrentImage
        {
            get => _currentImage;
            set => Set(ref _currentImage, value);
        }

    }
}
