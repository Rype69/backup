// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISetting.cs" company="Ryan Penfold">
//     Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Backup.UI.Windows
{
    using System;

    /// <summary>
    /// Provides an interface for a setting
    /// </summary>
    public interface ISetting
    {
        /// <summary>
        /// Gets or sets an identifier
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this setting is enabled
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets a source location
        /// </summary>
        string Source { get; set; }

        /// <summary>
        /// Gets or sets a destination location
        /// </summary>
        string Destination { get; set; }

        /// <summary>
        /// Gets or sets the mode of backup
        /// </summary>
        BackupMode BackupMode { get; set; }
    }
}
