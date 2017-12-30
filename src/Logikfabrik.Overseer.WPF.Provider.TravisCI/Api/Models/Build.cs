// <copyright file="Build.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI.Api.Models
{
    using System;
    using System.Collections.Generic;
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
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the previous state.
        /// </summary>
        /// <value>
        /// The previous state.
        /// </value>
        [JsonProperty(PropertyName = "previous_state")]
        public string PreviousState { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public int? Duration { get; set; }

        /// <summary>
        /// Gets or sets the event type.
        /// </summary>
        /// <value>
        /// The event type.
        /// </value>
        [JsonProperty(PropertyName = "event_type")]
        public string EventType { get; set; }

        /// <summary>
        /// Gets or sets the pull request title.
        /// </summary>
        /// <value>
        /// The pull request title.
        /// </value>
        [JsonProperty(PropertyName = "pull_request_title")]
        public string PullRequestTitle { get; set; }

        /// <summary>
        /// Gets or sets the pull request number.
        /// </summary>
        /// <value>
        /// The pull request number.
        /// </value>
        [JsonProperty(PropertyName = "pull_request_number")]
        public int? PullRequestNumber { get; set; }

        [JsonProperty(PropertyName = "started_at")]
        public DateTime? StartedAt { get; set; }

        [JsonProperty(PropertyName = "finished_at")]
        public DateTime? FinishedAt { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        /// <value>
        /// The repository.
        /// </value>
        public Repository Repository { get; set; }

        /// <summary>
        /// Gets or sets the branch.
        /// </summary>
        /// <value>
        /// The branch.
        /// </value>
        public Branch Branch { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>
        /// The tag.
        /// </value>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the commit.
        /// </summary>
        /// <value>
        /// The commit.
        /// </value>
        public Commit Commit { get; set; }

        /// <summary>
        /// Gets or sets the jobs.
        /// </summary>
        /// <value>
        /// The jobs.
        /// </value>
        public IEnumerable<Job> Jobs { get; set; }

        /// <summary>
        /// Gets or sets the stages.
        /// </summary>
        /// <value>
        /// The stages.
        /// </value>
        public IEnumerable<Stage> Stages { get; set; }

        [JsonProperty(PropertyName = "created_by")]
        public Owner CreatedBy { get; set; }
    }
}