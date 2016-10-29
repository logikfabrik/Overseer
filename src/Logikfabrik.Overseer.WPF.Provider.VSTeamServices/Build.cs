// <copyright file="Build.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        /// <param name="lastChanges">The last changes.</param>
        public Build(Api.Models.Build build, IEnumerable<Api.Models.Change> lastChanges)
        {
            Ensure.That(build).IsNotNull();
            Ensure.That(lastChanges).IsNotNull();

            Id = build.Id;
            Number = build.BuildNumber;
            Branch = build.SourceBranch;
            Started = build.StartTime?.ToUniversalTime();
            Finished = build.FinishTime?.ToUniversalTime();
            RequestedBy = build.RequestedFor.DisplayName;
            LastChanges = lastChanges.Select(lastChange => new Change
            {
                Id = lastChange.Id,
                Changed = lastChange.Timestamp?.ToUniversalTime(),
                ChangedBy = lastChange.Author.DisplayName,
                Comment = lastChange.Message
            });
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
        /// Gets the last changes.
        /// </summary>
        /// <value>
        /// The last changes.
        /// </value>
        public IEnumerable<IChange> LastChanges { get; }
    }
}
