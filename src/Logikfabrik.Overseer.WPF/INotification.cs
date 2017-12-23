// <copyright file="INotification.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System;
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="INotification" /> interface.
    /// </summary>
    public interface INotification : IViewAware
    {
        /// <summary>
        /// Occurs if closing.
        /// </summary>
        event EventHandler<EventArgs> Closing;
    }
}
