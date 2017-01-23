// <copyright file="ConnectionSettingsViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.ViewModels.Factories
{
    using EnsureThat;
    using WPF.ViewModels.Factories;

    /// <summary>
    /// The <see cref="ConnectionSettingsViewModelFactory" /> class.
    /// </summary>
    public class ConnectionSettingsViewModelFactory : IConnectionSettingsViewModelFactory
    {
        private readonly IProjectToMonitorViewModelFactory _projectToMonitorFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsViewModelFactory" /> class.
        /// </summary>
        /// <param name="projectToMonitorFactory">The project to monitor factory.</param>
        public ConnectionSettingsViewModelFactory(IProjectToMonitorViewModelFactory projectToMonitorFactory)
        {
            Ensure.That(projectToMonitorFactory).IsNotNull();

            _projectToMonitorFactory = projectToMonitorFactory;
        }

        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <returns>
        /// A view model.
        /// </returns>
        public ConnectionSettingsViewModel Create()
        {
            return new ConnectionSettingsViewModel(_projectToMonitorFactory);
        }
    }
}
