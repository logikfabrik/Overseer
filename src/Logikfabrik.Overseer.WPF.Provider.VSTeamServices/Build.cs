// <copyright file="Build.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
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
    // ReSharper disable once InheritdocConsiderUsage
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
            Changes = changes.Select(lastChange => new Change(lastChange.Id, lastChange.Timestamp?.ToUniversalTime(), lastChange.Author.DisplayName, lastChange.Message?.Trim())).ToArray();
        }

        /// <inheritdoc />
        public string Id { get; }

        /// <inheritdoc />
        public string Version { get; }

        /// <inheritdoc />
        public string Number { get; }

        /// <inheritdoc />
        public string Branch { get; }

        /// <inheritdoc />
        public DateTime? StartTime { get; }

        /// <inheritdoc />
        public DateTime? EndTime { get; }

        /// <inheritdoc />
        public BuildStatus? Status { get; }

        /// <inheritdoc />
        public string RequestedBy { get; }

        /// <inheritdoc />
        public Uri WebUrl { get; }

        /// <inheritdoc />
        public IEnumerable<IChange> Changes { get; }

        private static BuildStatus? GetStatus(Api.Models.Build build)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (build.Status)
            {
                case Api.Models.BuildStatus.InProgress:
                    return BuildStatus.InProgress;

                case Api.Models.BuildStatus.Cancelling:
                    return BuildStatus.Stopped;

                case Api.Models.BuildStatus.NotStarted:
                case Api.Models.BuildStatus.Postponed:
                    return BuildStatus.Queued;

                case Api.Models.BuildStatus.Completed:
                    // ReSharper disable once SwitchStatementMissingSomeCases
                    switch (build.Result)
                    {
                        case Api.Models.BuildResult.Canceled:
                            return BuildStatus.Stopped;

                        case Api.Models.BuildResult.Failed:
                            return BuildStatus.Failed;

                        case Api.Models.BuildResult.PartiallySucceeded:
                        case Api.Models.BuildResult.Succeeded:
                            return BuildStatus.Succeeded;

                        default:
                            return null;
                    }

                default:
                    return null;
            }
        }
    }
}