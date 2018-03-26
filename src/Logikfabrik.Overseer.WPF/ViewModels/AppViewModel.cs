// <copyright file="AppViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="AppViewModel" /> class.
    /// </summary>
#pragma warning disable S110 // Inheritance tree of classes should not be too deep

    // ReSharper disable once InheritdocConsiderUsage
    public sealed class AppViewModel : Conductor<IViewModel>
#pragma warning restore S110 // Inheritance tree of classes should not be too deep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="menuViewModel">The menu view model.</param>
        /// <param name="errorViewModel">The error view model.</param>
        /// <param name="viewDashboardViewModel">The view dashboard view model.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public AppViewModel(IEventAggregator eventAggregator, AppMenuViewModel menuViewModel, AppErrorViewModel errorViewModel, ViewDashboardViewModel viewDashboardViewModel)
            : base(eventAggregator)
        {
            Ensure.That(menuViewModel).IsNotNull();
            Ensure.That(errorViewModel).IsNotNull();
            Ensure.That(viewDashboardViewModel).IsNotNull();

            Menu = menuViewModel;
            Error = errorViewModel;

            DisplayName = "Overseer";

            ActivateItem(viewDashboardViewModel);
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
        public AppMenuViewModel Menu { get; }

        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public AppErrorViewModel Error { get; }

        /// <inheritdoc />
        protected override void ChangeActiveItem(IViewModel newItem, bool closePrevious)
        {
            base.ChangeActiveItem(newItem, closePrevious);

            NotifyOfPropertyChange(() => ViewDisplayName);
        }
    }
}