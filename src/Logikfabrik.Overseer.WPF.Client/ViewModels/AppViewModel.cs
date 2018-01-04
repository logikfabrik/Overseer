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

    // ReSharper disable once InheritdocConsiderUsage
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
        /// <param name="menuViewModel">The menu view model.</param>
        /// <param name="errorViewModel">The error view model.</param>
        /// <param name="connectionsViewModel">The connections view model.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public AppViewModel(IEventAggregator eventAggregator, AppMenuViewModel menuViewModel, AppErrorViewModel errorViewModel, ConnectionsViewModel connectionsViewModel)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(menuViewModel).IsNotNull();
            Ensure.That(errorViewModel).IsNotNull();
            Ensure.That(connectionsViewModel).IsNotNull();

            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            _navigator = new Navigator<IViewModel>(this);

            Menu = menuViewModel;
            Error = errorViewModel;

            DisplayName = "Overseer";

            ActivateItem(connectionsViewModel);
        }

        /// <summary>
        /// Gets the bar name.
        /// </summary>
        /// <value>
        /// The bar name.
        /// </value>
        public string BarName => ActiveItem?.DisplayName;

        /// <summary>
        /// Gets the menu.
        /// </summary>
        /// <value>
        /// The menu.
        /// </value>
        public AppMenuViewModel Menu { get; }

        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public AppErrorViewModel Error { get; }

        /// <inheritdoc />
        public void Handle(NavigationMessage message)
        {
            this.ThrowIfDisposed(_isDisposed);

            _navigator.Navigate(message);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        protected override void ChangeActiveItem(IViewModel newItem, bool closePrevious)
        {
            base.ChangeActiveItem(newItem, closePrevious);

            NotifyOfPropertyChange(() => BarName);
        }
    }
}