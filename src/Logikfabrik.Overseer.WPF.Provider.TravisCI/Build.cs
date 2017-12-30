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
            //WebUrl = build.WebUrl;
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
            // TODO: This check!
            return null;
        }

        private static BuildStatus? GetStatus(Api.Models.Build build)
        {
            // TODO: This status check.
            switch (build.State)
            {
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
                return new IChange[] {};
            }

            return new[]
            {
                new Change(commit.Sha, commit.CommittedAt, commit.Committer?.Name, commit.Message)
            };
        }
    }
}
