// <copyright file="Build.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.CircleCI.Api.Models
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// The <see cref="Build" /> class.
    /// </summary>
    public class Build
    {
        /// <summary>
        /// Gets or sets the VCS URL.
        /// </summary>
        /// <value>
        /// The VCS URL.
        /// </value>
        [JsonProperty(PropertyName = "vcs_url")]
        public Uri VcsUrl { get; set; }

        /// <summary>
        /// Gets or sets the VCS revision.
        /// </summary>
        /// <value>
        /// The VCS revision.
        /// </value>
        [JsonProperty(PropertyName = "vcs_revision")]
        public string VcsRevision { get; set; }

        /// <summary>
        /// Gets or sets the build URL.
        /// </summary>
        /// <value>
        /// The build URL.
        /// </value>
        [JsonProperty(PropertyName = "build_url")]
        public Uri BuildUrl { get; set; }

        /// <summary>
        /// Gets or sets the build number.
        /// </summary>
        /// <value>
        /// The build number.
        /// </value>
        [JsonProperty(PropertyName = "build_num")]
        public int BuildNumber { get; set; }

        /// <summary>
        /// Gets or sets the branch.
        /// </summary>
        /// <value>
        /// The branch.
        /// </value>
        public string Branch { get; set; }

        /// <summary>
        /// Gets or sets the committer name.
        /// </summary>
        /// <value>
        /// The committer name.
        /// </value>
        [JsonProperty(PropertyName = "committer_name")]
        public string CommitterName { get; set; }

        /// <summary>
        /// Gets or sets the committer e-mail.
        /// </summary>
        /// <value>
        /// The committer e-mail.
        /// </value>
        [JsonProperty(PropertyName = "committer_email")]
        public string CommitterEmail { get; set; }

        /// <summary>
        /// Gets or sets the commit message subject.
        /// </summary>
        /// <value>
        /// The commit message subject.
        /// </value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the commit message body.
        /// </summary>
        /// <value>
        /// The commit message body.
        /// </value>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the queued at time.
        /// </summary>
        /// <value>
        /// The queued at time.
        /// </value>
        [JsonProperty(PropertyName = "queued_at")]
        public DateTime? QueuedAt { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        [JsonProperty(PropertyName = "start_time")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Gets or sets the stop time.
        /// </summary>
        /// <value>
        /// The stop time.
        /// </value>
        [JsonProperty(PropertyName = "stop_time")]
        public DateTime? StopTime { get; set; }

        /// <summary>
        /// Gets or sets the build time in milliseconds.
        /// </summary>
        /// <value>
        /// The build time in milliseconds.
        /// </value>
        [JsonProperty(PropertyName = "build_time_millis")]
        public long? BuildTime { get; set; }

        /// <summary>
        /// Gets or sets the project username.
        /// </summary>
        /// <value>
        /// The project username.
        /// </value>
        [JsonProperty(PropertyName = "username")]
        public string ProjectUsername { get; set; }

        /// <summary>
        /// Gets or sets the project name.
        /// </summary>
        /// <value>
        /// The project name.
        /// </value>
        [JsonProperty(PropertyName = "reponame")]
        public string ProjectName { get; set; }

        /// <summary>
        /// Gets or sets the lifecycle.
        /// </summary>
        /// <value>
        /// The lifecycle.
        /// </value>
        public BuildLifecycle? Lifecycle { get; set; }

        /// <summary>
        /// Gets or sets the outcome.
        /// </summary>
        /// <value>
        /// The outcome.
        /// </value>
        public BuildOutcome? Outcome { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public BuildStatus? Status { get; set; }
    }
}
