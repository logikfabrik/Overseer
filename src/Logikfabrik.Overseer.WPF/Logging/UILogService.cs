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
    // ReSharper disable once InconsistentNaming
    public class UILogService : IUILogService
    {
        private readonly ILogService _logService;
        private readonly Type _type;

        public UILogService(ILogService logService, Type type)
        {
            Ensure.That(logService).IsNotNull();
            Ensure.That(type).IsNotNull();

            _logService = logService;
            _type = type;
        }

        public void Info(string format, params object[] args)
        {
            _logService.Log(_type, new LogEntry(LogEntryType.Information, string.Format(format, args)));
        }

        public void Warn(string format, params object[] args)
        {
            _logService.Log(_type, new LogEntry(LogEntryType.Warning, string.Format(format, args)));
        }

        public void Error(Exception exception)
        {
            _logService.Log(_type, new LogEntry(LogEntryType.Error, null, exception));
        }
    }
}
