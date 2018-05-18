// <copyright file="AppViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;
    using JetBrains.Annotations;

    /// <summary>
    /// The <see cref="AppViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
#pragma warning disable S110 // Inheritance tree of classes should not be too deep
    public sealed class AppViewModel : Conductor<IViewModel>
#pragma warning restore S110 // Inheritance tree of classes should not be too deep
    {
        private readonly IAppSettingsFactory _appSettingsFactory;
        private bool _isShowingNotifications;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="menuViewModel">The menu view model.</param>
        /// <param name="errorViewModel">The error view model.</param>
        /// <param name="viewDashboardViewModel">The view dashboard view model.</param>
        /// <param name="appSettingsFactory">The app settings factory.</param>
        [UsedImplicitly]

        // ReSharper disable once InheritdocConsiderUsage
        public AppViewModel(IEventAggregator eventAggregator, AppMenuViewModel menuViewModel, AppErrorViewModel errorViewModel, ViewDashboardViewModel viewDashboardViewModel, IAppSettingsFactory appSettingsFactory)
            : base(eventAggregator)
        {
            Ensure.That(menuViewModel).IsNotNull();
            Ensure.That(errorViewModel).IsNotNull();
            Ensure.That(viewDashboardViewModel).IsNotNull();
            Ensure.That(appSettingsFactory).IsNotNull();

            Menu = menuViewModel;
            Error = errorViewModel;

            DisplayName = "Overseer";

            ActivateItem(viewDashboardViewModel);

            _appSettingsFactory = appSettingsFactory;
            _isShowingNotifications = _appSettingsFactory.Create().ShowNotifications;
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

        public bool IsShowingNotifications => _isShowingNotifications;

        public bool IsNotShowingNotifications => !_isShowingNotifications;

        public void HideNotifications()
        {
            ToggleNotifications(false);
        }

        public void ShowNotifications()
        {
            ToggleNotifications(true);
        }

        /// <inheritdoc />
        protected override void ChangeActiveItem(IViewModel newItem, bool closePrevious)
        {
            base.ChangeActiveItem(newItem, closePrevious);

            NotifyOfPropertyChange(() => ViewDisplayName);
        }

        private void ToggleNotifications(bool show)
        {
            var settings = _appSettingsFactory.Create();

            settings.ShowNotifications = show;

            settings.Save();

            _isShowingNotifications = show;

            NotifyOfPropertyChange(() => IsShowingNotifications);
            NotifyOfPropertyChange(() => IsNotShowingNotifications);
        }
    }
}