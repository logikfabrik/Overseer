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
            Changes = new[]
            {
                new Change
                {
                    Id = build.VcsRevision,
                    Changed = build.QueuedAt?.ToUniversalTime(),
                    ChangedBy = build.CommitterName,
                    Comment = build.Subject
                }
            };
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
        /// Gets the changes.
        /// </summary>
        /// <value>
        /// The changes.
        /// </value>
        public IEnumerable<IChange> Changes { get; }

        private static BuildStatus? GetStatus(Api.Models.Build build)
        {
            if (!build.StopTime.HasValue || !build.Outcome.HasValue)
            {
                return BuildStatus.InProgress;
            }

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (build.Status)
            {
                case Api.Models.BuildStatus.Success:
                case Api.Models.BuildStatus.NoTests:
                    return BuildStatus.Succeeded;

                case Api.Models.BuildStatus.Failed:
                    return BuildStatus.Failed;

                case Api.Models.BuildStatus.Canceled:
                case Api.Models.BuildStatus.TimedOut:
                case Api.Models.BuildStatus.InfrastructureFail:
                    return BuildStatus.Stopped;

                default:
                    return null;
            }
        }
    }
}
