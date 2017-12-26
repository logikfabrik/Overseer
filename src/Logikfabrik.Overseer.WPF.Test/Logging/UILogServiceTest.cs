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
            var mocker = new AutoMocker();

            var uiLogService = mocker.CreateInstance<UILogService>();

            var logServiceMock = mocker.GetMock<ILogService>();

            uiLogService.Info(null, null);

            logServiceMock.Verify(m => m.Log(It.IsAny<Type>(), It.Is<LogEntry>(entry => entry.Type == LogEntryType.Information)), Times.Once);
        }

        [Fact]
        public void CanLogWarn()
        {
            var mocker = new AutoMocker();

            var uiLogService = mocker.CreateInstance<UILogService>();

            var logServiceMock = mocker.GetMock<ILogService>();

            uiLogService.Warn(null, null);

            logServiceMock.Verify(m => m.Log(It.IsAny<Type>(), It.Is<LogEntry>(entry => entry.Type == LogEntryType.Warning)), Times.Once);
        }

        [Fact]
        public void CanLogError()
        {
            var mocker = new AutoMocker();

            var uiLogService = mocker.CreateInstance<UILogService>();

            var logServiceMock = mocker.GetMock<ILogService>();

            uiLogService.Error(null);

            logServiceMock.Verify(m => m.Log(It.IsAny<Type>(), It.Is<LogEntry>(entry => entry.Type == LogEntryType.Error)), Times.Once);
        }
    }
}
