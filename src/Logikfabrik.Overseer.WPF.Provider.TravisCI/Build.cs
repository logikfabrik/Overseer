// <copyright file="Build.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI
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

            Id = build.Id.ToString();
            Number = build.Number;
            Version = null;
            Branch = build.Branch?.Name;
            StartTime = build.StartedAt?.ToUniversalTime();
            EndTime = build.FinishedAt?.ToUniversalTime();
            Status = GetStatus(build);
            RequestedBy = GetRequestedBy(build);
            WebUrl = null;
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
            return build.CreatedBy?.Login;
        }

        private static BuildStatus? GetStatus(Api.Models.Build build)
        {
            switch (build.State)
            {
                case "created":
                    return BuildStatus.Queued;

                case "booting":
                case "started":
                    return BuildStatus.InProgress;

                case "passed":
                    return BuildStatus.Succeeded;

                case "failed":
                    return BuildStatus.Failed;

                default:
                    return null;
            }
        }

        private static IEnumerable<IChange> GetChanges(Api.Models.Build build)
        {
            var commit = build.Commit;

            if (commit == null)
            {
                return new IChange[] { };
            }

            return new[]
            {
                new Change(commit.Sha, commit.CommittedAt.ToUniversalTime(), commit.Committer?.Name, commit.Message)
            };
        }
    }
}
