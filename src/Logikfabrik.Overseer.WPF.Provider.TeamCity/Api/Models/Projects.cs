// <copyright file="Projects.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Api.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="Projects" /> class.
    /// </summary>
    public class Projects
    {
        /// <summary>
        /// Gets or sets the projects count.
        /// </summary>
        /// <value>
        /// The projects count.
        /// </value>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the href.
        /// </summary>
        /// <value>
        /// The href.
        /// </value>
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        public IEnumerable<Project> Project { get; set; }
    }
}