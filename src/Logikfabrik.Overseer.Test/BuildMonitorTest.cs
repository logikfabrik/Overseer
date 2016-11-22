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
    using Xunit;

    public class BuildMonitorTest
    {
        [Fact]
        public void BuildMonitor_CanStartMonitoring()
        {
            var providerRepositoryMock = new Mock<IBuildProviderRepository>();

            var buildMonitor = new BuildMonitor(providerRepositoryMock.Object);

            buildMonitor.StartMonitoring();

            Assert.True(buildMonitor.IsMonitoring);
        }

        [Fact]
        public void BuildMonitor_CanStopMonitoring()
        {
            var providerRepositoryMock = new Mock<IBuildProviderRepository>();

            var buildMonitor = new BuildMonitor(providerRepositoryMock.Object);

            buildMonitor.StopMonitoring();

            Assert.False(buildMonitor.IsMonitoring);
        }

        [Fact]
        public void BuildMonitor_OnProgressChanged()
        {
            var projectMock = new Mock<IProject>();
            var providerMock = new Mock<IBuildProvider>();
            var providerRepositoryMock = new Mock<IBuildProviderRepository>();

            providerMock.Setup(m => m.GetProjectsAsync()).Returns(Task.FromResult((IEnumerable<IProject>)new[] { projectMock.Object }));

            providerRepositoryMock.Setup(m => m.GetAll()).Returns(new[] { providerMock.Object });

            var buildMonitor = new BuildMonitor(providerRepositoryMock.Object);

            var resetEvent = new ManualResetEventSlim(false);

            buildMonitor.ProgressChanged += GetEventHandler<BuildMonitorProgressEventArgs>(resetEvent, (sender, args) => Assert.Same(providerMock.Object, args.Provider));

            buildMonitor.StartMonitoring();

            WaitForResetEvent(resetEvent);
        }

        [Fact]
        public void BuildMonitor_OnError()
        {
            var providerRepositoryMock = new Mock<IBuildProviderRepository>();

            providerRepositoryMock.Setup(m => m.GetAll()).Throws(new Exception());

            var buildMonitor = new BuildMonitor(providerRepositoryMock.Object);

            var resetEvent = new ManualResetEventSlim(false);

            buildMonitor.Error += GetEventHandler<BuildMonitorErrorEventArgs>(resetEvent, (sender, args) =>
            {
                Assert.Null(args.Provider);
                Assert.Null(args.Project);
            });

            buildMonitor.StartMonitoring();

            WaitForResetEvent(resetEvent);
        }

        [Fact]
        public void BuildMonitor_OnGetProjectsError()
        {
            var providerMock = new Mock<IBuildProvider>();
            var providerRepositoryMock = new Mock<IBuildProviderRepository>();

            providerMock.Setup(m => m.GetProjectsAsync()).Throws(new Exception());

            providerRepositoryMock.Setup(m => m.GetAll()).Returns(new[] { providerMock.Object });

            var buildMonitor = new BuildMonitor(providerRepositoryMock.Object);

            var resetEvent = new ManualResetEventSlim(false);

            buildMonitor.Error += GetEventHandler<BuildMonitorErrorEventArgs>(resetEvent, (sender, args) =>
            {
                Assert.Same(providerMock.Object, args.Provider);
                Assert.Null(args.Project);
            });

            buildMonitor.StartMonitoring();

            WaitForResetEvent(resetEvent);
        }

        [Fact]
        public void BuildMonitor_OnGetBuildsError()
        {
            var projectMock = new Mock<IProject>();
            var providerMock = new Mock<IBuildProvider>();
            var providerRepositoryMock = new Mock<IBuildProviderRepository>();

            providerMock.Setup(m => m.GetProjectsAsync()).Returns(Task.FromResult((IEnumerable<IProject>)new[] { projectMock.Object }));

            providerMock.Setup(m => m.GetBuildsAsync(It.IsAny<string>())).Throws(new Exception());

            providerRepositoryMock.Setup(m => m.GetAll()).Returns(new[] { providerMock.Object });

            var buildMonitor = new BuildMonitor(providerRepositoryMock.Object);

            var resetEvent = new ManualResetEventSlim(false);

            buildMonitor.Error += GetEventHandler<BuildMonitorErrorEventArgs>(resetEvent, (sender, args) =>
            {
                Assert.Same(providerMock.Object, args.Provider);
                Assert.Same(projectMock.Object, args.Project);
            });

            buildMonitor.StartMonitoring();

            WaitForResetEvent(resetEvent);
        }

        private static EventHandler<T> GetEventHandler<T>(ManualResetEventSlim resetEvent, Action<object, T> handler)
            where T : EventArgs
        {
            return (sender, args) =>
            {
                handler(sender, args);

                resetEvent.Set();
            };
        }

        private static void WaitForResetEvent(ManualResetEventSlim resetEvent)
        {
            resetEvent.Wait(TimeSpan.FromSeconds(5));

            if (!resetEvent.IsSet)
            {
            }
        }
    }
}
