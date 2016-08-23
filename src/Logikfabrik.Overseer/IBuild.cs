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
        /// Gets the build number.
        /// </summary>
        /// <value>
        /// The build number.
        /// </value>
        string BuildNumber { get; }

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
    }
}
