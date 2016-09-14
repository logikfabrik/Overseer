// <copyright file="ProjectBuildViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ProjectBuildViewModel" /> class.
    /// </summary>
    public class ProjectBuildViewModel : PropertyChangedBase
    {
        public ProjectBuildViewModel(IProject project, IBuild build)
        {
            Ensure.That(project).IsNotNull();
            Ensure.That(build).IsNotNull();

            ProjectName = project.Name;
            BuildViewModel = new BuildViewModel(build);
        }

        /// <summary>
        /// Gets the project name.
        /// </summary>
        /// <value>
        /// The project name.
        /// </value>
        public string ProjectName { get; }

        /// <summary>
        /// Gets the build view model.
        /// </summary>
        /// <value>
        /// The build view model.
        /// </value>
        public BuildViewModel BuildViewModel { get; }
    }
}
