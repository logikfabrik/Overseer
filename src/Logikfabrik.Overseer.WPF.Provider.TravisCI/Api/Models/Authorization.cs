// <copyright file="Authorization.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI.Api.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// The <see cref="Authorization" /> class.
    /// </summary>
    public class Authorization
    {
        /// <summary>
        /// Gets or sets the GitHub token.
        /// </summary>
        /// <value>
        /// The GitHub token.
        /// </value>
        [JsonProperty(PropertyName = "github_token")]
        public string GitHubToken { get; set; }
    }
}
