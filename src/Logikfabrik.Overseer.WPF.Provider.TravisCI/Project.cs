// <copyright file="Project.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI
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
        /// <param name="repository">The repository.</param>
        public Project(Api.Models.Repository repository)
        {
            Ensure.That(repository).IsNotNull();

            Id = repository.Id.ToString();
            Name = repository.Name;
        }

        /// <inheritdoc />
        public string Id { get; }

        /// <inheritdoc />
        public string Name { get; }
    }
}
