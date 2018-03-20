// <copyright file="LogEntryType.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Logging
{
    /// <summary>
    /// The <see cref="LogEntryType" /> enumerable.
    /// </summary>
    public enum LogEntryType
    {
        /// <summary>
        /// Entry type for a debug entry.
        /// </summary>
        Debug = 0,

        /// <summary>
        /// Entry type for a information entry.
        /// </summary>
        Information = 1,

        /// <summary>
        /// Entry type for a warning entry.
        /// </summary>
        Warning = 2,

        /// <summary>
        /// Entry type for an error entry.
        /// </summary>
        Error = 3
    }
}