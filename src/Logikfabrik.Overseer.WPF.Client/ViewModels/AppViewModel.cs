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
    public sealed class AppViewModel : Conductor<IViewModel>.Collection.OneActive, IHandle<NavigationMessage>
    {
        private readonly IBuildNotificationManager _buildNotificationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildMonitor">The build monitor.</param>
        /// /// <param name="buildNotificationManager">The build notification manager.</param>
        /// <param name="connectionsViewModel">The connections view model.</param>
        public AppViewModel(IEventAggregator eventAggregator, IBuildMonitor buildMonitor, IBuildNotificationManager buildNotificationManager, MenuViewModel menuViewModel, ConnectionsViewModel connectionsViewModel)
        {
            Ensure.That(buildMonitor).IsNotNull();
            Ensure.That(buildNotificationManager).IsNotNull();
            Ensure.That(menuViewModel).IsNotNull();
            Ensure.That(connectionsViewModel).IsNotNull();

            _buildNotificationManager = buildNotificationManager;

            eventAggregator.Subscribe(this);

            WeakEventManager<IBuildMonitor, BuildMonitorProjectProgressEventArgs>.AddHandler(buildMonitor, nameof(buildMonitor.ProjectProgressChanged), BuildMonitorProgressChanged);

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
            var viewModel = (message as NavigationMessage2)?.ViewModel;

            // ReSharper disable once ConvertIfStatementToNullCoalescingExpression
            if (viewModel == null)
            {
                viewModel = GetChildren().SingleOrDefault(child => child.GetType() == message.ViewModelType) ?? IoC.GetInstance(message.ViewModelType, null) as IViewModel;
            }

            ActivateItem(viewModel);

            NotifyOfPropertyChange(() => ViewDisplayName);

            // TODO: Keep track of children that can be closed (and disposed). Make sure navigation takes this into consideration.
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