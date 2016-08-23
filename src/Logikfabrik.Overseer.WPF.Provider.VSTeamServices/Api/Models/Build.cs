// <copyright file="Build.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.Api.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="Build" /> class.
    /// </summary>
    public class Build
    {
        /// <summary>
        /// Gets or sets the build number.
        /// </summary>
        /// <value>
        /// The build number.
        /// </value>
        public string BuildNumber { get; set; }

        /// <summary>
        /// Gets or sets the finish time.
        /// </summary>
        /// <value>
        /// The finish time.
        /// </value>
        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets who made the last changes.
        /// </summary>
        /// <value>
        /// Who made the last changes.
        /// </value>
        public IdentityRef LastChangedBy { get; set; }

        /// <summary>
        /// Gets or sets the last changed date.
        /// </summary>
        /// <value>
        /// The last changed date.
        /// </value>
        public DateTime LastChangedDate { get; set; }

        /// <summary>
        /// Gets or sets the project.
        /// </summary>
        /// <value>
        /// The project.
        /// </value>
        public Project Project { get; set; }

        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        /// <value>
        /// The repository.
        /// </value>
        public Repository Repository { get; set; }

        /// <summary>
        /// Gets or sets who requested this build.
        /// </summary>
        /// <value>
        /// Who requested this build.
        /// </value>
        public IdentityRef RequestedBy { get; set; }

        /// <summary>
        /// Gets or sets who this build was requested for.
        /// </summary>
        /// <value>
        /// Who this build was requested for.
        /// </value>
        public IdentityRef RequestedFor { get; set; }

        /// <summary>
        /// Gets or sets the build result.
        /// </summary>
        /// <value>
        /// The build result.
        /// </value>
        public BuildResult? Result { get; set; }

        /// <summary>
        /// Gets or sets the source branch.
        /// </summary>
        /// <value>
        /// The source branch.
        /// </value>
        public string SourceBranch { get; set; }

        /// <summary>
        /// Gets or sets the source version.
        /// </summary>
        /// <value>
        /// The source version.
        /// </value>
        public string SourceVersion { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Gets or sets the build status.
        /// </summary>
        /// <value>
        /// The build status.
        /// </value>
        public BuildStatus? Status { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public IEnumerable<string> Tags { get; set; }

        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>
        /// The URI.
        /// </value>
        public Uri Uri { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }
    }
}