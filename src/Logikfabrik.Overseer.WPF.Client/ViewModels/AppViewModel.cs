// <copyright file="AppViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using System;
    using Caliburn.Micro;
    using EnsureThat;
    using Navigation;
    using Overseer.Extensions;
    using WPF.ViewModels;

    /// <summary>
    /// The <see cref="AppViewModel" /> class.
    /// </summary>
#pragma warning disable S110 // Inheritance tree of classes should not be too deep
    public sealed class AppViewModel : Conductor<IViewModel>.Collection.OneActive, IHandle<NavigationMessage>, IDisposable
#pragma warning restore S110 // Inheritance tree of classes should not be too deep
    {
        private IEventAggregator _eventAggregator;
        private Navigator<IViewModel> _navigator;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="appDomain">The application domain.</param>
        /// <param name="menuViewModel">The menu view model.</param>
        /// <param name="connectionsViewModel">The connections view model.</param>
        public AppViewModel(IEventAggregator eventAggregator, AppDomain appDomain, MenuViewModel menuViewModel, ConnectionsViewModel connectionsViewModel)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(appDomain).IsNotNull();
            Ensure.That(menuViewModel).IsNotNull();
            Ensure.That(connectionsViewModel).IsNotNull();

            _eventAggregator = eventAggregator;
            _navigator = new Navigator<IViewModel>(this);

            _eventAggregator.Subscribe(this);

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
        public string ViewDisplayName => ActiveItem?.DisplayName;

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
            this.ThrowIfDisposed(_isDisposed);

            _navigator.Navigate(message);
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

            _eventAggregator?.Unsubscribe(this);
            _eventAggregator = null;
            _navigator = null;

            _isDisposed = true;
        }

        /// <summary>
        /// Changes the active item.
        /// </summary>
        /// <param name="newItem">The new item to activate.</param>
        /// <param name="closePrevious">Indicates whether or not to close the previous active item.</param>
        protected override void ChangeActiveItem(IViewModel newItem, bool closePrevious)
        {
            base.ChangeActiveItem(newItem, closePrevious);

            NotifyOfPropertyChange(() => ViewDisplayName);
        }
    }
}