// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Ryan Penfold">
//     Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Backup.UI.Windows
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// A native methods class
    /// </summary>
    /// <remarks>
    /// this class just wraps some Win32 stuff that we're going to use
    /// </remarks>
    internal class NativeMethods
    {
        /// <summary>
        /// HWND_BROADCAST constant
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public const int HWND_BROADCAST = 0xffff;

        /// <summary>
        /// WM_SHOWME constant
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static readonly int WM_SHOWME = RegisterWindowMessage("WM_SHOWME");

        [DllImport("user32")]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

        [DllImport("user32")]
        public static extern int RegisterWindowMessage(string message);
    }
}
