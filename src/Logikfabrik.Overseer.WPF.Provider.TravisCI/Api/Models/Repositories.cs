// <copyright file="Repositories.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI.Api.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The <see cref="Repositories" /> class.
    /// </summary>
    public class Repositories
    {
        /// <summary>
        /// Gets or sets the records.
        /// </summary>
        /// <value>
        /// The records.
        /// </value>
        [JsonProperty(PropertyName = "repositories")]
        public IEnumerable<Repository> Records { get; set; }
    }
}
