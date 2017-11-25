// <copyright file="Builds.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.Codeship.Api.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The <see cref="Builds" /> class.
    /// </summary>
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

        /// <summary>
        /// Gets or sets the total items.
        /// </summary>
        /// <value>
        /// The total items.
        /// </value>
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the items per page.
        /// </summary>
        /// <value>
        /// The items per page.
        /// </value>
        [JsonProperty(PropertyName = "per_page")]
        public int PerPage { get; set; }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        public int Page { get; set; }
    }
}
