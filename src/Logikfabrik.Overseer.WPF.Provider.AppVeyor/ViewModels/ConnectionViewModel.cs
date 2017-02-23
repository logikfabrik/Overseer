﻿// <copyright file="ConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.ViewModels
{
    using System;
    using Caliburn.Micro;
    using EnsureThat;
    using WPF.ViewModels.Factories;

    /// <summary>
    /// The <see cref="ConnectionViewModel" /> class.
    /// </summary>
    public class ConnectionViewModel : WPF.ViewModels.ConnectionViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IEditConnectionViewModelFactory<ConnectionSettings> _editConnectionFactory;
        private readonly Settings.ConnectionSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildMonitor">The build monitor.</param>
        /// <param name="projectFactory">The project factory.</param>
        /// <param name="removeConnectionFactory">The remove connection factory.</param>
        /// <param name="editConnectionFactory">The edit connection factory.</param>
        /// <param name="settings">The settings.</param>
        public ConnectionViewModel(IEventAggregator eventAggregator, IBuildMonitor buildMonitor, IProjectViewModelFactory projectFactory, IRemoveConnectionViewModelFactory removeConnectionFactory, IEditConnectionViewModelFactory<ConnectionSettings> editConnectionFactory, Settings.ConnectionSettings settings)
            : base(eventAggregator, buildMonitor, projectFactory, removeConnectionFactory, settings.Id)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(editConnectionFactory).IsNotNull();

            _eventAggregator = eventAggregator;
            _editConnectionFactory = editConnectionFactory;
            _settings = settings;
        }

        /// <summary>
        /// Edit the connection.
        /// </summary>
        public override void Edit()
        {
            var viewModel = _editConnectionFactory.Create(_settings);

            var message = new NavigationMessage2(viewModel);

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}
