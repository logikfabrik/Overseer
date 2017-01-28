// <copyright file="BuildMessageUtilityTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Test
{
    using System.Collections.Generic;
    using Moq;
    using Xunit;

    public class BuildMessageUtilityTest
    {
        [Fact]
        public void CanGetGetBuildNameForProjectAndBuild()
        {
            var projectMock = new Mock<IProject>();

            projectMock.Setup(m => m.Name).Returns("My Project");

            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Version).Returns("1.0.0");
            buildMock.Setup(m => m.Branch).Returns("My Branch");

            var buildName = BuildMessageUtility.GetBuildName(projectMock.Object, buildMock.Object);

            Assert.Equal("My Project 1.0.0 (My Branch)", buildName);
        }

        [Fact]
        public void CanGetGetBuildNameForProjectNameVersionNumberAndBranch()
        {
            var buildName1 = BuildMessageUtility.GetBuildName("My Project", "1.0.0", "My Branch");

            Assert.Equal("My Project 1.0.0 (My Branch)", buildName1);

            var buildName2 = BuildMessageUtility.GetBuildName(null, "1.0.0", "My Branch");

            Assert.Equal("1.0.0 (My Branch)", buildName2);

            var buildName3 = BuildMessageUtility.GetBuildName(null, null, "My Branch");

            Assert.Equal("(My Branch)", buildName3);

            var buildName4 = BuildMessageUtility.GetBuildName(null, null, null);

            Assert.Null(buildName4);
        }

        [Fact]
        public void CanGetBuildStatusMessage()
        {
            var buildStatusMessage1 = BuildMessageUtility.GetBuildStatusMessage(BuildStatus.Failed);

            Assert.Equal("Build failed", buildStatusMessage1);

            var buildStatusMessage2 = BuildMessageUtility.GetBuildStatusMessage(BuildStatus.Succeeded);

            Assert.Equal("Build succeeded", buildStatusMessage2);

            var buildStatusMessage3 = BuildMessageUtility.GetBuildStatusMessage(BuildStatus.InProgress);

            Assert.Equal("Build in progress", buildStatusMessage3);

            var buildStatusMessage4 = BuildMessageUtility.GetBuildStatusMessage(BuildStatus.Stopped);

            Assert.Equal("Build stopped", buildStatusMessage4);

            var buildStatusMessage5 = BuildMessageUtility.GetBuildStatusMessage(null);

            Assert.Null(buildStatusMessage5);
        }

        [Fact]
        public void CanGetBuildStatusMessageWithParts()
        {
            var parts = new Dictionary<string, string>
            {
                { "requested by", "John Doe" },
                { "modified by", "Jane Doe" }
            };

            var buildStatusMessage1 = BuildMessageUtility.GetBuildStatusMessage(BuildStatus.Failed, parts);

            Assert.Equal("Build requested by John Doe and modified by Jane Doe failed", buildStatusMessage1);

            var buildStatusMessage2 = BuildMessageUtility.GetBuildStatusMessage(BuildStatus.Succeeded, parts);

            Assert.Equal("Build requested by John Doe and modified by Jane Doe succeeded", buildStatusMessage2);

            var buildStatusMessage3 = BuildMessageUtility.GetBuildStatusMessage(BuildStatus.InProgress, parts);

            Assert.Equal("Build requested by John Doe and modified by Jane Doe in progress", buildStatusMessage3);

            var buildStatusMessage4 = BuildMessageUtility.GetBuildStatusMessage(BuildStatus.Stopped, parts);

            Assert.Equal("Build requested by John Doe and modified by Jane Doe stopped", buildStatusMessage4);

            var buildStatusMessage5 = BuildMessageUtility.GetBuildStatusMessage(null, parts);

            Assert.Null(buildStatusMessage5);
        }
    }
}