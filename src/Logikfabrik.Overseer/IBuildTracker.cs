// <copyright file="IBuildTracker.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using Notification;

    /// <summary>
    /// The <see cref="IBuildTracker" /> interface.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public interface IBuildTracker : IObserver<Notification<IConnection>[]>
    {
        /// <summary>
        /// Occurs if there is an connection error.
        /// </summary>
        event EventHandler<BuildTrackerConnectionErrorEventArgs> ConnectionError;

        /// <summary>
        /// Occurs when connection progress changes.
        /// </summary>
        event EventHandler<BuildTrackerConnectionProgressEventArgs> ConnectionProgressChanged;

        /// <summary>
        /// Occurs if there is an project error.
        /// </summary>
        event EventHandler<BuildTrackerProjectErrorEventArgs> ProjectError;

        /// <summary>
        /// Occurs when project progress changes.
        /// </summary>
        event EventHandler<BuildTrackerProjectProgressEventArgs> ProjectProgressChanged;
    }
}