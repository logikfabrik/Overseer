// <copyright file="Build.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.Codeship
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

            Id = build.Id.ToString();
            Number = null;
            Version = null;
            Branch = build.Reference;
            StartTime = build.QueuedAt?.ToUniversalTime();
            EndTime = build.FinishedAt?.ToUniversalTime();
            Status = GetStatus(build);
            RequestedBy = build.Username;
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

        private static BuildStatus? GetStatus(Api.Models.Build build)
        {
            switch (build.Status)
            {
                case Api.Models.BuildStatus.Waiting:
                    return BuildStatus.Queued;

                case Api.Models.BuildStatus.Started:
                case Api.Models.BuildStatus.Testing:
                    return BuildStatus.InProgress;

                case Api.Models.BuildStatus.Failed:
                    return BuildStatus.Failed;

                case Api.Models.BuildStatus.Success:
                case Api.Models.BuildStatus.Recovered:
                    return BuildStatus.Succeeded;

                default:
                    return null;
            }
        }

        private static IEnumerable<IChange> GetChanges(Api.Models.Build build)
        {
            if (string.IsNullOrWhiteSpace(build.CommitSha) && string.IsNullOrWhiteSpace(build.CommitMessage))
            {
                return new IChange[] { };
            }

            return new[] { new Change(build.CommitSha, build.QueuedAt, build.Username, build.CommitMessage) };
        }
    }
}
