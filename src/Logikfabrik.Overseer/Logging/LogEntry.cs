// <copyright file="LogEntry.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Logging
{
    using System;

    /// <summary>
    /// The <see cref="LogEntry" /> class.
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntry" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="args">The arguments.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public LogEntry(LogEntryType type, string messageTemplate, params object[] args)
            : this(type, messageTemplate, null, args)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntry" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="args">The arguments.</param>
        public LogEntry(LogEntryType type, string messageTemplate, Exception exception, params object[] args)
        {
            Type = type;
            MessageTemplate = messageTemplate;
            Exception = exception;
            Arguments = args;
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public LogEntryType Type { get; }

        /// <summary>
        /// Gets the message template.
        /// </summary>
        /// <value>
        /// The message template.
        /// </value>
        public string MessageTemplate { get; }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        /// <value>
        /// The arguments.
        /// </value>
        public object[] Arguments { get; }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public Exception Exception { get; }
    }
}