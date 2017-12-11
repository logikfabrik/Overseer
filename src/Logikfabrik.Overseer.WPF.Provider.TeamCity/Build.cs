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
            WebUrl = build.WebUrl;
            Changes = build.LastChanges?.Change.Select(change => new Change(change.Version, change.Date?.ToUniversalTime(), change.Username, change.Comment?.Trim())).ToArray() ?? new IChange[] { };
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

        private static string GetRequestedBy(Api.Models.Build build)
        {
            return build.Triggered?.User?.Username;
        }

        private static BuildStatus? GetStatus(Api.Models.Build build)
        {
            switch (build.State)
            {
                case Api.Models.BuildState.Queued:
                    return BuildStatus.Queued;

                case Api.Models.BuildState.Running:
                    return BuildStatus.InProgress;

                case Api.Models.BuildState.Finished:
                    switch (build.Status)
                    {
                        case Api.Models.BuildStatus.Success:
                            return BuildStatus.Succeeded;

                        case Api.Models.BuildStatus.Failure:
                        case Api.Models.BuildStatus.Error:
                            return BuildStatus.Failed;

                        default:
                            return null;
                    }

                default:
                    return null;
            }
        }
    }
}
