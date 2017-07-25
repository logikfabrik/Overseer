// <copyright file="ErrorHandlerConfigurator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client
{
    using System;
    using EnsureThat;
    using Overseer.Logging;

    /// <summary>
    /// The <see cref="ErrorHandlerConfigurator" /> class.
    /// </summary>
    public static class ErrorHandlerConfigurator
    {
        /// <summary>
        /// Configures error handling.
        /// </summary>
        /// <param name="logService">The log service.</param>
        public static void Configure(ILogService logService)
        {
            Ensure.That(logService).IsNotNull();

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                var exception = e.ExceptionObject as Exception;

                logService.Log(typeof(ErrorHandlerConfigurator), new LogEntry(LogEntryType.Error, null, exception));
            };
        }
    }
}
