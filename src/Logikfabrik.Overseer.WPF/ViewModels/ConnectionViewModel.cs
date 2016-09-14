// <copyright file="ConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.Collections.Generic;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ConnectionViewModel" /> class.
    /// </summary>
    public class ConnectionViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionViewModel"/> class.
        /// </summary>
        /// <param name="buildProvider">The build provider settings.</param>
        public ConnectionViewModel(BuildProvider buildProvider)
        {
            Ensure.That(buildProvider).IsNotNull();

            ProviderName = buildProvider.ProviderName;
            ConnectionName = buildProvider.Settings.Name;

            ProjectBuildViewModels = new List<ProjectBuildViewModel>
            {
                new ProjectBuildViewModel(),
                new ProjectBuildViewModel(),
                new ProjectBuildViewModel(),
                new ProjectBuildViewModel(),
                new ProjectBuildViewModel(),
                new ProjectBuildViewModel(),
                new ProjectBuildViewModel(),
            };
        }

        /// <summary>
        /// Gets the connection name.
        /// </summary>
        /// <value>
        /// The connection name.
        /// </value>
        public string ConnectionName { get; }

        /// <summary>
        /// Gets provider name.
        /// </summary>
        /// <value>
        /// The provider name.
        /// </value>
        public string ProviderName { get; }

        /// <summary>
        /// Gets or sets the project build view models.
        /// </summary>
        /// <value>
        /// The project build view models.
        /// </value>
        public IEnumerable<ProjectBuildViewModel> ProjectBuildViewModels { get; set; }
    }
}
