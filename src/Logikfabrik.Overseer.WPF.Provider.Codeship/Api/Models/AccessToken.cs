// <copyright file="AccessToken.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.Codeship.Api.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The <see cref="AccessToken" /> class.
    /// </summary>
    public class AccessToken
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        [JsonProperty(PropertyName = "access_token")]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the organizations.
        /// </summary>
        /// <value>
        /// The organizations.
        /// </value>
        [JsonProperty(PropertyName = "organizations")]
        public IEnumerable<Organization> Organizations { get; set; }
    }
}
