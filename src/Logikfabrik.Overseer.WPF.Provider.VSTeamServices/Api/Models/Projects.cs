// <copyright file="Projects.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.Api.Models
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
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public IEnumerable<Project> Value { get; set; }
    }
}
