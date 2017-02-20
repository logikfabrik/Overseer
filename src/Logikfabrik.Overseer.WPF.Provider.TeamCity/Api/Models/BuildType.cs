// <copyright file="BuildType.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Api.Models
{
    /// <summary>
    /// The <see cref="BuildType" /> class.
    /// </summary>
    public class BuildType
    {
        /// <summary>
        /// Gets or sets the project name.
        /// </summary>
        /// <value>
        /// The project name.
        /// </value>
        public string ProjectName { get; set; }

        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        /// <value>
        /// The project identifier.
        /// </value>
        public string ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the builds.
        /// </summary>
        /// <value>
        /// The builds.
        /// </value>
        public Builds Builds { get; set; }
    }
}
