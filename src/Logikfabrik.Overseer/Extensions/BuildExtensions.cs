// <copyright file="BuildExtensions.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Extensions
{
    /// <summary>
    /// The <see cref="BuildExtensions" /> class.
    /// </summary>
    public static class BuildExtensions
    {
        /// <summary>
        /// Gets whether the specified <see cref="IBuild" /> is finished.
        /// </summary>
        /// <param name="build">The build.</param>
        /// <returns><c>true</c> if finished; otherwise <c>false</c>.</returns>
        public static bool IsFinished(this IBuild build)
        {
            var status = build.Status;

            if (status == null)
            {
                return false;
            }

            return status != BuildStatus.InProgress;
        }

        /// <summary>
        /// Gets whether the specified <see cref="IBuild" /> is in progress.
        /// </summary>
        /// <param name="build">The build.</param>
        /// <returns><c>true</c> if in progress; otherwise <c>false</c>.</returns>
        public static bool IsInProgress(this IBuild build)
        {
            var status = build.Status;

            return status == BuildStatus.InProgress;
        }
    }
}
