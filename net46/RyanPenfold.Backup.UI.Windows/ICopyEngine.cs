// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICopyEngine.cs" company="Ryan Penfold">
//   Copyright © Ryan Penfold. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Backup.UI.Windows
{
    /// <summary>
    /// Provides an interface for types that 
    /// provide the core functionality for copying files.
    /// </summary>
    public interface ICopyEngine
    {
        /// <summary>
        /// Gets or sets a value indicating whether a cancel request has been received
        /// </summary>
        bool CancelRequestReceived { get; set; }

        /// <summary>
        /// Copies files.
        /// </summary>
        void Process();

        /// <summary>
        /// Copies files.
        /// </summary>
        /// <param name="backgroundWorker">
        /// A background worker to report progress on.
        /// </param>
        void Process(LoggingBackgroundWorker backgroundWorker);
    }
}
