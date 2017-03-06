// <copyright file="Build.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity
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
        public Build(Api.Models.Build build)
        {
            Ensure.That(build).IsNotNull();

            Id = build.Id;
            Number = build.Number;
            Version = null;
            Branch = build.BranchName;
            StartTime = build.StartDate?.ToUniversalTime();
            EndTime = build.FinishDate?.ToUniversalTime();
            Status = GetStatus(build);
            RequestedBy = GetRequestedBy(build);

            if (build.LastChanges != null)
            {
                Changes = build.LastChanges.Change.Select(change => new Change
                {
                    Id = change.Id,
                    Changed = change.Date?.ToUniversalTime(),
                    ChangedBy = change.Username
                }).ToArray();
            }
            else
            {
                Changes = new IChange[] { };
            }
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
        /// The number.
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

        private static string GetRequestedBy(Api.Models.Build build)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (build.Triggered.Type)
            {
                case Api.Models.TriggerType.Vcs:
                    return build.Triggered.Details;

                case Api.Models.TriggerType.User:
                    return build.Triggered.User?.Username;

                default:
                    return null;
            }
        }

        private static BuildStatus? GetStatus(Api.Models.Build build)
        {
            if (!build.FinishDate.HasValue)
            {
                return BuildStatus.InProgress;
            }

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (build.Status)
            {
                case Api.Models.BuildStatus.Success:
                    return BuildStatus.Succeeded;

                case Api.Models.BuildStatus.Failure:
                    return BuildStatus.Failed;

                case Api.Models.BuildStatus.Error:
                    return BuildStatus.Stopped;

                default:
                    return null;
            }
        }
    }
}
