// <copyright file="BuildTrackerTest.cs" company="Logikfabrik">
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
    using Shouldly;
    using Xunit;

    public class BuildTrackerTest
    {
        [Fact]
        public async Task WillLogOnExceptionWhenGettingProjects()
        {
            var mocker = new AutoMocker();

            var buildTracker = mocker.CreateInstance<BuildTracker>();

            var connectionMock = new Mock<IConnection>();

            var settingsMock = new Mock<ConnectionSettings>();

            connectionMock.Setup(m => m.Settings).Returns(settingsMock.Object);
            connectionMock.Setup(m => m.GetProjectsAsync(It.IsAny<CancellationToken>())).Throws<Exception>();

            await buildTracker.GetProjectsAsync(connectionMock.Object, CancellationToken.None);

            var logServiceMock = mocker.GetMock<ILogService>();

            logServiceMock.Verify(m => m.Log(typeof(BuildTracker), It.Is<LogEntry>(entry => entry.Type == LogEntryType.Error)), Times.Once);
        }

        [Fact]
        public async Task CanRaiseConnectionErrorWhenGettingProjects()
        {
            var mocker = new AutoMocker();

            var buildTracker = mocker.CreateInstance<BuildTracker>();

            var connectionMock = new Mock<IConnection>();

            var settingsMock = new Mock<ConnectionSettings>();

            connectionMock.Setup(m => m.Settings).Returns(settingsMock.Object);
            connectionMock.Setup(m => m.GetProjectsAsync(It.IsAny<CancellationToken>())).Throws<Exception>();

            var evt = await Assert.RaisesAsync<BuildTrackerConnectionErrorEventArgs>(
                handler => buildTracker.ConnectionError += handler,
                handler => buildTracker.ConnectionError -= handler,
                () => buildTracker.GetProjectsAsync(connectionMock.Object, CancellationToken.None));

            evt.ShouldNotBeNull();
        }

        [Fact]
        public async Task CanRaiseConnectionProgressChangedWhenGettingProjects()
        {
            var mocker = new AutoMocker();

            var buildTracker = mocker.CreateInstance<BuildTracker>();

            var connectionMock = new Mock<IConnection>();

            var settingsMock = new Mock<ConnectionSettings>();

            connectionMock.Setup(m => m.Settings).Returns(settingsMock.Object);
            connectionMock.Setup(m => m.GetProjectsAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult<IEnumerable<IProject>>(new IProject[] { }));

            var evt = await Assert.RaisesAsync<BuildTrackerConnectionProgressEventArgs>(
                handler => buildTracker.ConnectionProgressChanged += handler,
                handler => buildTracker.ConnectionProgressChanged -= handler,
                () => buildTracker.GetProjectsAsync(connectionMock.Object, CancellationToken.None));

            evt.ShouldNotBeNull();
        }

        [Fact]
        public async Task WillLogOnExceptionWhenGettingBuilds()
        {
            var mocker = new AutoMocker();

            var buildTracker = mocker.CreateInstance<BuildTracker>();

            var connectionMock = new Mock<IConnection>();

            var settingsMock = new Mock<ConnectionSettings>();

            connectionMock.Setup(m => m.Settings).Returns(settingsMock.Object);
            connectionMock.Setup(m => m.GetBuildsAsync(It.IsAny<IProject>(), It.IsAny<CancellationToken>())).Throws<Exception>();

            var projectMock = new Mock<IProject>();

            await buildTracker.GetBuildsAsync(connectionMock.Object, projectMock.Object, CancellationToken.None);

            var logServiceMock = mocker.GetMock<ILogService>();

            logServiceMock.Verify(m => m.Log(typeof(BuildTracker), It.Is<LogEntry>(entry => entry.Type == LogEntryType.Error)), Times.Once);
        }

        [Fact]
        public async Task CanRaiseProjectErrorWhenGettingBuilds()
        {
            var mocker = new AutoMocker();

            var buildTracker = mocker.CreateInstance<BuildTracker>();

            var connectionMock = new Mock<IConnection>();

            var settingsMock = new Mock<ConnectionSettings>();

            connectionMock.Setup(m => m.Settings).Returns(settingsMock.Object);
            connectionMock.Setup(m => m.GetBuildsAsync(It.IsAny<IProject>(), It.IsAny<CancellationToken>())).Throws<Exception>();

            var projectMock = new Mock<IProject>();

            var evt = await Assert.RaisesAsync<BuildTrackerProjectErrorEventArgs>(
                handler => buildTracker.ProjectError += handler,
                handler => buildTracker.ProjectError -= handler,
                () => buildTracker.GetBuildsAsync(connectionMock.Object, projectMock.Object, CancellationToken.None));

            evt.ShouldNotBeNull();
        }

        [Fact]
        public async Task CanRaiseProjectProgressChangedWhenGettingBuilds()
        {
            var mocker = new AutoMocker();

            var buildTracker = mocker.CreateInstance<BuildTracker>();

            var connectionMock = new Mock<IConnection>();

            var settingsMock = new Mock<ConnectionSettings>();

            connectionMock.Setup(m => m.Settings).Returns(settingsMock.Object);
            connectionMock.Setup(m => m.GetBuildsAsync(It.IsAny<IProject>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<IEnumerable<IBuild>>(new IBuild[] { }));

            var projectMock = new Mock<IProject>();

            var evt = await Assert.RaisesAsync<BuildTrackerProjectProgressEventArgs>(
                handler => buildTracker.ProjectProgressChanged += handler,
                handler => buildTracker.ProjectProgressChanged -= handler,
                () => buildTracker.GetBuildsAsync(connectionMock.Object, projectMock.Object, CancellationToken.None));

            evt.ShouldNotBeNull();
        }

        [Fact]
        public void CanUpdateConnections()
        {
            // TODO: This unit test.
        }
    }
}
