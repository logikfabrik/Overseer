// <copyright file="Repository.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI.Api.Models
{
    /// <summary>
    /// The <see cref="Repository" /> class.
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        /// <value>
        /// The slug.
        /// </value>
        public string Slug { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        public string last_build_id { get; set; }
        public string last_build_number { get; set; }
        public string last_build_state { get; set; }
        public string last_build_duration { get; set; }
        public string last_build_started_at { get; set; }
        public string last_build_finished_at { get; set; }
        public bool active { get; set; }
    }
}
