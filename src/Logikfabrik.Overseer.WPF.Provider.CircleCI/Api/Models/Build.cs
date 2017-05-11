// <copyright file="Build.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.CircleCI.Api.Models
{
    using System;

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
        // ReSharper disable once InconsistentNaming
        public Uri Vcs_url { get; set; }

        /// <summary>
        /// Gets or sets the VCS revision.
        /// </summary>
        /// <value>
        /// The VCS revision.
        /// </value>
        // ReSharper disable once InconsistentNaming
        public string Vcs_revision { get; set; }

        /// <summary>
        /// Gets or sets the build URL.
        /// </summary>
        /// <value>
        /// The build URL.
        /// </value>
        // ReSharper disable once InconsistentNaming
        public Uri Build_url { get; set; }

        /// <summary>
        /// Gets or sets the build number.
        /// </summary>
        /// <value>
        /// The build number.
        /// </value>
        // ReSharper disable once InconsistentNaming
        public int Build_num { get; set; }

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
        // ReSharper disable once InconsistentNaming
        public string Committer_name { get; set; }

        /// <summary>
        /// Gets or sets the committer e-mail.
        /// </summary>
        /// <value>
        /// The committer e-mail.
        /// </value>
        // ReSharper disable once InconsistentNaming
        public string Committer_email { get; set; }

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

        // ReSharper disable once InconsistentNaming
        public DateTime? Queued_at { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        // ReSharper disable once InconsistentNaming
        public DateTime? Start_time { get; set; }

        /// <summary>
        /// Gets or sets the stop time.
        /// </summary>
        /// <value>
        /// The stop time.
        /// </value>
        // ReSharper disable once InconsistentNaming
        public DateTime? Stop_time { get; set; }

        /// <summary>
        /// Gets or sets the build time in milliseconds.
        /// </summary>
        /// <value>
        /// The build time in milliseconds.
        /// </value>
        // ReSharper disable once InconsistentNaming
        public long? Build_time_millis { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the repository name.
        /// </summary>
        /// <value>
        /// The repository name.
        /// </value>
        public string Reponame { get; set; }

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
