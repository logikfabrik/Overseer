﻿// <copyright file="BuildTrackerProjectErrorEventArgs.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;

    /// <summary>
    /// The <see cref="BuildTrackerProjectErrorEventArgs" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class BuildTrackerProjectErrorEventArgs : BuildTrackerProjectEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildTrackerProjectErrorEventArgs" /> class.
        /// </summary>
        /// <param name="settingsId">The settings identifier.</param>
        /// <param name="project">The project.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public BuildTrackerProjectErrorEventArgs(Guid settingsId, IProject project)
            : base(settingsId, project)
        {
        }
    }
}
