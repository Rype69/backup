// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Location.cs" company="Ryan Penfold">
//     Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Backup.UI.Windows
{
    /// <summary>
    /// The specification of a unique location
    /// </summary>
    public class Location : ILocation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class. 
        /// </summary>
        /// <param name="type">
        /// The type of location
        /// </param>
        /// <param name="path">
        /// The path to a resource
        /// </param>
        public Location(LocationType type, string path = null)
        {
            this.Type = type;
            this.Path = path;
        }

        /// <summary>
        /// Gets or sets the type of the location
        /// </summary>
        public LocationType Type { get; set; }

        /// <summary>
        /// Gets or sets the path of the location
        /// </summary>
        public string Path { get; set; }
    }
}
