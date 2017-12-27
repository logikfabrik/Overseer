// <copyright file="ProjectTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Test
{
    using Ploeh.AutoFixture.Xunit2;
    using Shouldly;
    using Xunit;

    public class ProjectTest
    {
        [Theory]
        [AutoData]
        public void CanGetId(string id)
        {
            var project = new Api.Models.Project { Id = id };

            new Project(project).Id.ShouldBe(id);
        }

        [Theory]
        [AutoData]
        public void CanGetName(string name)
        {
            var project = new Api.Models.Project { Name = name };

            new Project(project).Name.ShouldBe(name);
        }
    }
}
