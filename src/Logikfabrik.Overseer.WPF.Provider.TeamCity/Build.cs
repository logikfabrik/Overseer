// <copyright file="Build.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity
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

            Id = build.Id;
            Number = build.Number;

            // TODO: This!
        }

        public string Id { get; }
        public string Version { get; }
        public string Number { get; }
        public string Branch { get; }
        public DateTime? StartTime { get; }
        public DateTime? EndTime { get; }
        public BuildStatus? Status { get; }
        public string RequestedBy { get; }
        public IEnumerable<IChange> Changes { get; }
    }
}
