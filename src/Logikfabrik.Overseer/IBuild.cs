// <copyright file="IBuild.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="IBuild" /> interface.
    /// </summary>
    public interface IBuild
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        string Id { get; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        string Version { get; }

        /// <summary>
        /// Gets the number.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        string Number { get; }

        /// <summary>
        /// Gets the branch.
        /// </summary>
        /// <value>
        /// The branch.
        /// </value>
        string Branch { get; }

        /// <summary>
        /// Gets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        DateTime? StartTime { get; }

        /// <summary>
        /// Gets the end time.
        /// </summary>
        /// <value>
        /// The end time.
        /// </value>
        DateTime? EndTime { get; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        BuildStatus? Status { get; }

        /// <summary>
        /// Gets the name of whoever requested the build.
        /// </summary>
        /// <value>
        /// The name of whoever requested the build.
        /// </value>
        string RequestedBy { get; }

        /// <summary>
        /// Gets the changes.
        /// </summary>
        /// <value>
        /// The changes.
        /// </value>
        IEnumerable<IChange> Changes { get; }

        /// <summary>
        /// Gets the web URL.
        /// </summary>
        /// <value>
        /// The web URL.
        /// </value>
        Uri WebUrl { get; }
    }
}
