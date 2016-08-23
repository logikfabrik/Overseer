﻿// <copyright file="ConnectionsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Caliburn.Micro;
    using WPF.ViewModels;

    /// <summary>
    /// The <see cref="ConnectionsViewModel" /> class.
    /// </summary>
    public class ConnectionsViewModel : PropertyChangedBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionsViewModel" /> class.
        /// </summary>
        /// <param name="buildProviderViewModels">The build provider view models.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="buildProviderViewModels" /> is <c>null</c>.</exception>
        public ConnectionsViewModel(IEnumerable<BuildProviderViewModel> buildProviderViewModels)
        {
            if (buildProviderViewModels == null)
            {
                throw new ArgumentNullException(nameof(buildProviderViewModels));
            }

            BuildProviderViewModels = buildProviderViewModels;
        }

        /// <summary>
        /// Gets the build providers.
        /// </summary>
        /// <value>
        /// The build providers.
        /// </value>
        public IEnumerable<BuildProviderViewModel> BuildProviderViewModels { get; }
    }
}
