// <copyright file="IViewNotificationViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    /// <summary>
    /// The <see cref="IViewNotificationViewModelFactory" /> interface.
    /// </summary>
    public interface IViewNotificationViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="build">The build.</param>
        /// <returns>A view model.</returns>
        ViewNotificationViewModel Create(IProject project, IBuild build);
    }
}