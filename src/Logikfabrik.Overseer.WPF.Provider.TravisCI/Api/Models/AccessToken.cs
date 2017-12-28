// <copyright file="AccessToken.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI.Api.Models
{
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
    }
}
