// <copyright file="BuildViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildViewModel" /> class.
    /// </summary>
    public class BuildViewModel : PropertyChangedBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildViewModel" /> class.
        /// </summary>
        /// <param name="build">The build.</param>
        public BuildViewModel(IBuild build)
        {
            Ensure.That(build).IsNotNull();

            Version = build.Version;
            Branch = build.Branch;
            Started = build.Started;
            Finished = build.Finished;
            Revision = build.Revision;
            CommitterName = build.CommitterName;
            RequestorName = build.RequestorName;
            Comment = build.Comment;
        }

        /// <summary>
        /// Gets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; }

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
        /// Gets the branch.
        /// </summary>
        /// <value>
        /// The branch.
        /// </value>
        public string Branch { get; }

        /// <summary>
        /// Gets the revision.
        /// </summary>
        /// <value>
        /// The revision.
        /// </value>
        public string Revision { get; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; }

        /// <summary>
        /// Gets the started date.
        /// </summary>
        /// <value>
        /// The started date.
        /// </value>
        public DateTime? Started { get; }

        /// <summary>
        /// Gets finished date.
        /// </summary>
        /// <value>
        /// The finished date.
        /// </value>
        public DateTime? Finished { get; }
    }
}
