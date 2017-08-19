// <copyright file="ConnectionsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using EnsureThat;

    /// <summary>
    /// The <see cref="ConnectionsViewModel" /> class.
    /// </summary>
    public class ConnectionsViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionsViewModel" /> class.
        /// </summary>
        /// <param name="connectionsListViewModel">The connections list view model.</param>
        public ConnectionsViewModel(ConnectionsListViewModel connectionsListViewModel)
        {
            Ensure.That(connectionsListViewModel).IsNotNull();

            ConnectionsList = connectionsListViewModel;
            DisplayName = "Connections";
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