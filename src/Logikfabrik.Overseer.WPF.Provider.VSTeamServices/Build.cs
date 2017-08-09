// <copyright file="Build.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;

    /// <summary>
    /// The <see cref="Build" /> class.
    /// </summary>
    public class Build : IBuild
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Build" /> class.
        /// </summary>
        /// <param name="build">The build.</param>
        /// <param name="changes">The changes.</param>
        public Build(Api.Models.Build build, IEnumerable<Api.Models.Change> changes)
        {
            Ensure.That(build).IsNotNull();
            Ensure.That(changes).IsNotNull();

            Id = build.Id;
            Version = null;
            Number = build.BuildNumber;
            Branch = build.SourceBranch;
            StartTime = build.StartTime?.ToUniversalTime();
            EndTime = build.FinishTime?.ToUniversalTime();
            Status = GetStatus(build);
            RequestedBy = build.RequestedFor.DisplayName;
            WebUrl = build.Url;
            Changes = changes.Select(lastChange => new Change(lastChange.Id, lastChange.Timestamp?.ToUniversalTime(), lastChange.Author.DisplayName, lastChange.Message)).ToArray();
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; }

        /// <summary>
        /// Gets the number.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        public string Number { get; }

        /// <summary>
        /// Gets the branch.
        /// </summary>
        /// <value>
        /// The branch.
        /// </value>
        public string Branch { get; }

        /// <summary>
        /// Gets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        public DateTime? StartTime { get; }

        /// <summary>
        /// Gets the end time.
        /// </summary>
        /// <value>
        /// The end time.
        /// </value>
        public DateTime? EndTime { get; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public BuildStatus? Status { get; }

        /// <summary>
        /// Gets the name of whoever requested the build.
        /// </summary>
        /// <value>
        /// The name of whoever requested the build.
        /// </value>
        public string RequestedBy { get; }

        /// <summary>
        /// Gets the web URL.
        /// </summary>
        /// <value>
        /// The web URL.
        /// </value>
        public Uri WebUrl { get; }

        /// <summary>
        /// Gets the changes.
        /// </summary>
        /// <value>
        /// The changes.
        /// </value>
        public IEnumerable<IChange> Changes { get; }

        private static BuildStatus? GetStatus(Api.Models.Build build)
        {
            if (!build.FinishTime.HasValue)
            {
                return BuildStatus.InProgress;
            }

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (build.Result)
            {
                case Api.Models.BuildResult.Canceled:
                    return BuildStatus.Stopped;

                case Api.Models.BuildResult.Succeeded:
                case Api.Models.BuildResult.PartiallySucceeded:
                    return BuildStatus.Succeeded;

                case Api.Models.BuildResult.Failed:
                    return BuildStatus.Failed;

                default:
                    return null;
            }
        }
    }
}
