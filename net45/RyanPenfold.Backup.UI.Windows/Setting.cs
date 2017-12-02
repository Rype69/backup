// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Setting.cs" company="Ryan Penfold">
//     Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Backup.UI.Windows
{
    using System;

    /// <summary>
    /// An individual setting
    /// </summary>
    public class Setting : ISetting
    {
        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <inheritdoc />
        public bool Enabled { get; set; }

        /// <inheritdoc />
        public string Source { get; set; }

        /// <inheritdoc />
        public string Destination { get; set; }

        /// <inheritdoc />
        public BackupMode BackupMode { get; set; }
    }
}
