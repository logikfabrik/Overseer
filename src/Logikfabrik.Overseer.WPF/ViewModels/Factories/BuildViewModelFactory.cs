// <copyright file="BuildViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using EnsureThat;
    using Overseer.Extensions;

    /// <summary>
    /// The <see cref="BuildViewModelFactory" /> class.
    /// </summary>
    public class BuildViewModelFactory : IBuildViewModelFactory
    {
        private readonly IChangeViewModelFactory _changeFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildViewModelFactory" /> class.
        /// </summary>
        /// <param name="changeFactory">The change factory.</param>
        public BuildViewModelFactory(IChangeViewModelFactory changeFactory)
        {
            Ensure.That(changeFactory).IsNotNull();

            _changeFactory = changeFactory;
        }

        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="projectName">The project name.</param>
        /// <param name="build">The build.</param>
        /// <returns>
        /// A view model.
        /// </returns>
        public BuildViewModel Create(string projectName, IBuild build)
        {
            Ensure.That(build).IsNotNull();

            var viewModel = new BuildViewModel(
                _changeFactory,
                projectName,
                build.Id,
                build.Branch,
                build.GetVersionNumber(),
                build.RequestedBy,
                build.Changes,
                build.Status,
                build.StartTime,
                build.EndTime,
                build.GetRunTime());

            return viewModel;
        }
    }
}
