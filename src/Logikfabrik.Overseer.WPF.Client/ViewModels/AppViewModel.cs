// <copyright file="AppViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using System;
    using System.Linq;
    using System.Windows;
    using Caliburn.Micro;
    using EnsureThat;
    using Navigation;
    using WPF.ViewModels;

    /// <summary>
    /// The <see cref="AppViewModel" /> class.
    /// </summary>
#pragma warning disable S110 // Inheritance tree of classes should not be too deep
    public sealed class AppViewModel : Conductor<IViewModel>.Collection.OneActive, IHandle<NavigationMessage>, IDisposable
#pragma warning restore S110 // Inheritance tree of classes should not be too deep
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IBuildMonitor _buildMonitor;
        private readonly IBuildNotificationManager _buildNotificationManager;
        private readonly Navigator<IViewModel> _navigator;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildMonitor">The build monitor.</param>
        /// /// <param name="buildNotificationManager">The build notification manager.</param>
        /// <param name="menuViewModel">The menu view model.</param>
        /// <param name="connectionsViewModel">The connections view model.</param>
        public AppViewModel(IEventAggregator eventAggregator, IBuildMonitor buildMonitor, IBuildNotificationManager buildNotificationManager, MenuViewModel menuViewModel, ConnectionsViewModel connectionsViewModel)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(buildMonitor).IsNotNull();
            Ensure.That(buildNotificationManager).IsNotNull();
            Ensure.That(menuViewModel).IsNotNull();
            Ensure.That(connectionsViewModel).IsNotNull();

            _eventAggregator = eventAggregator;
            _buildMonitor = buildMonitor;
            _buildNotificationManager = buildNotificationManager;
            _navigator = new Navigator<IViewModel>(this);

            _eventAggregator.Subscribe(this);
            WeakEventManager<IBuildMonitor, BuildMonitorProjectProgressEventArgs>.AddHandler(_buildMonitor, nameof(_buildMonitor.ProjectProgressChanged), BuildMonitorProgressChanged);

            DisplayName = "Overseer";

            Menu = menuViewModel;

            ActivateItem(connectionsViewModel);
        }

        /// <summary>
        /// Gets the view display name.
        /// </summary>
        /// <value>
        /// The view display name.
        /// </value>
        public string ViewDisplayName => ActiveItem.DisplayName;

        /// <summary>
        /// Gets the menu.
        /// </summary>
        /// <value>
        /// The menu.
        /// </value>
        public MenuViewModel Menu { get; }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message to handle.</param>
        public void Handle(NavigationMessage message)
        {
            _navigator.Navigate(message);

            NotifyOfPropertyChange(() => ViewDisplayName);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _eventAggregator.Unsubscribe(this);

            WeakEventManager<IBuildMonitor, BuildMonitorProjectProgressEventArgs>.RemoveHandler(_buildMonitor, nameof(_buildMonitor.ProjectProgressChanged), BuildMonitorProgressChanged);

            _isDisposed = true;
        }

        private void BuildMonitorProgressChanged(object sender, BuildMonitorProjectProgressEventArgs e)
        {
            if (!e.Builds.Any())
            {
                return;
            }

            foreach (var build in e.Builds)
            {
                _buildNotificationManager.ShowNotification(e.Project, build);
            }
        }
    }
}