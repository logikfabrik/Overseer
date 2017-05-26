// <copyright file="UILogService.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Logging
{
    using System;
    using EnsureThat;
    using Overseer.Logging;

    /// <summary>
    /// The <see cref="UILogService" /> class.
    /// </summary>
#pragma warning disable S101 // Types should be named in camel case

    // ReSharper disable once InconsistentNaming
    public class UILogService : IUILogService
#pragma warning restore S101 // Types should be named in camel case
    {
        private readonly ILogService _logService;
        private readonly Type _type;

        /// <summary>
        /// Initializes a new instance of the <see cref="UILogService" /> class.
        /// </summary>
        /// <param name="logService">The log service.</param>
        /// <param name="type">The type.</param>
        public UILogService(ILogService logService, Type type)
        {
            Ensure.That(logService).IsNotNull();
            Ensure.That(type).IsNotNull();

            _logService = logService;
            _type = type;
        }

        /// <summary>
        /// Logs the specified information entry.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public void Info(string format, params object[] args)
        {
            _logService.Log(_type, new LogEntry(LogEntryType.Information, string.Format(format, args)));
        }

        /// <summary>
        /// Logs the specified warning entry.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public void Warn(string format, params object[] args)
        {
            _logService.Log(_type, new LogEntry(LogEntryType.Warning, string.Format(format, args)));
        }

        /// <summary>
        /// Logs the specified error entry.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void Error(Exception exception)
        {
            _logService.Log(_type, new LogEntry(LogEntryType.Error, null, exception));
        }
    }
}
