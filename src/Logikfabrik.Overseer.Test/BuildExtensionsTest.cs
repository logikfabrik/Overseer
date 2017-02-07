// <copyright file="BuildExtensionsTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test
{
    using System;
    using Extensions;
    using Moq;
    using Ploeh.AutoFixture.Xunit2;
    using Xunit;

    public class BuildExtensionsTest
    {
        [Fact]
        public void CanNotGetVersionNumber()
        {
            var buildMock = new Mock<IBuild>();

            var versionNumber = buildMock.Object.GetVersionNumber();

            Assert.Null(versionNumber);
        }

        [Theory]
        [AutoData]
        public void CanGetVersionNumberForBuildVersion(string version)
        {
            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Version).Returns(version);

            var versionNumber = buildMock.Object.GetVersionNumber();

            Assert.Equal(version, versionNumber);
        }

        [Theory]
        [AutoData]
        public void CanGetVersionNumberForBuildNumber(string number)
        {
            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Number).Returns(number);

            var versionNumber = buildMock.Object.GetVersionNumber();

            Assert.Equal(number, versionNumber);
        }

        [Fact]
        public void CanNotGetRunTimeForBuild()
        {
            var buildMock = new Mock<IBuild>();

            var runTime = buildMock.Object.GetRunTime();

            Assert.Null(runTime);
        }

        [Fact]
        public void CanGetRunTimeForInProgressBuild()
        {
            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Status).Returns(BuildStatus.InProgress);
            buildMock.Setup(m => m.StartTime).Returns(DateTime.UtcNow.AddHours(-1));

            var runTime = buildMock.Object.GetRunTime();

            // ReSharper disable once PossibleInvalidOperationException
            Assert.Equal(1, runTime.Value.TotalHours);
        }

        [Fact]
        public void CanNotGetRunTimeForInProgressBuildWithoutStartTime()
        {
            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Status).Returns(BuildStatus.InProgress);

            var runTime = buildMock.Object.GetRunTime();

            Assert.Null(runTime);
        }

        [Theory]
        [InlineData(BuildStatus.Failed)]
        [InlineData(BuildStatus.Succeeded)]
        [InlineData(BuildStatus.Stopped)]
        public void CanGetRunTimeForFinishedBuild(BuildStatus status)
        {
            var utcNow = DateTime.UtcNow;

            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Status).Returns(status);
            buildMock.Setup(m => m.StartTime).Returns(utcNow.AddHours(-1));
            buildMock.Setup(m => m.EndTime).Returns(utcNow);

            var runTime = buildMock.Object.GetRunTime();

            // ReSharper disable once PossibleInvalidOperationException
            Assert.Equal(1, runTime.Value.TotalHours);
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

            var runTime = buildMock.Object.GetRunTime();

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
            buildMock.Setup(m => m.StartTime).Returns(utcNow.AddHours(-2));

            var runTime = buildMock.Object.GetRunTime();

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

            var runTime = buildMock.Object.GetRunTime();

            Assert.Null(runTime);
        }
    }
}