// <copyright file="Builds.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI.Api.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The <see cref="Builds" /> class.
    /// </summary>
    public class Builds
    {
        /// <summary>
        /// Gets or sets the records.
        /// </summary>
        /// <value>
        /// The records.
        /// </value>
        [JsonProperty(PropertyName = "builds")]
        public IEnumerable<Build> Records { get; set; }
    }
}
