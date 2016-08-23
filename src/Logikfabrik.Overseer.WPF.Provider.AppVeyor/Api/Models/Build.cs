// <copyright file="Build.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.Api.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="Build" /> class.
    /// </summary>
    public class Build
    {
        /// <summary>
        /// Gets or sets the build identifier.
        /// </summary>
        /// <value>
        /// The build identifier.
        /// </value>
        public int BuildId { get; set; }

        /// <summary>
        /// Gets or sets the jobs.
        /// </summary>
        /// <value>
        /// The jobs.
        /// </value>
        public IEnumerable<Job> Jobs { get; set; }

        /// <summary>
        /// Gets or sets the build number.
        /// </summary>
        /// <value>
        /// The build number.
        /// </value>
        public int BuildNumber { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the branch.
        /// </summary>
        /// <value>
        /// The branch.
        /// </value>
        public string Branch { get; set; }

        /// <summary>
        /// Gets or sets the commit identifier.
        /// </summary>
        /// <value>
        /// The commit identifier.
        /// </value>
        public string CommitId { get; set; }

        /// <summary>
        /// Gets or sets the author name.
        /// </summary>
        /// <value>
        /// The author name.
        /// </value>
        public string AuthorName { get; set; }

        /// <summary>
        /// Gets or sets the author username.
        /// </summary>
        /// <value>
        /// The author username.
        /// </value>
        public string AuthorUsername { get; set; }

        /// <summary>
        /// Gets or sets the committer name.
        /// </summary>
        /// <value>
        /// The committer name.
        /// </value>
        public string CommitterName { get; set; }

        /// <summary>
        /// Gets or sets the committer username.
        /// </summary>
        /// <value>
        /// The committer username.
        /// </value>
        public string CommitterUsername { get; set; }

        /// <summary>
        /// Gets or sets the committed date.
        /// </summary>
        /// <value>
        /// The committed date.
        /// </value>
        public DateTime? Committed { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        public IEnumerable<string> Messages { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the started date.
        /// </summary>
        /// <value>
        /// The started date.
        /// </value>
        public DateTime? Started { get; set; }

        /// <summary>
        /// Gets or sets the finished date.
        /// </summary>
        /// <value>
        /// The finished date.
        /// </value>
        public DateTime? Finished { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime? Created { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public DateTime? Updated { get; set; }
    }
}