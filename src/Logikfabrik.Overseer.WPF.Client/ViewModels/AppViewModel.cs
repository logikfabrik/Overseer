// <copyright file="AppViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using System.Linq;
    using System.Windows;
    using Caliburn.Micro;
    using EnsureThat;
    using WPF.ViewModels;

    /// <summary>
    /// The <see cref="AppViewModel" /> class.
    /// </summary>
    public sealed class AppViewModel : Conductor<ViewModel>, IHandle<NavigationMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IBuildNotificationManager _buildNotificationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildMonitor">The build monitor.</param>
        /// /// <param name="buildNotificationManager">The build notification manager.</param>
        /// <param name="connectionsViewModel">The connections view model.</param>
        public AppViewModel(IEventAggregator eventAggregator, IBuildMonitor buildMonitor, IBuildNotificationManager buildNotificationManager, ConnectionsViewModel connectionsViewModel)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(buildMonitor).IsNotNull();
            Ensure.That(buildNotificationManager).IsNotNull();
            Ensure.That(connectionsViewModel).IsNotNull();

            _eventAggregator = eventAggregator;
            _buildNotificationManager = buildNotificationManager;

            _eventAggregator.Subscribe(this);

            WeakEventManager<IBuildMonitor, BuildMonitorProjectProgressEventArgs>.AddHandler(buildMonitor, nameof(buildMonitor.ProjectProgressChanged), BuildMonitorProgressChanged);

            DisplayName = "Overseer";

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
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message to handle.</param>
        public void Handle(NavigationMessage message)
        {
            ViewModel viewModel;

            var message2 = message as NavigationMessage2;

            if (message2 != null)
            {
                viewModel = message2.ViewModel;
            }
            else
            {
                viewModel = IoC.GetInstance(message.ViewModelType, null) as ViewModel;
            }

            ActivateItem(viewModel);

            NotifyOfPropertyChange(() => ViewDisplayName);
        }

        /// <summary>
        /// Edit the settings.
        /// </summary>
        public void EditSettings()
        {
            var message = new NavigationMessage(typeof(EditSettingsViewModel));

            _eventAggregator.PublishOnUIThread(message);
        }

        /// <summary>
        /// View the connections.
        /// </summary>
        public void ViewConnections()
        {
            var message = new NavigationMessage(typeof(ConnectionsViewModel));

            _eventAggregator.PublishOnUIThread(message);
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