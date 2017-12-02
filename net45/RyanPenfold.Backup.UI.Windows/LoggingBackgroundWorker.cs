// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggingBackgroundWorker.cs" company="Ryan Penfold">
//     Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Backup.UI.Windows
{
    using System.ComponentModel;

    /// <summary>
    /// A <see cref="BackgroundWorker"/> that employs logging functionality
    /// </summary>
    public class LoggingBackgroundWorker : BackgroundWorker, ILoggingBackgroundWorker
    {
        /// <summary>
        /// A logging event handler delegate
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">An <see cref="LoggingEventArgs" /> object that contains the event data. </param>
        public delegate void LoggingEventHandler(object sender, LoggingEventArgs e);

        /// <summary>
        /// A log event
        /// </summary>
        public event LoggingEventHandler Log;

        /// <summary>
        /// Occurs when the <see cref="Log"/> event is raised
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">An <see cref="LoggingEventArgs" /> object that contains the event data. </param>
        public void OnLog(object sender, LoggingEventArgs e)
        {
            this.Log?.Invoke(sender, e);
        }
    }
}
