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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Prism.Commands;
using Prism.Mvvm;
using WeekNotifier.Models;

namespace WeekNotifier.ViewModels
{
    /// <summary>
    /// Class MainViewModel.
    /// Implements the <see cref="Prism.Mvvm.BindableBase" />
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public class MainViewModel : BindableBase
    {
        private readonly Calendar _currentCalendar;
        private readonly Calendar _sampleCalendar;
        
        private DelegateCommand _saveSettingsCommand;
        private DelegateCommand _cancelSettingsCommand;
        private ImageSource _currentImage;
        private ImageSource _sampleImage;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel(Calendar currentCalendar)
        {
            _currentCalendar = currentCalendar;
            _currentImage = _currentCalendar.Image;
            
            _sampleCalendar = Calendar.CreateInstance(_currentCalendar.BackgroundImage, WeekNumber);

            TextSize = _currentCalendar.TextSize;
            BackgroundColor = _currentCalendar.BackgroundColor;
            TextColor = _currentCalendar.TextColor;
            SampleImage = _sampleCalendar.Image;
        }

        /// <summary>
        /// Gets the save settings command.
        /// </summary>
        /// <value>The save settings command.</value>
        public DelegateCommand SaveSettingsCommand => _saveSettingsCommand ??= new DelegateCommand(() =>
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
        public DelegateCommand CancelSettingsCommand => _cancelSettingsCommand ??= new DelegateCommand(() =>
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
            get => _sampleCalendar?.WeekNumber ?? 53;
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
            get => _sampleCalendar?.TextSize ?? 42;
            set
            {
                if (value == _sampleCalendar.TextSize) return;

                _sampleCalendar.TextSize = value;
                //RaisePropertyChanged();
                
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
                //RaisePropertyChanged();

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
                //RaisePropertyChanged();

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
            set => SetProperty(ref _sampleImage, value);
        }

        /// <summary>
        /// Gets or sets the current image.
        /// </summary>
        /// <value>The current image.</value>
        public ImageSource CurrentImage
        {
            get => _currentImage;
            set => SetProperty(ref _currentImage, value);
        }

    }
}
