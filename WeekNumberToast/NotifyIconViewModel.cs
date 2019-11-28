using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using FontStyle = System.Drawing.FontStyle;

namespace WeekNumberToast
{
    /// <summary>
    /// Provides bindable properties and commands for the NotifyIcon. In this sample, the
    /// view model is assigned to the NotifyIcon in XAML. Alternatively, the startup routing
    /// in App.xaml.cs could have created this view model, and assigned it to the NotifyIcon.
    /// </summary>
    public class NotifyIconViewModel
    {
        /// <summary>
        /// Shows a window, if none is already open.
        /// </summary>
        public ICommand ShowHideWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    //CanExecuteFunc = () => Application.Current.MainWindow == null,
                    CommandAction = () =>
                    {
                        if (Application.Current.MainWindow == null)
                        {
                            Application.Current.MainWindow = new MainWindow();
                        }

                        if (!Application.Current.MainWindow.IsVisible)
                        {
                            Application.Current.MainWindow.Show();
                        }
                        else
                        {
                            Application.Current.MainWindow.Close();
                        }
                    }
                };
            }
        }

        ///// <summary>
        ///// Hides the main window. This command is only enabled if a window is open.
        ///// </summary>
        //public ICommand HideWindowCommand
        //{
        //    get
        //    {
        //        return new DelegateCommand
        //        {
        //            CanExecuteFunc = () => Application.Current.MainWindow != null,
        //            CommandAction = () => Application.Current.MainWindow.Close()
        //        };
        //    }
        //}

        public ICommand RefreshCommand => new DelegateCommand
        {
            CommandAction = () => Debug.WriteLine("Refreshing!")
        };

        /// <summary>
        /// Shuts down the application.
        /// </summary>
        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand { CommandAction = () => Application.Current.Shutdown() };
            }
        }

        public string ToolTipText => "Double-click for window, right-click for menu";

        private readonly Rectangle _backgroundArea = new Rectangle(1, 8, 29, 22);

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <param name="weekNumber">The week number.</param>
        /// <returns></returns>
        public Icon GetIcon(int weekNumber)
        {
            try
            {
                var bmp = Properties.Resources.Calendar.ToBitmap();
                var g = Graphics.FromImage(bmp);

                g.FillRectangle(new SolidBrush(Color.LightBlue), _backgroundArea);
                g.DrawString(weekNumber.ToString("00"),
                    FontType, new SolidBrush(FontColor), OffsetX, OffsetY);

                return Icon.FromHandle(bmp.GetHicon());
            }
            catch (Exception)
            {
                return null;
            }
        }

        public float OffsetY { get; set; } = 6;

        public float OffsetX { get; set; } = -3;

        public Color FontColor { get; set; } = Color.Black;

        public Font FontType { get; set; } = new Font(
            FontFamily.GenericMonospace, 9, FontStyle.Bold, GraphicsUnit.Point);
    }
}
