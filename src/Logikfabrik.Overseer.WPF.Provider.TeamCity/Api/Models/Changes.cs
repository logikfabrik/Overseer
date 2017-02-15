// <copyright file="Changes.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Api.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="Changes" /> class.
    /// </summary>
    public class Changes
    {
        /// <summary>
        /// Gets or sets the changes.
        /// </summary>
        /// <value>
        /// The changes.
        /// </value>
        public IEnumerable<Change> Change { get; set; }
    }
}
