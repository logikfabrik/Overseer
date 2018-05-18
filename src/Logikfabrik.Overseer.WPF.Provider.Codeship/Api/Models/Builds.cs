// <copyright file="Builds.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.Codeship.Api.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The <see cref="Builds" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class Builds : IPageable
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [JsonProperty(PropertyName = "builds")]
        public IEnumerable<Build> Items { get; set; }

        /// <inheritdoc />
        public int Total { get; set; }

        /// <inheritdoc />
        [JsonProperty(PropertyName = "per_page")]
        public int PerPage { get; set; }

        /// <inheritdoc />
        public int Page { get; set; }
    }
}
