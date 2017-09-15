// <copyright file="Build.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor
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
