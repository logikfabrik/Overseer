// <copyright file="Commit.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI.Api.Models
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// The <see cref="Commit" /> class.
    /// </summary>
    public class Commit
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the SHA checksum.
        /// </summary>
        /// <value>
        /// The SHA checksum.
        /// </value>
        public string Sha { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        [JsonProperty(PropertyName = "ref")]
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the GitHub compare URL.
        /// </summary>
        /// <value>
        /// The GitHub compare URL.
        /// </value>
        [JsonProperty(PropertyName = "compare_url")]
        public string CompareUrl { get; set; }

        /// <summary>
        /// Gets or sets the committed at time.
        /// </summary>
        /// <value>
        /// The committed at time.
        /// </value>
        [JsonProperty(PropertyName = "committed_at")]
        public DateTime CommittedAt { get; set; }

        /// <summary>
        /// Gets or sets the committer.
        /// </summary>
        /// <value>
        /// The committer.
        /// </value>
        public User Committer { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        public User Author { get; set; }
    }
}
