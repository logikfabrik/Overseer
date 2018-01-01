// <copyright file="Project.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.Codeship
{
    using System;
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

            Id = project.Id.ToString();
            Name = GetName(project.Name);
        }

        /// <inheritdoc />
        public string Id { get; }

        /// <inheritdoc />
        public string Name { get; }

        private static string GetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return name;
            }

            var index = name.IndexOf("/", StringComparison.Ordinal);

            return index == -1 ? name : name.Substring(index + 1);
        }
    }
}