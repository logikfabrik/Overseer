// <copyright file="Build.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.Codeship
{
    using System;
    using System.Collections.Generic;
    using EnsureThat;

    /// <summary>
    /// The <see cref="Build" /> class.
    /// </summary>
    public class Build : IBuild
    {
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
            //WebUrl = build.WebUrl;
            Changes = GetChanges(build);
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
