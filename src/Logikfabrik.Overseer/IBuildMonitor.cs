// <copyright file="IBuildMonitor.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;

    /// <summary>
    /// The <see cref="IBuildMonitor" /> interface.
    /// </summary>
    public interface IBuildMonitor
    {
        /// <summary>
        /// Occurs when progress changes.
        /// </summary>
        event EventHandler<BuildMonitorProgressEventArgs> ProgressChanged;

        /// <summary>
        /// Gets a value indicating whether this instance is monitoring.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is monitoring; otherwise, <c>false</c>.
        /// </value>
        bool IsMonitoring { get; }

        /// <summary>
        /// Starts the monitoring.
        /// </summary>
        void StartMonitoring();

        /// <summary>
        /// Stops the monitoring.
        /// </summary>
        void StopMonitoring();
    }
}
