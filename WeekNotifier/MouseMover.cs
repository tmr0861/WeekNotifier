using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Timers;
using EEVCNA.Common.Utilities.Logging;
using Microsoft.Win32;

namespace WeekNotifier
{
    /// <summary>
    /// Class MouseMover.
    /// </summary>
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public class MouseMover : IDisposable
    {
        // Mouse movement data
        private const double DEFAULT_SCREENSAVER_TIMEOUT_SECONDS = 75;
        private readonly Mouseinput _mouseInput = new Mouseinput();
        private readonly Lpinput[] _lpInput = new Lpinput[1];
        private readonly Timer _mouseMoveTimer;
        private bool _active;

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseMover"/> class.
        /// </summary>
        public MouseMover()
        {
            //
            // Setup mouse movement and timer
            //
            // Set a relative mouse move with X and Y both 0
            _mouseInput.Dx = 0;
            _mouseInput.Dy = 0;
            _mouseInput.MouseData = 0;
            _mouseInput.Time = 0;
            _mouseInput.DwFlags = MOUSEEVENTF_MOVE;
            _mouseInput.DwExtraInfo = IntPtr.Zero;

            // Prepare the Send Input structure
            _lpInput[0].Type = INPUT_MOUSE;
            _lpInput[0].Mi = _mouseInput;

            _mouseMoveTimer = new Timer()
            {
                AutoReset = true,
                Enabled = false
            };

            _mouseMoveTimer.Elapsed += MouseMoveTimerElapsed;

        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MouseMover"/> is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        public bool Active
        {
            get => _active;
            set
            {
                _active = value;

                if (Active)
                {
                    // Set the mouse move interval to 80% of the screensaver timeout
                    var mouseMoveSeconds = GetScreensaverTimeout() * .80;
                    _mouseMoveTimer.Interval = mouseMoveSeconds * 1000d;
                    _mouseMoveTimer.Start();

                    Log.Manager.AsMouseMover().LogInformation($"MouseMoveTimer Enabled with an interval of {mouseMoveSeconds} seconds");
                }
                else
                {
                    _mouseMoveTimer.Stop();
                    Log.Manager.AsMouseMover().LogInformation("MouseMoveTimer Stopped");
                }
            }
        }

        private void MouseMoveTimerElapsed(object sender, ElapsedEventArgs e)
        {
            // Send the mouse move
            Log.Manager.AsMouseMover().LogInformation("MouseMoveTimer elapsed!");
            SendInput((uint)_lpInput.Length, _lpInput, Marshal.SizeOf(_lpInput[0].GetType()));
        }

        private double GetScreensaverTimeout()
        {
            // Try to get the Screensaver timeout group policy from the registry
            var screenSaveTimeout = (string)Registry.GetValue(
                                        @"HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Control Panel\Desktop",
                                        "ScreenSaveTimeOut", null);

            if (double.TryParse(screenSaveTimeout, out var cursorTimerInterval))
            {
                return cursorTimerInterval;
            }

            Log.Manager.AsMouseMover().LogWarning("Unable to read ScreenSaveTimeOut from Registry. Using default timeout.");
            return DEFAULT_SCREENSAVER_TIMEOUT_SECONDS;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _mouseMoveTimer.Dispose();
        }

        #region -- DLL Imports --

        private struct Mouseinput
        {
            public int Dx;
            public int Dy;
            public uint MouseData;
            public uint DwFlags;
            public uint Time;
            public IntPtr DwExtraInfo;
        }

        private struct Lpinput
        {
            public int Type;
            public Mouseinput Mi;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, Lpinput[] pInputs, int cbSize);

        private const int INPUT_MOUSE = 0;
        private const int INPUT_KEYBOARD = 1;
        private const int INPUT_HARDWARE = 2;

        private const uint MOUSEEVENTF_MOVE = 0x0001;
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        private const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const uint MOUSEEVENTF_XDOWN = 0x0080;
        private const uint MOUSEEVENTF_XUP = 0x0100;
        private const uint MOUSEEVENTF_WHEEL = 0x0800;
        private const uint MOUSEEVENTF_VIRTUALDESK = 0x4000;
        private const uint MOUSEEVENTF_ABSOLUTE = 0x8000;

        #endregion

    }
}
