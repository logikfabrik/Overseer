// <copyright file="BuildStatus.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    /// <summary>
    /// The <see cref="BuildStatus" /> enumeration.
    /// </summary>
    public enum BuildStatus
    {
        /// <summary>
        /// The build failed.
        /// </summary>
        Failed = 0,

        /// <summary>
        /// The build succeeded.
        /// </summary>
        Succeeded = 1,

        /// <summary>
        /// The build is in progress.
        /// </summary>
        InProgress = 2,

        /// <summary>
        /// The build was stopped.
        /// </summary>
        Stopped = 3
    }
}
