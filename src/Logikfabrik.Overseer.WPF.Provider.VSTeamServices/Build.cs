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
            RequestedBy = build.RequestedFor.DisplayName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Build" /> class.
        /// </summary>
        /// <param name="build">The build.</param>
        /// <param name="lastChange">The last change.</param>
        public Build(Api.Models.Build build, Api.Models.Change lastChange)
            : this(build)
        {
            Ensure.That(lastChange).IsNotNull();

            LastChange = new Change
            {
                Id = lastChange.Id,
                Changed = lastChange.Timestamp,
                ChangedBy = lastChange.Author.DisplayName,
                Comment = lastChange.Message
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
