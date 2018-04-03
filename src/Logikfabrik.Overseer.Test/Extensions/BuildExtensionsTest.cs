// <copyright file="BuildExtensionsTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Extensions
{
    using System;
    using AutoFixture.Xunit2;
    using Moq;
    using Overseer.Extensions;
    using Shouldly;
    using Xunit;

    public class BuildExtensionsTest
    {
        [Theory]
        [InlineData(null, null, null)]
        [InlineData("My Project", null, "My Project")]
        [InlineData("My Project", "1.0.0", "My Project 1.0.0")]
        [InlineData(null, "1.0.0", "1.0.0")]
        public void CanGetName(string projectName, string version, string expected)
        {
            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Version).Returns(version);

            var name = buildMock.Object.Name(projectName);

            name.ShouldBe(expected);
        }

        [Fact]
        public void CanNotGetVersionNumber()
        {
            var buildMock = new Mock<IBuild>();

            var versionNumber = buildMock.Object.VersionNumber();

            versionNumber.ShouldBeNull();
        }

        [Theory]
        [AutoData]
        public void CanGetVersionNumberForVersion(string version)
        {
            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Version).Returns(version);

            var versionNumber = buildMock.Object.VersionNumber();

            versionNumber.ShouldBe(version);
        }

        [Theory]
        [AutoData]
        public void CanGetVersionNumberForNumber(string number)
        {
            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Number).Returns(number);

            var versionNumber = buildMock.Object.VersionNumber();

            versionNumber.ShouldBe(number);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(BuildStatus.Failed)]
        [InlineData(BuildStatus.Succeeded)]
        [InlineData(BuildStatus.Stopped)]
        [InlineData(BuildStatus.Queued)]
        public void CanNotGetRunTimeWithoutStartTime(BuildStatus? status)
        {
            var utcNow = DateTime.UtcNow;

            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Status).Returns(status);
            buildMock.Setup(m => m.EndTime).Returns(utcNow);

            var runTime = buildMock.Object.RunTime();

            runTime.ShouldBeNull();
        }

        [Theory]
        [InlineData(null)]
        [InlineData(BuildStatus.Failed)]
        [InlineData(BuildStatus.Succeeded)]
        [InlineData(BuildStatus.Stopped)]
        [InlineData(BuildStatus.Queued)]
        public void CanNotGetRunTimeWithoutEndTime(BuildStatus? status)
        {
            var utcNow = DateTime.UtcNow;

            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Status).Returns(status);
            buildMock.Setup(m => m.StartTime).Returns(utcNow);

            var runTime = buildMock.Object.RunTime();

            runTime.ShouldBeNull();
        }

        [Theory]
        [InlineData(null)]
        [InlineData(BuildStatus.Failed)]
        [InlineData(BuildStatus.Succeeded)]
        [InlineData(BuildStatus.InProgress)]
        [InlineData(BuildStatus.Stopped)]
        [InlineData(BuildStatus.Queued)]
        public void CanNotGetRunTimedWithoutStartAndEndTime(BuildStatus? status)
        {
            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Status).Returns(status);

            var runTime = buildMock.Object.RunTime();

            runTime.ShouldBeNull();
        }

        [Fact]
        public void CanNotGetRunTimeWhenInProgressWithoutStartTime()
        {
            var utcNow = DateTime.UtcNow;

            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Status).Returns(BuildStatus.InProgress);
            buildMock.Setup(m => m.EndTime).Returns(utcNow);

            var runTime = buildMock.Object.RunTime();

            runTime.ShouldBeNull();
        }

        [Fact]
        public void CanGetRunTimeWhenInProgressWithoutEndTime()
        {
            var utcNow = DateTime.UtcNow;

            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Status).Returns(BuildStatus.InProgress);
            buildMock.Setup(m => m.StartTime).Returns(utcNow);

            var runTime = buildMock.Object.RunTime();

            runTime.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(BuildStatus.Failed, 1)]
        [InlineData(BuildStatus.Succeeded, 2)]
        [InlineData(BuildStatus.Stopped, 3)]
        public void CanGetRunTime(BuildStatus status, int hoursRunning)
        {
            var utcNow = DateTime.UtcNow;

            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Status).Returns(status);
            buildMock.Setup(m => m.StartTime).Returns(utcNow.AddHours(-1 * hoursRunning));
            buildMock.Setup(m => m.EndTime).Returns(utcNow);

            var runTime = buildMock.Object.RunTime();

            // ReSharper disable once PossibleInvalidOperationException
            runTime.Value.TotalHours.ShouldBe(hoursRunning);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void CanGetRunTimeWhenInProgress(int hoursRunning)
        {
            var utcNow = DateTime.UtcNow;

            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Status).Returns(BuildStatus.InProgress);
            buildMock.Setup(m => m.StartTime).Returns(utcNow.AddHours(-1 * hoursRunning));

            var runTime = buildMock.Object.RunTime(utcNow);

            // ReSharper disable once PossibleInvalidOperationException
            runTime.Value.TotalHours.ShouldBe(hoursRunning);
        }
    }
}