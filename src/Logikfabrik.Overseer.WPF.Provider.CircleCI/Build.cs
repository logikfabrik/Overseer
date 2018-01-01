// <copyright file="Build.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.CircleCI
{
    using System;
    using System.Collections.Generic;
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
        public Build(Api.Models.Build build)
        {
            Ensure.That(build).IsNotNull();

            Id = string.Concat(build.ProjectName, build.BuildNumber);
            Version = null;
            Number = build.BuildNumber.ToString();
            Branch = build.Branch;
            StartTime = build.StartTime?.ToUniversalTime();
            EndTime = build.StopTime?.ToUniversalTime();
            Status = GetStatus(build);
            RequestedBy = null;
            WebUrl = build.BuildUrl;
            Changes = new[]
            {
                new Change(build.VcsRevision, build.QueuedAt?.ToUniversalTime(), build.CommitterName, build.Subject?.Trim())
            };
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
                case Api.Models.BuildStatus.Queued:
                    return BuildStatus.Queued;

                case Api.Models.BuildStatus.Success:
                    return BuildStatus.Succeeded;

                case Api.Models.BuildStatus.Failed:
                case Api.Models.BuildStatus.NoTests:
                    return BuildStatus.Failed;

                case Api.Models.BuildStatus.Canceled:
                case Api.Models.BuildStatus.TimedOut:
                case Api.Models.BuildStatus.InfrastructureFail:
                    return BuildStatus.Stopped;

                default:
                    if (!build.StopTime.HasValue || !build.Outcome.HasValue)
                    {
                        return BuildStatus.InProgress;
                    }

                    return null;
            }
        }
    }
}
