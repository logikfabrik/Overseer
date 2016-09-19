// <copyright file="ProjectHistory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.Api.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="ProjectHistory" /> class.
    /// </summary>
    public class ProjectHistory
    {
        /// <summary>
        /// Gets or sets the project.
        /// </summary>
        /// <value>
        /// The project.
        /// </value>
        public Project Project { get; set; }

        /// <summary>
        /// Gets or sets the builds.
        /// </summary>
        /// <value>
        /// The builds.
        /// </value>
        public IEnumerable<Build> Builds { get; set; }
    }
}
