// <copyright file="Build.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
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
        [JsonProperty(PropertyName = "uuid")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "project_uuid")]
        public Guid ProjectId { get; set; }

        [JsonProperty(PropertyName = "organization_uuid")]
        public Guid OrganizationId { get; set; }

        [JsonProperty(PropertyName = "commit_message")]
        public string CommitMessage { get; set; }

        [JsonProperty(PropertyName = "commit_sha")]
        public string CommitSha { get; set; }

        [JsonProperty(PropertyName = "ref")]
        public string Reference { get; set; }

        public BuildStatus? Status { get; set; }

        public string Username { get; set; }

        [JsonProperty(PropertyName = "queued_at")]
        public DateTime? QueuedAt { get; set; }

        [JsonProperty(PropertyName = "finished_at")]
        public DateTime? FinishedAt { get; set; }

        [JsonProperty(PropertyName = "allocated_at")]
        public DateTime? AllocatedAt { get; set; }
    }
}