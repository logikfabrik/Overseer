// <copyright file="LogServiceTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Logging
{
    using System;
    using Moq;
    using Overseer.Logging;
    using Serilog;
    using Serilog.Events;
    using Xunit;

    public class LogServiceTest
    {
        [Theory]
        [InlineData(false, 0)]
        [InlineData(true, 1)]
        public void CanLogDebugWithoutException(bool isEnabled, int times)
        {
            var loggerMock = new Mock<ILogger>();

            loggerMock.Setup(m => m.ForContext(typeof(object))).Returns(loggerMock.Object);
            loggerMock.Setup(m => m.IsEnabled(LogEventLevel.Debug)).Returns(isEnabled);

            var logService = new LogService(loggerMock.Object);

            var entry = new LogEntry(LogEntryType.Debug, null, null, null);

            logService.Log(typeof(object), entry);

            loggerMock.Verify(m => m.Debug(entry.MessageTemplate, entry.Arguments), Times.Exactly(times));
        }

        [Theory]
        [InlineData(false, 0)]
        [InlineData(true, 1)]
        public void CanLogDebugWithException(bool isEnabled, int times)
        {
            var loggerMock = new Mock<ILogger>();

            loggerMock.Setup(m => m.ForContext(typeof(object))).Returns(loggerMock.Object);
            loggerMock.Setup(m => m.IsEnabled(LogEventLevel.Debug)).Returns(isEnabled);

            var logService = new LogService(loggerMock.Object);

            var entry = new LogEntry(LogEntryType.Debug, null, new Exception(), null);

            logService.Log(typeof(object), entry);

            loggerMock.Verify(m => m.Debug(entry.Exception, entry.MessageTemplate, entry.Arguments), Times.Exactly(times));
        }

        [Theory]
        [InlineData(false, 0)]
        [InlineData(true, 1)]
        public void CanLogInformationWithoutException(bool isEnabled, int times)
        {
            var loggerMock = new Mock<ILogger>();

            loggerMock.Setup(m => m.ForContext(typeof(object))).Returns(loggerMock.Object);
            loggerMock.Setup(m => m.IsEnabled(LogEventLevel.Information)).Returns(isEnabled);

            var logService = new LogService(loggerMock.Object);

            var entry = new LogEntry(LogEntryType.Information, null, null, null);

            logService.Log(typeof(object), entry);

            loggerMock.Verify(m => m.Information(entry.MessageTemplate, entry.Arguments), Times.Exactly(times));
        }

        [Theory]
        [InlineData(false, 0)]
        [InlineData(true, 1)]
        public void CanLogInformationWithException(bool isEnabled, int times)
        {
            var loggerMock = new Mock<ILogger>();

            loggerMock.Setup(m => m.ForContext(typeof(object))).Returns(loggerMock.Object);
            loggerMock.Setup(m => m.IsEnabled(LogEventLevel.Information)).Returns(isEnabled);

            var logService = new LogService(loggerMock.Object);

            var entry = new LogEntry(LogEntryType.Information, null, new Exception(), null);

            logService.Log(typeof(object), entry);

            loggerMock.Verify(m => m.Information(entry.Exception, entry.MessageTemplate, entry.Arguments), Times.Exactly(times));
        }

        [Theory]
        [InlineData(false, 0)]
        [InlineData(true, 1)]
        public void CanLogWarningWithoutException(bool isEnabled, int times)
        {
            var loggerMock = new Mock<ILogger>();

            loggerMock.Setup(m => m.ForContext(typeof(object))).Returns(loggerMock.Object);
            loggerMock.Setup(m => m.IsEnabled(LogEventLevel.Warning)).Returns(isEnabled);

            var logService = new LogService(loggerMock.Object);

            var entry = new LogEntry(LogEntryType.Warning, null, null, null);

            logService.Log(typeof(object), entry);

            loggerMock.Verify(m => m.Warning(entry.MessageTemplate, entry.Arguments), Times.Exactly(times));
        }

        [Theory]
        [InlineData(false, 0)]
        [InlineData(true, 1)]
        public void CanLogWarningWithException(bool isEnabled, int times)
        {
            var loggerMock = new Mock<ILogger>();

            loggerMock.Setup(m => m.ForContext(typeof(object))).Returns(loggerMock.Object);
            loggerMock.Setup(m => m.IsEnabled(LogEventLevel.Warning)).Returns(isEnabled);

            var logService = new LogService(loggerMock.Object);

            var entry = new LogEntry(LogEntryType.Warning, null, new Exception(), null);

            logService.Log(typeof(object), entry);

            loggerMock.Verify(m => m.Warning(entry.Exception, entry.MessageTemplate, entry.Arguments), Times.Exactly(times));
        }

        [Theory]
        [InlineData(false, 0)]
        [InlineData(true, 1)]
        public void CanLogErrorWithoutException(bool isEnabled, int times)
        {
            var loggerMock = new Mock<ILogger>();

            loggerMock.Setup(m => m.ForContext(typeof(object))).Returns(loggerMock.Object);
            loggerMock.Setup(m => m.IsEnabled(LogEventLevel.Error)).Returns(isEnabled);

            var logService = new LogService(loggerMock.Object);

            var entry = new LogEntry(LogEntryType.Error, null, null, null);

            logService.Log(typeof(object), entry);

            loggerMock.Verify(m => m.Error(entry.MessageTemplate, entry.Arguments), Times.Exactly(times));
        }

        [Theory]
        [InlineData(false, 0)]
        [InlineData(true, 1)]
        public void CanLogErrorWithException(bool isEnabled, int times)
        {
            var loggerMock = new Mock<ILogger>();

            loggerMock.Setup(m => m.ForContext(typeof(object))).Returns(loggerMock.Object);
            loggerMock.Setup(m => m.IsEnabled(LogEventLevel.Error)).Returns(isEnabled);

            var logService = new LogService(loggerMock.Object);

            var entry = new LogEntry(LogEntryType.Error, null, new Exception(), null);

            logService.Log(typeof(object), entry);

            loggerMock.Verify(m => m.Error(entry.Exception, entry.MessageTemplate, entry.Arguments), Times.Exactly(times));
        }
    }
}
