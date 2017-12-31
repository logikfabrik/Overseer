// <copyright file="ConnectionsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ConnectionsViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ConnectionsViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionsViewModel" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        /// <param name="connectionsListViewModel">The connections list view model.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public ConnectionsViewModel(IPlatformProvider platformProvider, ConnectionsListViewModel connectionsListViewModel)
            : base(platformProvider)
        {
            Ensure.That(connectionsListViewModel).IsNotNull();

            ConnectionsList = connectionsListViewModel;
            DisplayName = Properties.Resources.Connections_View;
            KeepAlive = true; // TODO: Why do we need this?
        }

        /// <summary>
        /// Gets the connections list.
        /// </summary>
        /// <value>
        /// The connections list.
        /// </value>
        public ConnectionsListViewModel ConnectionsList { get; }
    }
}