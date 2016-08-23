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
        Failed,

        /// <summary>
        /// The build succeeded.
        /// </summary>
        Succeeded,

        /// <summary>
        /// The build is in progress.
        /// </summary>
        InProgress,

        /// <summary>
        /// The build was stopped.
        /// </summary>
        Stopped
    }
}
