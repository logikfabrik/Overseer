// <copyright file="Project.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.Codeship
{
    using EnsureThat;

    /// <summary>
    /// The <see cref="Project" /> class.
    /// </summary>
    public class Project : IProject
    {
        public Project(Api.Models.Project project)
        {
            Ensure.That(project).IsNotNull();

            Id = project.Id.ToString();
            Name = project.Name;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }
    }
}