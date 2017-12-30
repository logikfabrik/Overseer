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
            Changes = GetChanges(build);
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

        private static IEnumerable<IChange> GetChanges(Api.Models.Build build)
        {
            return build.LastChanges?.Change.Select(change => new Change(change.Version, change.Date?.ToUniversalTime(), change.Username, change.Comment?.Trim())).ToArray() ?? new IChange[] { };
        }
    }
}
