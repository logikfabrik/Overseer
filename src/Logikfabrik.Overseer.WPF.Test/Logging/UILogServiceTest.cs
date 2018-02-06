// <copyright file="UILogServiceTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Test.Logging
{
    using System;
    using Moq;
    using Moq.AutoMock;
    using Overseer.Logging;
    using WPF.Logging;
    using Xunit;

#pragma warning disable S101 // Types should be named in camel case

    // ReSharper disable once InconsistentNaming
    public class UILogServiceTest
#pragma warning restore S101 // Types should be named in camel case
    {
        [Fact]
        public void CanLogInfo()
        {
            CanLog(uiLogService => uiLogService.Info(null, null), LogEntryType.Information);
        }

        [Fact]
        public void CanLogWarn()
        {
            CanLog(uiLogService => uiLogService.Warn(null, null), LogEntryType.Warning);
        }

        [Fact]
        public void CanLogError()
        {
            CanLog(uiLogService => uiLogService.Error(null), LogEntryType.Error);
        }

        private static void CanLog(Action<IUILogService> action, LogEntryType type)
        {
            var mocker = new AutoMocker();

            var uiLogService = mocker.CreateInstance<UILogService>();

            var logServiceMock = mocker.GetMock<ILogService>();

            action(uiLogService);

            logServiceMock.Verify(m => m.Log(It.IsAny<Type>(), It.Is<LogEntry>(entry => entry.Type == type)), Times.Once);
        }
    }
}
