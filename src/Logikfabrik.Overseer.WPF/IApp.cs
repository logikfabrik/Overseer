// <copyright file="IApp.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System.Windows.Threading;

    /// <summary>
    /// The <see cref="IApp" /> interface.
    /// </summary>
    public interface IApp
    {
        /// <summary>
        /// Occurs when an exception is thrown by an application but not handled.
        /// </summary>
        event DispatcherUnhandledExceptionEventHandler DispatcherUnhandledException;

        /// <summary>
        /// Gets the dispatcher.
        /// </summary>
        /// <value>
        /// The dispatcher.
        /// </value>
        Dispatcher Dispatcher { get; }

        /// <summary>
        /// Shuts this instance down.
        /// </summary>
        void Shutdown();
    }
}
