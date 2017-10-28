// <copyright file="BuildTrackerConnectionErrorEventArgs.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;

    /// <summary>
    /// The <see cref="BuildTrackerConnectionErrorEventArgs" /> class.
    /// </summary>
    public class BuildTrackerConnectionErrorEventArgs : BuildTrackerConnectionEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildTrackerConnectionErrorEventArgs" /> class.
        /// </summary>
        /// <param name="settingsId">The settings identifier.</param>
        public BuildTrackerConnectionErrorEventArgs(Guid settingsId)
            : base(settingsId)
        {
        }
    }
}