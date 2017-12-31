// <copyright file="Stage.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI.Api.Models
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// The <see cref="Stage" /> class.
    /// </summary>
    public class Stage
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the started at time.
        /// </summary>
        /// <value>
        /// The started at time.
        /// </value>
        [JsonProperty(PropertyName = "started_at")]
        public DateTime? StartedAt { get; set; }

        /// <summary>
        /// Gets or sets the finished at time.
        /// </summary>
        /// <value>
        /// The finished at time.
        /// </value>
        [JsonProperty(PropertyName = "finished_at")]
        public DateTime? FinishedAt { get; set; }
    }
}