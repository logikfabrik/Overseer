// <copyright file="Revisions.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Api.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="Revisions" /> class.
    /// </summary>
    public class Revisions
    {
        /// <summary>
        /// Gets or sets the revisions.
        /// </summary>
        /// <value>
        /// The revisions.
        /// </value>
        public IEnumerable<Revision> Revision { get; set; }
    }
}
