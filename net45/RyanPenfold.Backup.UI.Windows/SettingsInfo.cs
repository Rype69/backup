// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsInfo.cs" company="Ryan Penfold">
//     Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Backup.UI.Windows
{
    using System.Collections.Generic;

    /// <summary>
    /// Settings information
    /// </summary>
    public class SettingsInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsInfo"/> class. 
        /// </summary>
        public SettingsInfo()
        {
            this.Paths = new List<Setting>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether the application should automatically close when it completes
        /// </summary>
        public bool AutoClose { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the application should
        /// end the Google Drive process before copying and restart it afterwards
        /// </summary>
        public bool EndRestartGoogleDriveProcess { get; set; }

        /// <summary>
        /// Gets or sets a set of settings 
        /// </summary>
        public IList<Setting> Paths { get; set; }
    }
}
