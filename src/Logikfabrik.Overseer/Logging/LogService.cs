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
                    LogDebug(logger, entry);

                    break;

                case LogEntryType.Information:
                    LogInformation(logger, entry);

                    break;

                case LogEntryType.Warning:
                    LogWarning(logger, entry);

                    break;

                case LogEntryType.Error:
                    LogError(logger, entry);

                    break;
            }
        }

        private static void LogDebug(ILogger logger, LogEntry entry)
        {
            if (!logger.IsEnabled(LogEventLevel.Debug))
            {
                return;
            }

            if (entry.Exception == null)
            {
                logger.Debug(entry.MessageTemplate, entry.Arguments);
            }
            else
            {
                logger.Debug(entry.Exception, entry.MessageTemplate, entry.Arguments);
            }
        }

        private static void LogInformation(ILogger logger, LogEntry entry)
        {
            if (!logger.IsEnabled(LogEventLevel.Information))
            {
                return;
            }

            if (entry.Exception == null)
            {
                logger.Information(entry.MessageTemplate, entry.Arguments);
            }
            else
            {
                logger.Information(entry.Exception, entry.MessageTemplate, entry.Arguments);
            }
        }

        private static void LogWarning(ILogger logger, LogEntry entry)
        {
            if (!logger.IsEnabled(LogEventLevel.Warning))
            {
                return;
            }

            if (entry.Exception == null)
            {
                logger.Warning(entry.MessageTemplate, entry.Arguments);
            }
            else
            {
                logger.Warning(entry.Exception, entry.MessageTemplate, entry.Arguments);
            }
        }

        private static void LogError(ILogger logger, LogEntry entry)
        {
            if (!logger.IsEnabled(LogEventLevel.Error))
            {
                return;
            }

            if (entry.Exception == null)
            {
                logger.Error(entry.MessageTemplate, entry.Arguments);
            }
            else
            {
                logger.Error(entry.Exception, entry.MessageTemplate, entry.Arguments);
            }
        }
    }
}