﻿// <copyright file="BuildExtensions.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Extensions
{
    using System;

    /// <summary>
    /// The <see cref="BuildExtensions" /> class.
    /// </summary>
    public static class BuildExtensions
    {
        /// <summary>
        /// Gets whether the specified <see cref="IBuild" /> is finished.
        /// </summary>
        /// <param name="build">The build.</param>
        /// <returns><c>true</c> if finished; otherwise, <c>false</c>.</returns>
        public static bool IsFinished(this IBuild build)
        {
            return build.Status.IsFinished();
        }

        /// <summary>
        /// Gets whether the specified <see cref="IBuild" /> is in progress.
        /// </summary>
        /// <param name="build">The build.</param>
        /// <returns><c>true</c> if in progress; otherwise, <c>false</c>.</returns>
        public static bool IsInProgress(this IBuild build)
        {
            return build.Status.IsInProgress();
        }

        /// <summary>
        /// Gets the version number of the specified <see cref="IBuild" />.
        /// </summary>
        /// <param name="build">The build.</param>
        /// <returns>The version number.</returns>
        public static string VersionNumber(this IBuild build)
        {
            return !string.IsNullOrWhiteSpace(build.Version) ? build.Version : build.Number;
        }

        /// <summary>
        /// Gets the run time of the specified <see cref="IBuild" />.
        /// </summary>
        /// <param name="build">The build.</param>
        /// <returns>The run time.</returns>
        public static TimeSpan? RunTime(this IBuild build)
        {
            if (build.IsInProgress())
            {
                return DateTime.UtcNow - build.StartTime;
            }

            if (build.IsFinished())
            {
                return build.EndTime - build.StartTime;
            }

            return null;
        }
    }
}
