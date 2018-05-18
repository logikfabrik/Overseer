// <copyright file="ViewConnectionsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;
    using JetBrains.Annotations;

    /// <summary>
    /// The <see cref="ViewConnectionsViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ViewConnectionsViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewConnectionsViewModel" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        /// <param name="connectionsViewModel">The connections view model.</param>
        [UsedImplicitly]

        // ReSharper disable once InheritdocConsiderUsage
        public ViewConnectionsViewModel(IPlatformProvider platformProvider, ConnectionsViewModel connectionsViewModel)
            : base(platformProvider)
        {
            Ensure.That(connectionsViewModel).IsNotNull();

            Connections = connectionsViewModel;
            DisplayName = Properties.Resources.ViewConnections_View;
            KeepAlive = true; // TODO: Why do we need this?
        }

        /// <summary>
        /// Gets the connections.
        /// </summary>
        /// <value>
        /// The connections.
        /// </value>
        public ConnectionsViewModel Connections { get; }
    }
}