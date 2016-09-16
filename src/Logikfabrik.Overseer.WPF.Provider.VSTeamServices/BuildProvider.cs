// <copyright file="BuildProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices
{
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// The <see cref="BuildProvider" /> class.
    /// </summary>
    public class BuildProvider : Overseer.BuildProvider
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public override string Name { get; } = "VS Team Services";

        public override IEnumerable<IProject> GetProjects()
        {
            // TODO: Implement!
            Thread.Sleep(5000);

            return new[]
            {
                new StubProject
                {
                    Id = "1",
                    Name = "Project 1"
                },
                new StubProject
                {
                    Id = "2",
                    Name = "Project 2"
                },
                new StubProject
                {
                    Id = "3",
                    Name = "Project 3"
                },
                new StubProject
                {
                    Id = "4",
                    Name = "Project 4"
                }
            };
        }

        public override IEnumerable<IBuild> GetBuilds(string projectId)
        {
            // TODO: Implement!
            Thread.Sleep(5000);

            return new[]
            {
                new StubBuild
                {
                    Branch = "Master",
                    Number = "1",
                    Status = BuildStatus.Failed
                },
                new StubBuild
                {
                    Branch = "Master",
                    Number = "2",
                    Status = BuildStatus.Succeeded
                },
                new StubBuild
                {
                    Branch = "Master",
                    Number = "3",
                    Status = BuildStatus.InProgress
                }
            };
        }
    }
}
