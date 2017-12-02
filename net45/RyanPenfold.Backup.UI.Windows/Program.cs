// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Ryan Penfold">
//     Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Backup.UI.Windows
{
    using System;
    using System.Threading;
    using System.Windows.Forms;

    /// <summary>
    /// Contains the main entry point of the program
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// A mutex
        /// </summary>
        private static readonly Mutex Mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            if (Mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Add the event handler for handling UI thread exceptions to the event.
                Application.ThreadException += UIThreadException;

                // Set the unhandled exception mode to force all Windows Forms errors
                // to go through our handler.
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                // Add the event handler for handling non-UI thread exceptions to the event. 
                AppDomain.CurrentDomain.UnhandledException += UnhandledException;

                Application.Run(MainForm.Instance);
                Mutex.ReleaseMutex();
            }
            else
            {
                // send our Win32 message to make the currently running instance
                // jump on top of all the other windows
                NativeMethods.PostMessage(
                    (IntPtr)NativeMethods.HWND_BROADCAST,
                    NativeMethods.WM_SHOWME,
                    IntPtr.Zero,
                    IntPtr.Zero);
            }
        }

        /// <summary>
        /// Occurs when an untrapped thread exception is thrown.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">A <see cref="EventArgs" /> containing event data</param>
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once StyleCop.SA1650
        private static void UIThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Logger.Instance.Log(e.Exception);
        }

        /// <summary>
        /// Occurs when an exception is not caught.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">A <see cref="EventArgs" /> containing event data</param>
        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Instance.Log(e.ExceptionObject as Exception);
        }
    }
}
