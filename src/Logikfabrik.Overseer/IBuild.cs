// <copyright file="IBuild.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;

    /// <summary>
    /// The <see cref="IBuild" /> interface.
    /// </summary>
    public interface IBuild
    {
        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        string Version { get; }

        /// <summary>
        /// Gets the branch.
        /// </summary>
        /// <value>
        /// The branch.
        /// </value>
        string Branch { get; }

        /// <summary>
        /// Gets the started date.
        /// </summary>
        /// <value>
        /// The started date.
        /// </value>
        DateTime? Started { get; }

        /// <summary>
        /// Gets the finished date.
        /// </summary>
        /// <value>
        /// The finished date.
        /// </value>
        DateTime? Finished { get; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        BuildStatus? Status { get; }

        /// <summary>
        /// Gets the revision.
        /// </summary>
        /// <value>
        /// The revision.
        /// </value>
        string Revision { get; }

        /// <summary>
        /// Gets the name of the committer.
        /// </summary>
        /// <value>
        /// The name of the committer.
        /// </value>
        string CommitterName { get; }

        /// <summary>
        /// Gets the name of the requestor.
        /// </summary>
        /// <value>
        /// The name of the requestor.
        /// </value>
        string RequestorName { get; }

        /// <summary>
        /// Gets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        string Comment { get; }
    }
}
