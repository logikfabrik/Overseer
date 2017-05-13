// <copyright file="Project.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.CircleCI.Api.Models
{
    using System;
    using Newtonsoft.Json;

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
        [JsonProperty(PropertyName = "vcs_url")]
        public Uri VcsUrl { get; set; }

        /// <summary>
        /// Gets or sets the VCS type.
        /// </summary>
        /// <value>
        /// The VCS type.
        /// </value>
        [JsonProperty(PropertyName = "vcs_type")]
        public string VcsType { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty(PropertyName = "reponame")]
        public string Name { get; set; }
    }
}