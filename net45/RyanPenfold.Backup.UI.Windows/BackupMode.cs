// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BackupMode.cs" company="Ryan Penfold">
//     Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Backup.UI.Windows
{
    /// <summary>
    /// Denotes a mode of backup
    /// </summary>
    public enum BackupMode
    {
        /// <summary>
        /// An invalid backup mode
        /// </summary>
        Invalid = 0,

        /// <summary>
        /// Denotes that all files merely get copied
        /// from the source to the destination
        /// </summary>
        Backup = 1,

        /// <summary>
        /// Denotes to keep the contents of the destination exactly 
        /// the same as the contents of the source.
        /// </summary>
        Synchronise = 2
    }
}