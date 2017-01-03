// <copyright file="BuildMonitorConnectionErrorEventArgs.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;

    /// <summary>
    /// The <see cref="BuildMonitorConnectionErrorEventArgs" /> class.
    /// </summary>
    public class BuildMonitorConnectionErrorEventArgs : BuildMonitorConnectionEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildMonitorConnectionErrorEventArgs" /> class.
        /// </summary>
        /// <param name="settingsId">The settings identifier.</param>
        public BuildMonitorConnectionErrorEventArgs(Guid settingsId)
            : base(settingsId)
        {
        }
    }
}