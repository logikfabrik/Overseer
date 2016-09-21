// <copyright file="BuildProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

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

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns>
        /// A task.
        /// </returns>
        public override async Task<IEnumerable<IProject>> GetProjectsAsync()
        {
            // TODO: Implement!
            // Thread.Sleep(5000);

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

        /// <summary>
        /// Gets the builds for the project with the specified project identifier.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <returns>
        /// A task.
        /// </returns>
        public override async Task<IEnumerable<IBuild>> GetBuildsAsync(string projectId)
        {
            // TODO: Implement!
            //Thread.Sleep(5000);

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
