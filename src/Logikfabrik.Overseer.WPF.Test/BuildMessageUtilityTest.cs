// <copyright file="BuildMessageUtilityTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Moq;
    using Shouldly;
    using Xunit;

    public class BuildMessageUtilityTest
    {
        [Fact]
        public void CanGetGetBuildNameForProjectAndBuildWithVersion()
        {
            var projectMock = new Mock<IProject>();

            projectMock.Setup(m => m.Name).Returns("My Project");

            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Version).Returns("1.0.0");

            var buildName = BuildMessageUtility.GetBuildName(projectMock.Object, buildMock.Object);

            buildName.ShouldBe("My Project 1.0.0");
        }

        [Fact]
        public void CanGetGetBuildNameForProjectAndBuildWithNumber()
        {
            var projectMock = new Mock<IProject>();

            projectMock.Setup(m => m.Name).Returns("My Project");

            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Number).Returns("100");

            var buildName = BuildMessageUtility.GetBuildName(projectMock.Object, buildMock.Object);

            buildName.ShouldBe("My Project 100");
        }

        [Fact]
        public void CanGetGetBuildNameForProjectAndBuildWithoutVersionOrNumber()
        {
            var projectMock = new Mock<IProject>();

            projectMock.Setup(m => m.Name).Returns("My Project");

            var buildMock = new Mock<IBuild>();

            var buildName = BuildMessageUtility.GetBuildName(projectMock.Object, buildMock.Object);

            buildName.ShouldBe("My Project");
        }

        [Theory]
        [InlineData(null, null, null)]
        [InlineData("My Project", null, "My Project")]
        [InlineData("My Project", "1.0.0", "My Project 1.0.0")]
        [InlineData(null, "1.0.0", "1.0.0")]
        public void CanGetBuildName(string projectName, string versionNumber, string expected)
        {
            var buildName = BuildMessageUtility.GetBuildName(projectName, versionNumber);

            buildName.ShouldBe(expected);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(BuildStatus.Failed, "Build failed")]
        [InlineData(BuildStatus.Succeeded, "Build succeeded")]
        [InlineData(BuildStatus.InProgress, "Build in progress")]
        [InlineData(BuildStatus.Stopped, "Build stopped")]
        [InlineData(BuildStatus.Queued, "Build queued")]
        public void CanGetBuildStatusMessage(BuildStatus? status, string expected)
        {
            var buildStatusMessage = BuildMessageUtility.GetBuildStatusMessage(status);

            buildStatusMessage.ShouldBe(expected);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(BuildStatus.Failed, "Build requested by John Doe and modified by Jane Doe failed")]
        [InlineData(BuildStatus.Succeeded, "Build requested by John Doe and modified by Jane Doe succeeded")]
        [InlineData(BuildStatus.InProgress, "Build requested by John Doe and modified by Jane Doe in progress")]
        [InlineData(BuildStatus.Stopped, "Build requested by John Doe and modified by Jane Doe stopped")]
        [InlineData(BuildStatus.Queued, "Build requested by John Doe and modified by Jane Doe queued")]
        public void CanGetBuildStatusMessageWithParts(BuildStatus? status, string expected)
        {
            var parts = GetPartsForBuildStatusMessage();

            var buildStatusMessage = BuildMessageUtility.GetBuildStatusMessage(status, parts);

            buildStatusMessage.ShouldBe(expected);
        }

        [Theory]
        [ClassData(typeof(CanGetBuildRunTimeMessageClassData))]
        public void CanGetBuildRunTimeMessage(DateTime currentTime, BuildStatus? status, DateTime? endTime, TimeSpan? runTime, string expected)
        {
            var buildRunTimeMessage = BuildMessageUtility.GetBuildRunTimeMessage(currentTime, status, endTime, runTime);

            buildRunTimeMessage.ShouldBe(expected);
        }

        private static IDictionary<string, string> GetPartsForBuildStatusMessage()
        {
            return new Dictionary<string, string>
            {
                { "requested by", "John Doe" },
                { "modified by", "Jane Doe" },
                { "canceled by", string.Empty }
            };
        }

        private class CanGetBuildRunTimeMessageClassData : IEnumerable<object[]>
        {
            private readonly IEnumerable<object[]> _data = new[]
            {
                new object[] { DateTime.UtcNow, null, null, null, null },
                new object[] { DateTime.UtcNow, null, DateTime.UtcNow, null, null },
                new object[] { DateTime.UtcNow, null, DateTime.UtcNow, TimeSpan.FromHours(1), null },
                new object[] { DateTime.UtcNow, null, null, TimeSpan.FromHours(1), null },

                new object[] { DateTime.UtcNow, BuildStatus.Failed, null, null, "Failed" },
                new object[] { DateTime.UtcNow, BuildStatus.Failed, DateTime.UtcNow, null, "Failed now" },
                new object[] { DateTime.UtcNow, BuildStatus.Failed, DateTime.UtcNow, TimeSpan.FromHours(1), "Failed in 1 hour, now" },
                new object[] { DateTime.UtcNow, BuildStatus.Failed, null, TimeSpan.FromHours(1), "Failed in 1 hour" },

                new object[] { DateTime.UtcNow, BuildStatus.Succeeded, null, null, "Succeeded" },
                new object[] { DateTime.UtcNow, BuildStatus.Succeeded, DateTime.UtcNow, null, "Succeeded now" },
                new object[] { DateTime.UtcNow, BuildStatus.Succeeded, DateTime.UtcNow, TimeSpan.FromHours(1), "Succeeded in 1 hour, now" },
                new object[] { DateTime.UtcNow, BuildStatus.Succeeded, null, TimeSpan.FromHours(1), "Succeeded in 1 hour" },

                new object[] { DateTime.UtcNow, BuildStatus.InProgress, null, null, "In progress" },
                new object[] { DateTime.UtcNow, BuildStatus.InProgress, DateTime.UtcNow, null, "In progress" },
                new object[] { DateTime.UtcNow, BuildStatus.InProgress, DateTime.UtcNow, TimeSpan.FromHours(1), "In progress for 1 hour" },
                new object[] { DateTime.UtcNow, BuildStatus.InProgress, null, TimeSpan.FromHours(1), "In progress for 1 hour" },

                new object[] { DateTime.UtcNow, BuildStatus.Stopped, null, null, "Stopped" },
                new object[] { DateTime.UtcNow, BuildStatus.Stopped, DateTime.UtcNow, null, "Stopped now" },
                new object[] { DateTime.UtcNow, BuildStatus.Stopped, DateTime.UtcNow, TimeSpan.FromHours(1), "Stopped in 1 hour, now" },
                new object[] { DateTime.UtcNow, BuildStatus.Stopped, null, TimeSpan.FromHours(1), "Stopped in 1 hour" },

                new object[] { DateTime.UtcNow, BuildStatus.Queued, null, null, "Queued" },
                new object[] { DateTime.UtcNow, BuildStatus.Queued, DateTime.UtcNow, null, "Queued" },
                new object[] { DateTime.UtcNow, BuildStatus.Queued, DateTime.UtcNow, TimeSpan.FromHours(1), "Queued" },
                new object[] { DateTime.UtcNow, BuildStatus.Queued, null, TimeSpan.FromHours(1), "Queued" }
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}