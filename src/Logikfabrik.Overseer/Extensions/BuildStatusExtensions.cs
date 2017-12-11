﻿// <copyright file="BuildStatusExtensions.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Extensions
{
    using System.Linq;

    /// <summary>
    /// The <see cref="BuildStatusExtensions" /> class.
    /// </summary>
    public static class BuildStatusExtensions
    {
        /// <summary>
        /// Determines whether this status is in progress or finished.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns><c>true</c> if in progress or finished; otherwise, <c>false</c>.</returns>
        public static bool IsInProgressOrFinished(this BuildStatus? status)
        {
            return IsInProgress(status) || IsFinished(status);
        }

        /// <summary>
        /// Determines whether this status is finished.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns><c>true</c> if finished; otherwise, <c>false</c>.</returns>
        public static bool IsFinished(this BuildStatus? status)
        {
            return status != null && new[] { BuildStatus.Failed, BuildStatus.Succeeded, BuildStatus.Stopped }.Contains(status.Value);
        }

        /// <summary>
        /// Determines whether this status is in progress.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns><c>true</c> if in progress; otherwise, <c>false</c>.</returns>
        public static bool IsInProgress(this BuildStatus? status)
        {
            return status == BuildStatus.InProgress;
        }

        /// <summary>
        /// Determines whether this status is queued.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns><c>true</c> if queued; otherwise, <c>false</c>.</returns>
        public static bool IsQueued(this BuildStatus? status)
        {
            return status == BuildStatus.Queued;
        }
    }
}
