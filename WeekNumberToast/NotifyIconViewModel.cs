using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using EEVCNA.Common.Utilities.Logging;
using JetBrains.Annotations;
using WeekNumberToast.Helpers;
using WeekNumberToast.Properties;
using Color = System.Drawing.Color;

namespace WeekNumberToast
{
    /// <summary>
    /// Provides bindable properties and commands for the NotifyIcon. In this sample, the
    /// view model is assigned to the NotifyIcon in XAML. Alternatively, the startup routing
    /// in App.xaml.cs could have created this view model, and assigned it to the NotifyIcon.
    /// </summary>
    public class NotifyIconViewModel : INotifyPropertyChanged
    {
        private readonly double _refreshTimerInterval;
        private int _currentWeek;
        private Icon _sampleIcon;
        private Icon _currentIcon;
        private Timer _refreshTimer;

        /// <summary>
        /// Occurs when [refresh icon].
        /// </summary>
        public event Action RefreshIcon;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyIconViewModel"/> class.
        /// </summary>
        public NotifyIconViewModel()
        {
            SampleIcon = GetIcon(SampleWeek);
            _refreshTimerInterval = Settings.Default.RefreshMinutes * 60 * 1000;
        }

        /// <summary>
        /// Gets the command to toggle the Settings Window Visibility.
        /// </summary>
        /// <value>The show hide settings command.</value>
        public ICommand ToggleSettingsWindowCommand => new DelegateCommand
        {
            CommandAction = () =>
            {
                if (Application.Current.MainWindow == null)
                {
                    Application.Current.MainWindow = new SettingsWindow()
                    {
                        DataContext = this
                    };
                }

                Application.Current.MainWindow.Visibility = Application.Current.MainWindow.IsVisible 
                    ? Visibility.Hidden 
                    : Visibility.Visible;
            }
        };

        /// <summary>
        /// Gets the save settings command.
        /// </summary>
        /// <value>The save settings command.</value>
        public ICommand SaveSettingsCommand => new DelegateCommand
        {
            CommandAction = SaveSettings
        };

        /// <summary>
        /// Gets the cancel settings command.
        /// </summary>
        /// <value>The cancel settings command.</value>
        public ICommand CancelSettingsCommand => new DelegateCommand
        {
            CommandAction = CancelSettings
        };

        /// <summary>
        /// Gets the refresh command that updates the current week number.
        /// </summary>
        /// <value>The refresh command.</value>
        public ICommand RefreshCommand => new DelegateCommand
        {
            CommandAction = () => CurrentWeek = DateTime.Today.GetIso8601WeekOfYear()
        };

        /// <summary>
        /// Shuts down the application.
        /// </summary>
        /// <value>The exit application command.</value>
        public ICommand ExitApplicationCommand => new DelegateCommand
        {
            CommandAction = () => Application.Current.Shutdown()
        };

        /// <summary>
        /// Gets or sets the week number.
        /// </summary>
        /// <value>The week number.</value>
        public int CurrentWeek
        {
            get => _currentWeek;
            set
            {
                if (value == _currentWeek) return;

                _currentWeek = value;
                CurrentIcon = GetIcon();
            }
        }

        /// <summary>
        /// Gets the tool tip text.
        /// </summary>
        /// <value>The tool tip text.</value>
        public string ToolTipText => $"Today is {DateTime.Today:D} and is Week {CurrentWeek}!";

        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
        public Color BackgroundColor
        {
            get => Settings.Default.BackgroundColor;
            set
            {
                Settings.Default.BackgroundColor = value;
                SampleIcon = GetIcon(SampleWeek);
            }
        }

        /// <summary>
        /// Gets or sets the color of the font.
        /// </summary>
        /// <value>The color of the font.</value>
        public Color FontColor
        {
            get => Settings.Default.FontColor;
            set
            {
                Settings.Default.FontColor = value;
                SampleIcon = GetIcon(SampleWeek);
            }
        }

        /// <summary>
        /// Gets or sets the type of the font.
        /// </summary>
        /// <value>The type of the font.</value>
        public Font FontType
        {
            get => Settings.Default.FontType;
            set
            {
                Settings.Default.FontType = value;
                SampleIcon = GetIcon(SampleWeek);
            }
        }

        /// <summary>
        /// Gets or sets the offset x.
        /// </summary>
        /// <value>The offset x.</value>
        public int OffsetX
        {
            get => Settings.Default.OffsetX;
            set
            {
                Settings.Default.OffsetX = value;
                SampleIcon = GetIcon(SampleWeek);
            }
        }

        /// <summary>
        /// Gets or sets the offset y.
        /// </summary>
        /// <value>The offset y.</value>
        public int OffsetY
        {
            get => Settings.Default.OffsetY;
            set
            {
                Settings.Default.OffsetY = value;
                SampleIcon = GetIcon(SampleWeek);
            }
        }

        /// <summary>
        /// Gets or sets the sample week.
        /// </summary>
        /// <value>The sample week.</value>
        public int SampleWeek
        {
            get => Settings.Default.SampleWeek;
            set
            {
                Settings.Default.SampleWeek = value;
                SampleIcon = GetIcon(SampleWeek);
            }
        }

        /// <summary>
        /// Gets or sets the sample icon.
        /// </summary>
        /// <value>The sample icon.</value>
        public Icon SampleIcon
        {
            get => _sampleIcon;
            set
            {
                _sampleIcon = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the current icon.
        /// </summary>
        /// <value>The current icon.</value>
        public Icon CurrentIcon
        {
            get => _currentIcon;
            set
            {
                Log.Manager.AsWeekNumberToast().LogInformation($"CurrentIcon updated to week {CurrentWeek}");
                _currentIcon = value;
                OnPropertyChanged();
                OnRefreshIcon();
            }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            // Set the initial week number to raise the RefreshIcon event
            RefreshCommand?.Execute(null);

            // Start a timer that checks the current week every so often
            _refreshTimer = new Timer(_refreshTimerInterval);
            _refreshTimer.Elapsed += RefreshTimer_Elapsed;
            _refreshTimer.Start();
        }

        /// <summary>
        /// Gets the icon representing the week of the current day.
        /// </summary>
        /// <returns>Icon.</returns>
        public Icon GetIcon()
        {
            return GetIcon(CurrentWeek);
        }

        /// <summary>
        /// Gets the icon representing the given week number.
        /// </summary>
        /// <param name="weekNumber">The week number.</param>
        /// <returns>Icon.</returns>
        public Icon GetIcon(int weekNumber)
        {
            try
            {
                var bmp = Resources.Calendar.ToBitmap();
                var g = Graphics.FromImage(bmp);

                g.FillRectangle(new SolidBrush(BackgroundColor), new Rectangle(1, 8, 29, 22));
                g.DrawString(weekNumber.ToString("00"),
                    FontType, new SolidBrush(FontColor), OffsetX, OffsetY);

                return Icon.FromHandle(bmp.GetHicon());
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void CancelSettings()
        {
            // Put the original settings back
            Settings.Default.Reload();
            SampleIcon = GetIcon(SampleWeek);
            Application.Current.MainWindow?.Close();
        }

        private void SaveSettings()
        {
            // Save the new settings
            Settings.Default.Save();
            Application.Current.MainWindow?.Close();
            CurrentIcon = GetIcon();
        }

        private void RefreshTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Log.Manager.AsWeekNumberToast().LogInformation("Refresh Timer Elapsed!");
            RefreshCommand?.Execute(null);
        }

        private void OnRefreshIcon()
        {
            RefreshIcon?.Invoke();
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
