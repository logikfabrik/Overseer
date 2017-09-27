// <copyright file="UILogServiceTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Test.Logging
{
    using Moq;
    using Overseer.Logging;
    using WPF.Logging;
    using Xunit;

    public class UILogServiceTest
    {
        [Fact]
        public void CanLogInfo()
        {
            var logServiceMock = new Mock<ILogService>();

            var uiLogService = new UILogService(logServiceMock.Object, typeof(object));

            uiLogService.Info(null, null);

            logServiceMock.Verify(m => m.Log(typeof(object), It.Is<LogEntry>(entry => entry.Type == LogEntryType.Information)), Times.Once);
        }

        [Fact]
        public void CanLogWarn()
        {
            var logServiceMock = new Mock<ILogService>();

            var uiLogService = new UILogService(logServiceMock.Object, typeof(object));

            uiLogService.Warn(null, null);

            logServiceMock.Verify(m => m.Log(typeof(object), It.Is<LogEntry>(entry => entry.Type == LogEntryType.Warning)), Times.Once);
        }

        [Fact]
        public void CanLogError()
        {
            var logServiceMock = new Mock<ILogService>();

            var uiLogService = new UILogService(logServiceMock.Object, typeof(object));

            uiLogService.Error(null);

            logServiceMock.Verify(m => m.Log(typeof(object), It.Is<LogEntry>(entry => entry.Type == LogEntryType.Error)), Times.Once);
        }
    }
}
