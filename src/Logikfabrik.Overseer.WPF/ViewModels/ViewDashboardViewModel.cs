// <copyright file="ViewDashboardViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;
    using JetBrains.Annotations;

    /// <summary>
    /// The <see cref="ViewDashboardViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ViewDashboardViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewDashboardViewModel" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        /// <param name="favoritesViewModel">The favorites view model.</param>
        [UsedImplicitly]

        // ReSharper disable once InheritdocConsiderUsage
        public ViewDashboardViewModel(IPlatformProvider platformProvider, FavoritesViewModel favoritesViewModel)
            : base(platformProvider)
        {
            Ensure.That(favoritesViewModel).IsNotNull();

            Favorites = favoritesViewModel;
            DisplayName = Properties.Resources.ViewDashboard_View;
            KeepAlive = true; // TODO: Why do we need this?
        }

        /// <summary>
        /// Gets the favorites.
        /// </summary>
        /// <value>
        /// The favorites.
        /// </value>
        public FavoritesViewModel Favorites { get; }
    }
}
