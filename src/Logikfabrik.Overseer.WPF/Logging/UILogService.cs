// <copyright file="UILogService.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Logging
{
    using System;
    using EnsureThat;
    using JetBrains.Annotations;
    using Overseer.Logging;

    /// <summary>
    /// The <see cref="UILogService" /> class.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once InheritdocConsiderUsage
#pragma warning disable S101 // Types should be named in camel case
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
        [UsedImplicitly]

        public UILogService(ILogService logService, Type type)
        {
            Ensure.That(logService).IsNotNull();
            Ensure.That(type).IsNotNull();

            _logService = logService;
            _type = type;
        }

        /// <inheritdoc />
        public void Info(string format, params object[] args)
        {
            _logService.Log(_type, new LogEntry(LogEntryType.Information, format, args));
        }

        /// <inheritdoc />
        public void Warn(string format, params object[] args)
        {
            _logService.Log(_type, new LogEntry(LogEntryType.Warning, format, args));
        }

        /// <inheritdoc />
        public void Error(Exception exception)
        {
            _logService.Log(_type, new LogEntry(LogEntryType.Error, null, exception));
        }
    }
}
