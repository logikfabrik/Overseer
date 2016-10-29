// <copyright file="AppViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;
    using Caliburn.Micro;
    using EnsureThat;
    using WPF.ViewModels;

    /// <summary>
    /// The <see cref="AppViewModel" /> class.
    /// </summary>
    public sealed class AppViewModel : Conductor<PropertyChangedBase>, IHandle<NavigationMessage>
    {
        private readonly INotificationManager _notificationManager;
        private readonly Lazy<DateTime> _appStartTime = new Lazy<DateTime>(() => Process.GetCurrentProcess().StartTime);

        /// <summary>
        /// Initializes a new instance of the <see cref="AppViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="notificationManager">The notification manager.</param>
        /// <param name="buildMonitor">The build monitor.</param>
        /// <param name="connectionsViewModel">The connections view model.</param>
        public AppViewModel(IEventAggregator eventAggregator, INotificationManager notificationManager, IBuildMonitor buildMonitor, ConnectionsViewModel connectionsViewModel)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(connectionsViewModel).IsNotNull();

            _notificationManager = notificationManager;
            eventAggregator.Subscribe(this);

            WeakEventManager<IBuildMonitor, BuildMonitorProgressEventArgs>.AddHandler(buildMonitor, nameof(buildMonitor.ProgressChanged), BuildMonitorProgressChanged);

            DisplayName = "Overseer";

            ActivateItem(connectionsViewModel);
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message to handle.</param>
        public void Handle(NavigationMessage message)
        {
            var viewModel = IoC.GetInstance(message.ViewModelType, null) as PropertyChangedBase;

            ActivateItem(viewModel);
        }

        private void BuildMonitorProgressChanged(object sender, BuildMonitorProgressEventArgs e)
        {
            if (!e.Builds.Any())
            {
                return;
            }

            foreach (var build in e.Builds.Where(build => !(build.Finished <= _appStartTime.Value)))
            {
                _notificationManager.ShowNotification(new BuildNotificationViewModel(build));
            }
        }
    }
}
