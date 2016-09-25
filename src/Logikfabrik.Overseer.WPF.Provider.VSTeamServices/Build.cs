// <copyright file="Build.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices
{
    using System;
    using EnsureThat;

    /// <summary>
    /// The <see cref="Build" /> class.
    /// </summary>
    public class Build : IBuild
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Build"/> class.
        /// </summary>
        /// <param name="build">The build.</param>
        public Build(Api.Models.Build build)
        {
            Ensure.That(build).IsNotNull();

            Number = build.BuildNumber;
            Branch = build.SourceBranch;
            Started = build.StartTime;
            Finished = build.FinishTime;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Build" /> class.
        /// </summary>
        /// <param name="build">The build.</param>
        /// <param name="lastChangeset">The last changeset.</param>
        public Build(Api.Models.Build build, Api.Models.Changeset lastChangeset)
            : this(build)
        {
            Ensure.That(lastChangeset).IsNotNull();

            LastChange = new Change
            {
                Changed = lastChangeset.CreatedDate,
                ChangedBy = lastChangeset.CheckedInBy.DisplayName,
                Comment = lastChangeset.Comment
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Build" /> class.
        /// </summary>
        /// <param name="build">The build.</param>
        /// <param name="lastCommit">The last commit.</param>
        public Build(Api.Models.Build build, Api.Models.Commit lastCommit)
            : this(build)
        {
            Ensure.That(lastCommit).IsNotNull();

            LastChange = new Change
            {
                ChangedBy = lastCommit.Committer.Name,
                Comment = lastCommit.Comment
            };
        }

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
        /// Gets the name of whoever requested the build.
        /// </summary>
        /// <value>
        /// The name of whoever requested the build.
        /// </value>
        public string RequestedBy { get; }

        /// <summary>
        /// Gets the last change.
        /// </summary>
        /// <value>
        /// The last change.
        /// </value>
        public IChange LastChange { get; }
    }
}
