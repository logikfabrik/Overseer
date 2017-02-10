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
        /// Type for debug entry.
        /// </summary>
        Debug = 0,

        /// <summary>
        /// Type for information entry.
        /// </summary>
        Information = 1,

        /// <summary>
        /// Type for warning entry.
        /// </summary>
        Warning = 2,

        /// <summary>
        /// Type for error entry.
        /// </summary>
        Error = 3
    }
}