// <copyright file="Project.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor
{
    using EnsureThat;

    /// <summary>
    /// The <see cref="Project" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class Project : IProject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Project" /> class.
        /// </summary>
        /// <param name="project">The project.</param>
        public Project(Api.Models.Project project)
        {
            Ensure.That(project).IsNotNull();

            Id = project.ProjectId.ToString();
            Name = project.Name;
        }

        /// <inheritdoc />
        public string Id { get; }

        /// <inheritdoc />
        public string Name { get; }
    }
}
