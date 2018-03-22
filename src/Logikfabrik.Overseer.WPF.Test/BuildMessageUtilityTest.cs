// <copyright file="BuildMessageUtilityTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Test
{
    using Shouldly;
    using Xunit;

    public class BuildMessageUtilityTest
    {
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
    }
}