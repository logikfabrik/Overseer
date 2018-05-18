// <copyright file="Build.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.Codeship.Api.Models
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// The <see cref="Build" /> class.
    /// </summary>
    public class Build
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty(PropertyName = "uuid")]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        /// <value>
        /// The project identifier.
        /// </value>
        [JsonProperty(PropertyName = "project_uuid")]
        public Guid ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the organization identifier.
        /// </summary>
        /// <value>
        /// The organization identifier.
        /// </value>
        [JsonProperty(PropertyName = "organization_uuid")]
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// Gets or sets the commit message.
        /// </summary>
        /// <value>
        /// The commit message.
        /// </value>
        [JsonProperty(PropertyName = "commit_message")]
        public string CommitMessage { get; set; }

        /// <summary>
        /// Gets or sets the commit SHA.
        /// </summary>
        /// <value>
        /// The commit SHA.
        /// </value>
        [JsonProperty(PropertyName = "commit_sha")]
        public string CommitSha { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        [JsonProperty(PropertyName = "ref")]
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public BuildStatus? Status { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the queued at time.
        /// </summary>
        /// <value>
        /// The queued at time.
        /// </value>
        [JsonProperty(PropertyName = "queued_at")]
        public DateTime? QueuedAt { get; set; }

        /// <summary>
        /// Gets or sets the finished at time.
        /// </summary>
        /// <value>
        /// The finished at time.
        /// </value>
        [JsonProperty(PropertyName = "finished_at")]
        public DateTime? FinishedAt { get; set; }

        /// <summary>
        /// Gets or sets the allocated at time.
        /// </summary>
        /// <value>
        /// The allocated at time.
        /// </value>
        [JsonProperty(PropertyName = "allocated_at")]
        public DateTime? AllocatedAt { get; set; }
    }
}