// <copyright file="Project.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.CircleCI.Api.Models
{
    using System;

    /// <summary>
    /// The <see cref="Project" /> class.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Gets or sets the VCS URL.
        /// </summary>
        /// <value>
        /// The VCS URL.
        /// </value>
        // ReSharper disable once InconsistentNaming
        public Uri Vcs_url { get; set; }

        /// <summary>
        /// Gets or sets the VCS type.
        /// </summary>
        /// <value>
        /// The VCS type.
        /// </value>
        // ReSharper disable once InconsistentNaming
        public string Vcs_type { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the repository name.
        /// </summary>
        /// <value>
        /// The repository name.
        /// </value>
        public string Reponame { get; set; }
    }
}