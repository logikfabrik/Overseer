//// <copyright file="BuildMonitorTest.cs" company="Logikfabrik">
////   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
//// </copyright>

//namespace Logikfabrik.Overseer.Test
//{
//    using Moq;
//    using Xunit;

//    public class BuildMonitorTest
//    {
//        [Fact]
//        public void CanStartMonitoring()
//        {
//            var providerRepositoryMock = new Mock<IBuildProviderRepository>();

//            var buildMonitor = new BuildMonitor(providerRepositoryMock.Object);

//            buildMonitor.StartMonitoring();

//            Assert.True(buildMonitor.IsMonitoring);
//        }

//        [Fact]
//        public void CanStopMonitoring()
//        {
//            var providerRepositoryMock = new Mock<IBuildProviderRepository>();

//            var buildMonitor = new BuildMonitor(providerRepositoryMock.Object);

//            buildMonitor.StopMonitoring();

//            Assert.False(buildMonitor.IsMonitoring);
//        }
//    }
//}
