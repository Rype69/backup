// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Logger.cs" company="Ryan Penfold">
//     Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Backup.UI.Windows
{
    using System;
    using System.IO;

    /// <summary>
    /// A logger
    /// </summary>
    public class Logger : ILogger
    {
        /// <summary>
        /// A singleton instance of this type
        /// </summary>
        private static ILogger instance;

        /// <summary>
        /// Prevents a default instance of the <see cref="Logger"/> class from being created. 
        /// </summary>
        private Logger()
        {
        }

        /// <summary>
        /// Gets the singleton instance of this type
        /// </summary>
        public static ILogger Instance => instance ?? (instance = new Logger());

        /// <summary>
        /// Gets the file path to a log file
        /// </summary>
        private string LogFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.dat");

        /// <summary>
        /// Appends some text to the log text box
        /// </summary>
        /// <param name="message">The text to append</param>
        /// <param name="isException">Indicates whether or not this message is the result of an <see cref="Exception"/>.</param>
        public void Log(string message, bool isException = false)
        {
            if (!File.Exists(this.LogFilePath))
            {
                File.WriteAllText(this.LogFilePath, "Rype Backup Log file\r\n");
            }

            File.AppendAllText(this.LogFilePath, $"\r\n{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {message}");

            MainForm.Instance.AppendToLogTextBox(message, isException);
        }

        /// <summary>
        /// Appends some text to the log text box
        /// </summary>
        /// <param name="exception">Contains the text to append</param>
        public void Log(Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            this.Log($"{exception.Message}\r\n{exception.StackTrace}", true);
        }
    }
}
