// <copyright file="MenuViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using System;
    using System.Windows.Input;
    using Caliburn.Micro;
    using EnsureThat;
    using Navigation;
    using WPF.ViewModels;

    /// <summary>
    /// The <see cref="MenuViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class MenuViewModel : PropertyChangedBase, IDisposable
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMouseManager _mouseManager;
        private bool _isExpanded;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="mouseManager">The mouse manager.</param>
        /// <param name="connectionsListViewModel">The connections list view model.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public MenuViewModel(IEventAggregator eventAggregator, IMouseManager mouseManager, ConnectionsListViewModel connectionsListViewModel)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(mouseManager).IsNotNull();
            Ensure.That(connectionsListViewModel).IsNotNull();

            _eventAggregator = eventAggregator;
            _mouseManager = mouseManager;

            _mouseManager.PreProcessInput += MouseManagerPreProcessMouse;

            ConnectionsList = connectionsListViewModel;
        }

        /// <summary>
        /// Gets the connections list.
        /// </summary>
        /// <value>
        /// The connections list.
        /// </value>
        public ConnectionsListViewModel ConnectionsList { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is expanded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is expanded; otherwise, <c>false</c>.
        /// </value>
        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }

            set
            {
                _isExpanded = value;
                NotifyOfPropertyChange(() => IsExpanded);
            }
        }

        /// <summary>
        /// Opens this instance.
        /// </summary>
        public void Open()
        {
            IsExpanded = true;
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            IsExpanded = false;
        }

        /// <summary>
        /// Goes to dashboard.
        /// </summary>
        public void GoToDashboard()
        {
            GoTo(typeof(DashboardViewModel));
        }

        /// <summary>
        /// Goes to connections.
        /// </summary>
        public void GoToConnections()
        {
            GoTo(typeof(ConnectionsViewModel));
        }

        /// <summary>
        /// Goes to add connection.
        /// </summary>
        public void GoToAddConnection()
        {
            GoTo(typeof(BuildProvidersViewModel));
        }

        /// <summary>
        /// Goes to settings.
        /// </summary>
        public void GoToSettings()
        {
            GoTo(typeof(EditSettingsViewModel));
        }

        /// <summary>
        /// Goes to about.
        /// </summary>
        public void GoToAbout()
        {
            GoTo(typeof(AboutViewModel));
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                _mouseManager.PreProcessInput -= MouseManagerPreProcessMouse;
            }

            _isDisposed = true;
        }

        private void GoTo(Type itemType)
        {
            var message = new NavigationMessage(itemType);

            _eventAggregator.PublishOnUIThread(message);

            Close();
        }

        private void MouseManagerPreProcessMouse(object sender, NotifyInputEventArgs e)
        {
            var args = e.StagingItem.Input as MouseButtonEventArgs;

            if (args?.ChangedButton == MouseButton.Left && args.ButtonState == MouseButtonState.Released)
            {
                Close();
            }
        }
    }
}
