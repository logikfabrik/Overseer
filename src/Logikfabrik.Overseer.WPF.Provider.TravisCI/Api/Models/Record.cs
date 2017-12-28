// <copyright file="Record.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI.Api.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// The <see cref="Record" /> class.
    /// </summary>
    public class Record
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the GitHub name.
        /// </summary>
        /// <value>
        /// The GitHub name.
        /// </value>
        [JsonProperty(PropertyName = "name")]
        public string GitHubName { get; set; }

        /// <summary>
        /// Gets or sets the GitHub login.
        /// </summary>
        /// <value>
        /// The GitHub login.
        /// </value>
        [JsonProperty(PropertyName = "login")]
        public string GitHubLogin { get; set; }

        /// <summary>
        /// Gets or sets type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }
    }
}
