using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace WeekNotifier
{
    public partial class WeekNumberIcon : Component
    {
        #region -- D A T A --
        private const double REFRESH_TIMER_INTERVAL = 60 * 60 * 1000;   // Milliseconds

        private Color _backgroundColor = Color.Bisque;
        private Color _fontColor = Color.Black;
        private Font _fontType = new Font( FontFamily.GenericMonospace, 18, FontStyle.Bold, GraphicsUnit.Point );
        private int _offsetX = -3;
        private int _offsetY = 5;

        Rectangle _backgroundArea = new Rectangle( 1, 8, 29, 22 );

        System.Timers.Timer _refreshTimer;
        System.Timers.Timer _cursorTimer;

        // Cursor movement data
        MOUSEINPUT _mouseInput = new MOUSEINPUT();
        LPINPUT[] _lpInput = new LPINPUT[1];
        Boolean _cursorMovementActive;

        #endregion

        #region -- DLL Imports --

        struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        struct LPINPUT
        {
            public int type;
            public MOUSEINPUT mi;
        }

        [DllImport( "user32.dll", SetLastError = true )]
        private static extern uint SendInput( uint nInputs, LPINPUT[] pInputs, int cbSize );

        const int INPUT_MOUSE = 0;
        const int INPUT_KEYBOARD = 1;
        const int INPUT_HARDWARE = 2;

        const uint MOUSEEVENTF_MOVE = 0x0001;
        const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        const uint MOUSEEVENTF_LEFTUP = 0x0004;
        const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        const uint MOUSEEVENTF_XDOWN = 0x0080;
        const uint MOUSEEVENTF_XUP = 0x0100;
        const uint MOUSEEVENTF_WHEEL = 0x0800;
        const uint MOUSEEVENTF_VIRTUALDESK = 0x4000;
        const uint MOUSEEVENTF_ABSOLUTE = 0x8000;

        #endregion

        #region -- E V E N T S --

        /// <summary>
        /// Occurs when [exit selected].
        /// </summary>
        public event EventHandler ExitSelected;

        /// <summary>
        /// Occurs when [about selected].
        /// </summary>
        public event EventHandler AboutSelected;

        /// <summary>
        /// Occurs when [settings selected].
        /// </summary>
        public event EventHandler SettingsSelected;
        #endregion

        #region -- C O N S T R U C T O R S --

        /// <summary>
        /// Initializes a new instance of the <see cref="WeekNumberIcon"/> class.
        /// </summary>
        public WeekNumberIcon()
        {
            InitializeMe();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeekNumberIcon"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public WeekNumberIcon( IContainer container )
        {
            container.Add( this );
            InitializeMe();
        }
        #endregion

        #region -- P R O P E R T I E S --

        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
        public Color BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }
            set
            {
                if ( value != _backgroundColor )
                {
                    _backgroundColor = value;
                    ShowIcon();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the font.
        /// </summary>
        /// <value>The color of the font.</value>
        public Color FontColor
        {
            get
            {
                return _fontColor;
            }
            set
            {
                if ( value != _fontColor )
                {
                    _fontColor = value;
                    ShowIcon();
                }
            }
        }

        /// <summary>
        /// Gets or sets the type of the font.
        /// </summary>
        /// <value>The type of the font.</value>
        public Font FontType
        {
            get
            {
                return _fontType;
            }
            set
            {
                if ( value != _fontType )
                {
                    _fontType = value;
                    ShowIcon();
                }
            }
        }

        /// <summary>
        /// Gets or sets the offset X.
        /// </summary>
        /// <value>The offset X.</value>
        public int OffsetX
        {
            get
            {
                return _offsetX;
            }
            set
            {
                if ( value != _offsetX )
                {
                    _offsetX = value;
                    ShowIcon();
                }
            }
        }

        /// <summary>
        /// Gets or sets the offset Y.
        /// </summary>
        /// <value>The offset Y.</value>
        public int OffsetY
        {
            get
            {
                return _offsetY;
            }
            set
            {
                if ( value != _offsetY )
                {
                    _offsetY = value;
                    ShowIcon();
                }
            }
        }

        /// <summary>
        /// Gets the week number.
        /// </summary>
        /// <value>The week number.</value>
        public int CurrentWeek
        {
            get { return GetIso8601WeekOfYear( System.DateTime.Now ); }
        }

        #endregion

        #region -- E V E N T   H A N D L E R S --

        /// <summary>
        /// Handles the ItemClicked event of the _contextMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.ToolStripItemClickedEventArgs"/> instance containing the event data.</param>
        private void _contextMenu_ItemClicked( object sender, ToolStripItemClickedEventArgs e )
        {
            if ( e.ClickedItem.Tag.ToString().Equals( "Exit" ) )
            {
                OnExitSelected( new EventArgs() );
            }
            else if ( e.ClickedItem.Tag.ToString().Equals( "About" ) )
            {
                OnAboutSelected( new EventArgs() );
            }
            else if ( e.ClickedItem.Tag.ToString().Equals( "Settings" ) )
            {
                OnSettingsSelected( new EventArgs() );
            }
            else if ( e.ClickedItem.Tag.ToString().Equals( "Refresh" ) )
            {
                ShowIcon();
            }
        }

        /// <summary>
        /// Handles the MouseClick event of the _weekNumberNotifyIcon control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void _weekNumberNotifyIcon_MouseClick( object sender, MouseEventArgs e )
        {
            Trace.WriteLine( string.Format( "SingleClick {0} at {1}", e.Button, DateTime.Now.ToLongTimeString() ) );

            if ( e.Button.Equals( MouseButtons.Left ) )
            {
                // Change automated cursor movement state
                _cursorMovementActive = !_cursorMovementActive;
                ShowIcon();
            }
        }

        /// <summary>
        /// Handles the Elapsed event of the _refreshTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        private void _refreshTimer_Elapsed( object sender, System.Timers.ElapsedEventArgs e )
        {
            Debug.WriteLine( string.Format( "Timer Fired at {0}", e.SignalTime ) );
            ShowIcon();
        }

        /// <summary>
        /// Handles the Elapsed event of the _cursorTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        private void _cursorTimer_Elapsed( object sender, System.Timers.ElapsedEventArgs e )
        {
            Debug.WriteLine( string.Format( "Cursor movement active {0} at {1}", _cursorMovementActive, e.SignalTime ) );

            if ( _cursorMovementActive.Equals( true ) )
            {
                // Send the mouse move
                SendInput( (uint)_lpInput.Length, _lpInput, Marshal.SizeOf( _lpInput[0].GetType() ) );
            }
        }

        #endregion

        #region -- M E T H O D S --

        /// <summary>
        /// Initializes me.
        /// </summary>
        private void InitializeMe()
        {
            InitializeComponent();

            _weekNumberNotifyIcon.ContextMenuStrip = _contextMenu;

            _refreshTimer = new System.Timers.Timer( REFRESH_TIMER_INTERVAL );
            _refreshTimer.Elapsed += new System.Timers.ElapsedEventHandler( _refreshTimer_Elapsed );
            _refreshTimer.AutoReset = true;
            _refreshTimer.Start();

            _cursorMovementActive = false;

            ShowIcon();


            //
            // Setup mouse movement and timer
            //
            // Set a relative mouse move with X and Y both 0
            _mouseInput.dx = 0;
            _mouseInput.dy = 0;
            _mouseInput.mouseData = 0;
            _mouseInput.time = 0;
            _mouseInput.dwFlags = MOUSEEVENTF_MOVE;
            _mouseInput.dwExtraInfo = IntPtr.Zero;

            // Prepare the Send Input structure
            _lpInput[0].type = INPUT_MOUSE;
            _lpInput[0].mi = _mouseInput;

            // Determine custor timer interval
            double CursorTimerInterval;

            string ScreenSaveTimeout = (string)Registry.GetValue( "HKEY_CURRENT_USER\\Software\\Policies\\Microsoft\\Windows\\Control Panel\\Desktop", "ScreenSaveTimeOut", "-----" );

            if ( double.TryParse( ScreenSaveTimeout, out CursorTimerInterval ) == false )
            {
                Debug.WriteLine( "Unable to read ScreenSaveTimeOut from Registry" );
                CursorTimerInterval = 75;                   // Default ScreenSaverTimeout (seconds)
            }

            CursorTimerInterval *= ( ( 8 * 1000 ) / 10 );         // 80% of ScreenSaverTimeout (milliseconds)

            // Set Cursor Timer
            _cursorTimer = new System.Timers.Timer( CursorTimerInterval );
            _cursorTimer.Elapsed += new System.Timers.ElapsedEventHandler( _cursorTimer_Elapsed );
            _cursorTimer.AutoReset = true;
            _cursorTimer.Start();
        }

        /// <summary>
        /// Shows the icon.
        /// </summary>
        public void ShowIcon()
        {
            if ( DesignMode ) return;

            var week = CurrentWeek;
            _weekNumberNotifyIcon.Icon = GetIcon( week );
            _weekNumberNotifyIcon.Text = string.Format( "{0:D} is w{0:yy}{1:00}", DateTime.Now, week );
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <param name="weekNumber">The week number.</param>
        /// <returns></returns>
        public Icon GetIcon( int weekNumber )
        {
            Bitmap bmp;

            try
            {
                if ( _cursorMovementActive.Equals( true ) )
                {
                    bmp = Properties.Resources.RedCalendar.ToBitmap();
                }
                else
                {
                    bmp = Properties.Resources.BlueCalendar.ToBitmap();
                }

                Graphics g = Graphics.FromImage( bmp );

                g.FillRectangle( new SolidBrush( BackgroundColor ), _backgroundArea );
                g.DrawString( weekNumber.ToString( "00" ), FontType, new SolidBrush( FontColor ), OffsetX, OffsetY );

                return Icon.FromHandle( bmp.GetHicon() );
            }
            catch ( Exception e )
            {
                Debug.WriteLine( e.Message );
                return null;
            }
        }

        /// <summary>
        /// Gets the iso8601 week of year.
        /// This presumes that weeks start with Monday and week 1 is the 1st week of the year with a Thursday in it.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static int GetIso8601WeekOfYear( DateTime date )
        {
            // Seriously cheat.  
            // If its Monday, Tuesday or Wednesday, then it'll be the same week number as whatever Thursday, Friday or Saturday are, and we always get those right
            var day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek( date );
            if ( day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday )
            {
                date = date.AddDays( 3 );
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear( date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday );
        }

        /// <summary>
        /// Raises the <see cref="E:ExitSelected"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnExitSelected( EventArgs e )
        {
            EventHandler handler = ExitSelected;
            if ( null != handler )
            {
                handler( this, e );
            }
        }

        /// <summary>
        /// Raises the <see cref="E:SettingsSelected"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnSettingsSelected( EventArgs e )
        {
            EventHandler handler = SettingsSelected;
            if ( null != handler )
            {
                handler( this, e );
            }
        }

        /// <summary>
        /// Raises the <see cref="E:AboutSelected"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnAboutSelected( EventArgs e )
        {
            EventHandler handler = AboutSelected;
            if ( null != handler )
            {
                handler( this, e );
            }
        }

        #endregion

    }
}
