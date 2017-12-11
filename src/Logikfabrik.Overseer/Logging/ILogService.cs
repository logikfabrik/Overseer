// <copyright file="ILogService.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Logging
{
    using System;

    /// <summary>
    /// The <see cref="ILogService" /> interface.
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Logs the specified entry.
        /// </summary>
        /// <param name="type">The logging type.</param>
        /// <param name="entry">The entry.</param>
        void Log(Type type, LogEntry entry);
    }
}