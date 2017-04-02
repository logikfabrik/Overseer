// <copyright file="LogService.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Logging
{
    using System;
    using EnsureThat;
    using log4net;

    /// <summary>
    /// The <see cref="LogService" /> class.
    /// </summary>
    public class LogService : ILogService
    {
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

            var logger = GetLogger(type);

            if (logger == null)
            {
                return;
            }

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (entry.Type)
            {
                case LogEntryType.Debug:
                    if (!logger.IsDebugEnabled)
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
                    if (!logger.IsInfoEnabled)
                    {
                        break;
                    }

                    if (entry.Exception == null)
                    {
                        logger.Info(entry.Message);
                    }
                    else
                    {
                        logger.Info(entry.Message, entry.Exception);
                    }

                    break;

                case LogEntryType.Warning:
                    if (!logger.IsWarnEnabled)
                    {
                        break;
                    }

                    if (entry.Exception == null)
                    {
                        logger.Warn(entry.Message);
                    }
                    else
                    {
                        logger.Warn(entry.Message, entry.Exception);
                    }

                    break;

                case LogEntryType.Error:
                    if (!logger.IsErrorEnabled)
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

        private static ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(type);
        }
    }
}