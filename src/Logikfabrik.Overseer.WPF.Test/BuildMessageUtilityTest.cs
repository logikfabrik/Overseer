// <copyright file="BuildMessageUtilityTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Test
{
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

            buildMock.Setup(m => m.Version).Returns("1.0.0.0");
            buildMock.Setup(m => m.Branch).Returns("My Branch");

            var buildName = BuildMessageUtility.GetBuildName(projectMock.Object, buildMock.Object);

            Assert.Equal("My Project 1.0.0.0 (My Branch)", buildName);
        }

        [Fact]
        public void CanGetGetBuildNameForProjectNameVersionNumberAndBranch()
        {
            var buildName = BuildMessageUtility.GetBuildName("My Project", "1.0.0.0", "My Branch");

            Assert.Equal("My Project 1.0.0.0 (My Branch)", buildName);
        }
    }
}
