// <copyright file="LogService.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Logging
{
    using System;
    using EnsureThat;
    using Serilog;
    using Serilog.Events;

    /// <summary>
    /// The <see cref="LogService" /> class.
    /// </summary>
    public class LogService : ILogService
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogService" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public LogService(ILogger logger)
        {
            Ensure.That(logger).IsNotNull();

            _logger = logger;
        }

        /// <summary>
        /// Logs the specified entry.
        /// </summary>
        /// <typeparam name="T">The logging type.</typeparam>
        /// <param name="entry">The entry.</param>
        public void Log<T>(LogEntry entry)
        {
            Ensure.That(entry).IsNotNull();

            Log(typeof(T), entry);
        }

        /// <summary>
        /// Logs the specified entry.
        /// </summary>
        /// <param name="type">The logging type.</param>
        /// <param name="entry">The entry.</param>
        public void Log(Type type, LogEntry entry)
        {
            Ensure.That(type).IsNotNull();
            Ensure.That(entry).IsNotNull();

            var logger = _logger.ForContext(type);

            if (logger == null)
            {
                return;
            }

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (entry.Type)
            {
                case LogEntryType.Debug:
                    if (!logger.IsEnabled(LogEventLevel.Debug))
                    {
                        break;
                    }

                    if (entry.Exception == null)
                    {
                        logger.Debug(entry.Message);
                    }
                    else
                    {
                        logger.Debug(entry.Message, entry.Exception);
                    }

                    break;

                case LogEntryType.Information:
                    if (!logger.IsEnabled(LogEventLevel.Information))
                    {
                        break;
                    }

                    if (entry.Exception == null)
                    {
                        logger.Information(entry.Message);
                    }
                    else
                    {
                        logger.Information(entry.Message, entry.Exception);
                    }

                    break;

                case LogEntryType.Warning:
                    if (!logger.IsEnabled(LogEventLevel.Warning))
                    {
                        break;
                    }

                    if (entry.Exception == null)
                    {
                        logger.Warning(entry.Message);
                    }
                    else
                    {
                        logger.Warning(entry.Message, entry.Exception);
                    }

                    break;

                case LogEntryType.Error:
                    if (!logger.IsEnabled(LogEventLevel.Error))
                    {
                        break;
                    }

                    if (entry.Exception == null)
                    {
                        logger.Error(entry.Message);
                    }
                    else
                    {
                        logger.Error(entry.Message, entry.Exception);
                    }

                    break;
            }
        }
    }
}