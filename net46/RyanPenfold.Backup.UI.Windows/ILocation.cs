// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILocation.cs" company="Ryan Penfold">
//     Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Backup.UI.Windows
{
    /// <summary>
    /// Provides an interface for a location specification
    /// </summary>
    public interface ILocation
    {
        /// <summary>
        /// Gets or sets the type of the location
        /// </summary>
        LocationType Type { get; set; }

        /// <summary>
        /// Gets or sets the path of the location
        /// </summary>
        string Path { get; set; }
    }
}