// <copyright file="Build.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Api.Models
{
    using System;

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
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the build type identifier.
        /// </summary>
        /// <value>
        /// The build type identifier.
        /// </value>
        public string BuildTypeId { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public BuildStatus? Status { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public BuildState? State { get; set; }

        /// <summary>
        /// Gets or sets the web URL.
        /// </summary>
        /// <value>
        /// The web URL.
        /// </value>
        public Uri WebUrl { get; set; }

        /// <summary>
        /// Gets or sets the branch name.
        /// </summary>
        /// <value>
        /// The branch name.
        /// </value>
        public string BranchName { get; set; }

        /// <summary>
        /// Gets or sets the queued date.
        /// </summary>
        /// <value>
        /// The queued date.
        /// </value>
        public DateTime? QueuedDate { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the finish date.
        /// </summary>
        /// <value>
        /// The finish date.
        /// </value>
        public DateTime? FinishDate { get; set; }

        /// <summary>
        /// Gets or sets the last changes.
        /// </summary>
        /// <value>
        /// The last changes.
        /// </value>
        public Changes LastChanges { get; set; }

        /// <summary>
        /// Gets or sets the test occurrences.
        /// </summary>
        /// <value>
        /// The test occurrences.
        /// </value>
        public TestOccurrences TestOccurrences { get; set; }
    }
}