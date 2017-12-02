// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILoggingBackgroundWorker.cs" company="Ryan Penfold">
//     Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Backup.UI.Windows
{
    /// <summary>
    /// Provides an interface for a logging background worker
    /// </summary>
    public interface ILoggingBackgroundWorker
    {
        /// <summary>
        /// A "log" event
        /// </summary>
        event LoggingBackgroundWorker.LoggingEventHandler Log;
    }
}