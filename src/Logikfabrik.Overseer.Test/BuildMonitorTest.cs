// <copyright file="BuildMonitorTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Logging;
    using Moq;
    using Overseer.Settings;
    using Xunit;

    public class BuildMonitorTest
    {
        [Fact]
        public async Task CanGetProjectsAndBuildsAsync()
        {
            var projectMock = new Mock<IProject>();

            var buildMock = new Mock<IBuild>();

            var settingsMock = new Mock<ConnectionSettings>();

            var connectionMock = new Mock<IConnection>();

            connectionMock.SetupGet(m => m.Settings).Returns(settingsMock.Object);

            connectionMock.Setup(m => m.GetProjectsAsync(It.IsAny<CancellationToken>())).Returns((CancellationToken token) => Task.FromResult(new[] { projectMock.Object }.AsEnumerable())).Verifiable();
            connectionMock.Setup(m => m.GetBuildsAsync(It.IsAny<IProject>(), It.IsAny<CancellationToken>())).Returns((IProject project, CancellationToken token) => Task.FromResult(new[] { buildMock.Object }.AsEnumerable())).Verifiable();

            var poolMock = new Mock<IConnectionPool>();

            var logMock = new Mock<ILogService>();

            var monitor = new BuildMonitor(poolMock.Object, logMock.Object);

            await monitor.GetProjectsAndBuildsAsync(new[] { connectionMock.Object }, 0, CancellationToken.None);

            connectionMock.Verify(m => m.GetProjectsAsync(It.IsAny<CancellationToken>()), Times.Once);
            connectionMock.Verify(m => m.GetBuildsAsync(It.IsAny<IProject>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}