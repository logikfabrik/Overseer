// <copyright file="ErrorLogHandlerConfigurator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client
{
    using System;
    using EnsureThat;
    using Overseer.Logging;

    /// <summary>
    /// The <see cref="ErrorLogHandlerConfigurator" /> class.
    /// </summary>
    public static class ErrorLogHandlerConfigurator
    {
        /// <summary>
        /// Configures error handling.
        /// </summary>
        /// <param name="appDomain">The application domain.</param>
        /// <param name="logService">The log service.</param>
        public static void Configure(AppDomain appDomain, ILogService logService)
        {
            Ensure.That(appDomain).IsNotNull();
            Ensure.That(logService).IsNotNull();

            appDomain.UnhandledException += (sender, e) =>
            {
                var exception = e.ExceptionObject as Exception;

                logService.Log(typeof(ErrorLogHandlerConfigurator), new LogEntry(LogEntryType.Error, null, exception));
            };
        }
    }
}
