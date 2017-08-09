// <copyright file="BuildExtensionsTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Extensions
{
    using System;
    using Moq;
    using Overseer.Extensions;
    using Ploeh.AutoFixture.Xunit2;
    using Xunit;

    public class BuildExtensionsTest
    {
        [Fact]
        public void CanNotGetVersionNumber()
        {
            var buildMock = new Mock<IBuild>();

            var versionNumber = buildMock.Object.VersionNumber();

            Assert.Null(versionNumber);
        }

        [Theory]
        [AutoData]
        public void CanGetVersionNumberForBuildVersion(string version)
        {
            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Version).Returns(version);

            var versionNumber = buildMock.Object.VersionNumber();

            Assert.Equal(version, versionNumber);
        }

        [Theory]
        [AutoData]
        public void CanGetVersionNumberForBuildNumber(string number)
        {
            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Number).Returns(number);

            var versionNumber = buildMock.Object.VersionNumber();

            Assert.Equal(number, versionNumber);
        }

        [Fact]
        public void CanNotGetRunTimeForBuild()
        {
            var buildMock = new Mock<IBuild>();

            var runTime = buildMock.Object.RunTime();

            Assert.Null(runTime);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void CanGetRunTimeForInProgressBuild(int hoursRunning)
        {
            var utcNow = DateTime.UtcNow;

            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Status).Returns(BuildStatus.InProgress);
            buildMock.Setup(m => m.StartTime).Returns(utcNow.AddHours(-1 * hoursRunning));

            var runTime = buildMock.Object.RunTime(utcNow);

            // ReSharper disable once PossibleInvalidOperationException
            Assert.Equal(hoursRunning, runTime.Value.TotalHours);
        }

        [Fact]
        public void CanNotGetRunTimeForInProgressBuildWithoutStartTime()
        {
            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Status).Returns(BuildStatus.InProgress);

            var runTime = buildMock.Object.RunTime();

            Assert.Null(runTime);
        }

        [Theory]
        [InlineData(BuildStatus.Failed, 1)]
        [InlineData(BuildStatus.Succeeded, 2)]
        [InlineData(BuildStatus.Stopped, 3)]
        public void CanGetRunTimeForFinishedBuild(BuildStatus status, int hoursRunning)
        {
            var utcNow = DateTime.UtcNow;

            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Status).Returns(status);
            buildMock.Setup(m => m.StartTime).Returns(utcNow.AddHours(-1 * hoursRunning));
            buildMock.Setup(m => m.EndTime).Returns(utcNow);

            var runTime = buildMock.Object.RunTime();

            // ReSharper disable once PossibleInvalidOperationException
            Assert.Equal(hoursRunning, runTime.Value.TotalHours);
        }

        [Theory]
        [InlineData(BuildStatus.Failed)]
        [InlineData(BuildStatus.Succeeded)]
        [InlineData(BuildStatus.Stopped)]
        public void CanNotGetRunTimeForFinishedBuildWithoutStartTime(BuildStatus status)
        {
            var utcNow = DateTime.UtcNow;

            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Status).Returns(status);
            buildMock.Setup(m => m.EndTime).Returns(utcNow);

            var runTime = buildMock.Object.RunTime();

            Assert.Null(runTime);
        }

        [Theory]
        [InlineData(BuildStatus.Failed)]
        [InlineData(BuildStatus.Succeeded)]
        [InlineData(BuildStatus.Stopped)]
        public void CanNotGetRunTimeForFinishedBuildWithoutEndTime(BuildStatus status)
        {
            var utcNow = DateTime.UtcNow;

            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Status).Returns(status);
            buildMock.Setup(m => m.StartTime).Returns(utcNow.AddHours(-1));

            var runTime = buildMock.Object.RunTime();

            Assert.Null(runTime);
        }

        [Theory]
        [InlineData(BuildStatus.Failed)]
        [InlineData(BuildStatus.Succeeded)]
        [InlineData(BuildStatus.Stopped)]
        public void CanNotGetRunTimeForFinishedBuildWithoutStartAndEndTime(BuildStatus status)
        {
            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Status).Returns(status);

            var runTime = buildMock.Object.RunTime();

            Assert.Null(runTime);
        }
    }
}