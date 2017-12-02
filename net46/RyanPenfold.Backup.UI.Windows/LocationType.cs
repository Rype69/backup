// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocationType.cs" company="Ryan Penfold">
//     Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Backup.UI.Windows
{
    /// <summary>
    /// Denotes the type of path
    /// </summary>
    public enum LocationType
    {
        /// <summary>
        /// An invalid type of path
        /// </summary>
        Invalid = 0,

        /// <summary>
        /// Denotes a path pertains to a file system
        /// </summary>
        FileSystem = 1,

        /// <summary>
        /// Denotes a path pertains to Google drive
        /// </summary>
        GoogleDrive = 2,
    }
}
