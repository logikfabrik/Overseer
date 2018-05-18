// <copyright file="AppMenuViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.Windows.Input;
    using Caliburn.Micro;
    using EnsureThat;
    using JetBrains.Annotations;
    using Navigation;

    /// <summary>
    /// The <see cref="AppMenuViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class AppMenuViewModel : PropertyChangedBase, IDisposable
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMouseManager _mouseManager;
        private readonly INavigationMessageFactory<ViewDashboardViewModel> _viewDashboardViewModelNavigationMessageFactory;
        private readonly INavigationMessageFactory<ViewConnectionsViewModel> _viewConnectionsViewModelNavigationMessageFactory;
        private readonly INavigationMessageFactory<NewConnectionViewModel> _newConnectionViewModelNavigationMessageFactory;
        private readonly INavigationMessageFactory<EditSettingsViewModel> _editSettingsViewModelNavigationMessageFactory;
        private readonly INavigationMessageFactory<ViewAboutViewModel> _viewAboutViewModelNavigationMessageFactory;
        private bool _isExpanded;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppMenuViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="mouseManager">The mouse manager.</param>
        /// <param name="viewDashboardViewModelNavigationMessageFactory">The view dashboard view model navigation message factory.</param>
        /// <param name="viewConnectionsViewModelNavigationMessageFactory">The view connections view model navigation message factory.</param>
        /// <param name="newConnectionViewModelNavigationMessageFactory">The new connection view model navigation message factory.</param>
        /// <param name="editSettingsViewModelNavigationMessageFactory">The edit settings view model navigation message factory.</param>
        /// <param name="viewAboutViewModelNavigationMessageFactory">The view about view model navigation message factory.</param>
        /// <param name="connectionsViewModel">The connections view model.</param>
        [UsedImplicitly]

        // ReSharper disable once InheritdocConsiderUsage
#pragma warning disable S107 // Methods should not have too many parameters
        public AppMenuViewModel(
            IEventAggregator eventAggregator,
            IMouseManager mouseManager,
            INavigationMessageFactory<ViewDashboardViewModel> viewDashboardViewModelNavigationMessageFactory,
            INavigationMessageFactory<ViewConnectionsViewModel> viewConnectionsViewModelNavigationMessageFactory,
            INavigationMessageFactory<NewConnectionViewModel> newConnectionViewModelNavigationMessageFactory,
            INavigationMessageFactory<EditSettingsViewModel> editSettingsViewModelNavigationMessageFactory,
            INavigationMessageFactory<ViewAboutViewModel> viewAboutViewModelNavigationMessageFactory,
            ConnectionsViewModel connectionsViewModel)
        {
#pragma warning restore S107 // Methods should not have too many parameters
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(mouseManager).IsNotNull();
            Ensure.That(viewDashboardViewModelNavigationMessageFactory).IsNotNull();
            Ensure.That(viewConnectionsViewModelNavigationMessageFactory).IsNotNull();
            Ensure.That(newConnectionViewModelNavigationMessageFactory).IsNotNull();
            Ensure.That(editSettingsViewModelNavigationMessageFactory).IsNotNull();
            Ensure.That(viewAboutViewModelNavigationMessageFactory).IsNotNull();
            Ensure.That(connectionsViewModel).IsNotNull();

            _eventAggregator = eventAggregator;
            _mouseManager = mouseManager;
            _viewDashboardViewModelNavigationMessageFactory = viewDashboardViewModelNavigationMessageFactory;
            _viewConnectionsViewModelNavigationMessageFactory = viewConnectionsViewModelNavigationMessageFactory;
            _newConnectionViewModelNavigationMessageFactory = newConnectionViewModelNavigationMessageFactory;
            _editSettingsViewModelNavigationMessageFactory = editSettingsViewModelNavigationMessageFactory;
            _viewAboutViewModelNavigationMessageFactory = viewAboutViewModelNavigationMessageFactory;

            _mouseManager.PreProcessInput += MouseManagerPreProcessMouse;

            Connections = connectionsViewModel;
        }

        /// <summary>
        /// Gets the connections list.
        /// </summary>
        /// <value>
        /// The connections list.
        /// </value>
        public ConnectionsViewModel Connections { get; }

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
            var message = _viewDashboardViewModelNavigationMessageFactory.Create();

            GoTo(message);
        }

        /// <summary>
        /// Goes to connections.
        /// </summary>
        public void GoToConnections()
        {
            var message = _viewConnectionsViewModelNavigationMessageFactory.Create();

            GoTo(message);
        }

        /// <summary>
        /// Goes to add connection.
        /// </summary>
        public void GoToAddConnection()
        {
            var message = _newConnectionViewModelNavigationMessageFactory.Create();

            GoTo(message);
        }

        /// <summary>
        /// Goes to settings.
        /// </summary>
        public void GoToSettings()
        {
            var message = _editSettingsViewModelNavigationMessageFactory.Create();

            GoTo(message);
        }

        /// <summary>
        /// Goes to about.
        /// </summary>
        public void GoToAbout()
        {
            var message = _viewAboutViewModelNavigationMessageFactory.Create();

            GoTo(message);
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

        private void GoTo(INavigationMessage message)
        {
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
