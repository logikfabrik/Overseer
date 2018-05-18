// <copyright file="INotification.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System;

    /// <summary>
    /// The <see cref="INotification" /> interface.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public interface INotification
    {
        /// <summary>
        /// Occurs if closing.
        /// </summary>
        event EventHandler<EventArgs> Closing;

        /// <summary>
        /// Closes this notification.
        /// </summary>
        void Close();
    }
}
