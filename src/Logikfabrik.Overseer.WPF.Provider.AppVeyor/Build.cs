// <copyright file="Build.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor
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
        /// <param name="project">The project.</param>
        /// <param name="build">The build.</param>
        public Build(Api.Models.Project project, Api.Models.Build build)
        {
            Ensure.That(project).IsNotNull();
            Ensure.That(build).IsNotNull();

            Id = build.BuildId.ToString();
            Version = build.Version;
            Number = null;
            Branch = build.Branch;
            StartTime = build.Started?.ToUniversalTime();
            EndTime = build.Finished?.ToUniversalTime();
            Status = GetStatus(build);
            RequestedBy = build.AuthorUsername;
            WebUrl = GetWebUrl(project, build);
            Changes = new[]
            {
                new Change(build.CommitId, build.Committed?.ToUniversalTime(), build.CommitterName, build.Message?.Trim())
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
                case Api.Models.BuildStatus.Running:
                    return BuildStatus.InProgress;

                case Api.Models.BuildStatus.Queued:
                    return BuildStatus.Queued;

                case Api.Models.BuildStatus.Success:
                    return BuildStatus.Succeeded;

                case Api.Models.BuildStatus.Failed:
                    return BuildStatus.Failed;

                case Api.Models.BuildStatus.Canceled:
                    return BuildStatus.Stopped;

                default:
                    return null;
            }
        }

        private static Uri GetWebUrl(Api.Models.Project project, Api.Models.Build build)
        {
            var builder = new UriBuilder(UriUtility.BaseUri)
            {
                Path = $"project/{project.AccountName}/{project.Slug}/build/{build.Version}"
            };

            return builder.Uri;
        }
    }
}
