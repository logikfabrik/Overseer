// <copyright file="Revision.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Api.Models
{
    /// <summary>
    /// The <see cref="Revision" /> class.
    /// </summary>
    public class Revision
    {
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the VCS branch name.
        /// </summary>
        /// <value>
        /// The the VCS branch name.
        /// </value>
        public string VcsBranchName { get; set; }
    }
}
