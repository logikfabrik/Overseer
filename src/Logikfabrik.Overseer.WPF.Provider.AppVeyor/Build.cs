// <copyright file="Build.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor
{
    using System;
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

            Version = build.Version;
            Branch = build.Branch;
            Started = build.Started;
            Finished = build.Finished;
            Revision = build.CommitId;
            CommitterName = build.CommitterName;
            RequestorName = build.AuthorName;
            Comment = build.Message;
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; }

        /// <summary>
        /// Gets the branch.
        /// </summary>
        /// <value>
        /// The branch.
        /// </value>
        public string Branch { get; }

        /// <summary>
        /// Gets the started date.
        /// </summary>
        /// <value>
        /// The started date.
        /// </value>
        public DateTime? Started { get; }

        /// <summary>
        /// Gets the finished date.
        /// </summary>
        /// <value>
        /// The finished date.
        /// </value>
        public DateTime? Finished { get; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public BuildStatus? Status { get; }

        /// <summary>
        /// Gets the revision.
        /// </summary>
        /// <value>
        /// The revision.
        /// </value>
        public string Revision { get; }

        /// <summary>
        /// Gets the name of the committer.
        /// </summary>
        /// <value>
        /// The name of the committer.
        /// </value>
        public string CommitterName { get; }

        /// <summary>
        /// Gets the name of the requestor.
        /// </summary>
        /// <value>
        /// The name of the requestor.
        /// </value>
        public string RequestorName { get; }

        /// <summary>
        /// Gets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; }
    }
}
