// <copyright file="IBuildViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    /// <summary>
    /// The <see cref="IBuildViewModelFactory" /> interface.
    /// </summary>
    public interface IBuildViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="build">The build.</param>
        /// <returns>A view model.</returns>
        BuildViewModel Create(IProject project, IBuild build);
    }
}
