// <copyright file="BuildMonitorTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Moq;
    using Moq.AutoMock;
    using Overseer.Logging;
    using Overseer.Settings;
    using Xunit;

    public class BuildMonitorTest
    {
        [Fact]
        public async void WillLogOnExceptionWhenGettingProjects()
        {
            var mocker = new AutoMocker();

            var buildMonitor = mocker.CreateInstance<BuildMonitor>();

            var connectionMock = new Mock<IConnection>();

            var settingsMock = new Mock<ConnectionSettings>();

            connectionMock.Setup(m => m.Settings).Returns(settingsMock.Object);
            connectionMock.Setup(m => m.GetProjectsAsync(It.IsAny<CancellationToken>())).Throws<Exception>();

            await buildMonitor.GetProjectsAsync(connectionMock.Object, CancellationToken.None);

            var logServiceMock = mocker.GetMock<ILogService>();

            logServiceMock.Verify(m => m.Log(typeof(BuildMonitor), It.Is<LogEntry>(entry => entry.Type == LogEntryType.Error)), Times.Once);
        }

        [Fact]
        public async void CanRaiseConnectionErrorWhenGettingProjects()
        {
            var mocker = new AutoMocker();

            var buildMonitor = mocker.CreateInstance<BuildMonitor>();

            var connectionMock = new Mock<IConnection>();

            var settingsMock = new Mock<ConnectionSettings>();

            connectionMock.Setup(m => m.Settings).Returns(settingsMock.Object);
            connectionMock.Setup(m => m.GetProjectsAsync(It.IsAny<CancellationToken>())).Throws<Exception>();

            var evt = await Assert.RaisesAsync<BuildMonitorConnectionErrorEventArgs>(
                handler => buildMonitor.ConnectionError += handler,
                handler => buildMonitor.ConnectionError -= handler,
                () => buildMonitor.GetProjectsAsync(connectionMock.Object, CancellationToken.None));

            Assert.NotNull(evt);
        }

        [Fact]
        public async void CanRaiseConnectionProgressChangedWhenGettingProjects()
        {
            var mocker = new AutoMocker();

            var buildMonitor = mocker.CreateInstance<BuildMonitor>();

            var connectionMock = new Mock<IConnection>();

            var settingsMock = new Mock<ConnectionSettings>();

            connectionMock.Setup(m => m.Settings).Returns(settingsMock.Object);
            connectionMock.Setup(m => m.GetProjectsAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult<IEnumerable<IProject>>(new IProject[] { }));

            var evt = await Assert.RaisesAsync<BuildMonitorConnectionProgressEventArgs>(
                handler => buildMonitor.ConnectionProgressChanged += handler,
                handler => buildMonitor.ConnectionProgressChanged -= handler,
                () => buildMonitor.GetProjectsAsync(connectionMock.Object, CancellationToken.None));

            Assert.NotNull(evt);
        }

        [Fact]
        public async void WillLogOnExceptionWhenGettingBuilds()
        {
            var mocker = new AutoMocker();

            var buildMonitor = mocker.CreateInstance<BuildMonitor>();

            var connectionMock = new Mock<IConnection>();

            var settingsMock = new Mock<ConnectionSettings>();

            connectionMock.Setup(m => m.Settings).Returns(settingsMock.Object);
            connectionMock.Setup(m => m.GetBuildsAsync(It.IsAny<IProject>(), It.IsAny<CancellationToken>())).Throws<Exception>();

            var projectMock = new Mock<IProject>();

            await buildMonitor.GetBuildsAsync(connectionMock.Object, projectMock.Object, CancellationToken.None);

            var logServiceMock = mocker.GetMock<ILogService>();

            logServiceMock.Verify(m => m.Log(typeof(BuildMonitor), It.Is<LogEntry>(entry => entry.Type == LogEntryType.Error)), Times.Once);
        }

        [Fact]
        public async void CanRaiseProjectErrorWhenGettingBuilds()
        {
            var mocker = new AutoMocker();

            var buildMonitor = mocker.CreateInstance<BuildMonitor>();

            var connectionMock = new Mock<IConnection>();

            var settingsMock = new Mock<ConnectionSettings>();

            connectionMock.Setup(m => m.Settings).Returns(settingsMock.Object);
            connectionMock.Setup(m => m.GetBuildsAsync(It.IsAny<IProject>(), It.IsAny<CancellationToken>())).Throws<Exception>();

            var projectMock = new Mock<IProject>();

            var evt = await Assert.RaisesAsync<BuildMonitorProjectErrorEventArgs>(
                handler => buildMonitor.ProjectError += handler,
                handler => buildMonitor.ProjectError -= handler,
                () => buildMonitor.GetBuildsAsync(connectionMock.Object, projectMock.Object, CancellationToken.None));

            Assert.NotNull(evt);
        }

        [Fact]
        public async void CanRaiseProjectProgressChangedWhenGettingBuilds()
        {
            var mocker = new AutoMocker();

            var buildMonitor = mocker.CreateInstance<BuildMonitor>();

            var connectionMock = new Mock<IConnection>();

            var settingsMock = new Mock<ConnectionSettings>();

            connectionMock.Setup(m => m.Settings).Returns(settingsMock.Object);
            connectionMock.Setup(m => m.GetBuildsAsync(It.IsAny<IProject>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<IEnumerable<IBuild>>(new IBuild[] { }));

            var projectMock = new Mock<IProject>();

            var evt = await Assert.RaisesAsync<BuildMonitorProjectProgressEventArgs>(
                handler => buildMonitor.ProjectProgressChanged += handler,
                handler => buildMonitor.ProjectProgressChanged -= handler,
                () => buildMonitor.GetBuildsAsync(connectionMock.Object, projectMock.Object, CancellationToken.None));

            Assert.NotNull(evt);
        }

        [Fact]
        public void CanUpdateConnections()
        {
            // TODO: This unit test.
        }
    }
}
