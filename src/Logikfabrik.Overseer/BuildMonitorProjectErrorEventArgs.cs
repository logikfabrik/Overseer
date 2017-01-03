// <copyright file="BuildMonitorProjectErrorEventArgs.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;

    /// <summary>
    /// The <see cref="BuildMonitorProjectErrorEventArgs" /> class.
    /// </summary>
    public class BuildMonitorProjectErrorEventArgs : BuildMonitorProjectEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildMonitorProjectErrorEventArgs" /> class.
        /// </summary>
        /// <param name="settingsId">The settings identifier.</param>
        /// <param name="project">The project.</param>
        public BuildMonitorProjectErrorEventArgs(Guid settingsId, IProject project)
            : base(settingsId, project)
        {
        }
    }
}
