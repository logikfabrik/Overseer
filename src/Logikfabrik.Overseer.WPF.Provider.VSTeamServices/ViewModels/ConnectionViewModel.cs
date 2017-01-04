// <copyright file="ConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.ViewModels
{
    using System;
    using Caliburn.Micro;
    using WPF.ViewModels.Factories;

    /// <summary>
    /// The <see cref="ConnectionViewModel" /> class.
    /// </summary>
    public class ConnectionViewModel : WPF.ViewModels.ConnectionViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildMonitor">The build monitor.</param>
        /// <param name="projectFactory">The project factory.</param>
        /// <param name="settingsId">The settings identifier.</param>
        public ConnectionViewModel(IEventAggregator eventAggregator, IBuildMonitor buildMonitor, IProjectViewModelFactory projectFactory, Guid settingsId)
            : base(eventAggregator, buildMonitor, projectFactory, settingsId)
        {
        }

        /// <summary>
        /// Gets the type of the view model to edit the connection.
        /// </summary>
        /// <value>
        /// The type of the view model to edit the connection.
        /// </value>
        protected override Type EditConnectionViewModelType { get; } = typeof(EditConnectionViewModel);
    }
}
