// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Ryan Penfold">
//     Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Backup.UI.Windows
{
    using System;

    /// <summary>
    /// Provides an interface for functionality that sends data to a log
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Appends some text to the log text box
        /// </summary>
        /// <param name="message">The text to append</param>
        /// <param name="isException">Indicates whether or not this message is the result of an <see cref="Exception"/>.</param>
        void Log(string message, bool isException = false);

        /// <summary>
        /// Appends some text to the log text box
        /// </summary>
        /// <param name="exception">Contains the text to append</param>
        void Log(Exception exception);
    }
}