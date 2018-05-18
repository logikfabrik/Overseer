// <copyright file="IViewBuildViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    /// <summary>
    /// The <see cref="IViewBuildViewModelFactory" /> interface.
    /// </summary>
    public interface IViewBuildViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="build">The build.</param>
        /// <returns>A view model.</returns>
        ViewBuildViewModel Create(IProject project, IBuild build);
    }
}
