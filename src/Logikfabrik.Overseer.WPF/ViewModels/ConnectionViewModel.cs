﻿// <copyright file="ConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using Settings;

    /// <summary>
    /// The <see cref="ConnectionViewModel" /> class.
    /// </summary>
    public class ConnectionViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionViewModel"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="settings">The settings.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="provider" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="settings" /> is <c>null</c>.</exception>
        public ConnectionViewModel(BuildProvider provider, BuildProviderSettings settings)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            Provider = provider;
            Settings = settings;
        }

        /// <summary>
        /// Gets the provider.
        /// </summary>
        /// <value>
        /// The provider.
        /// </value>
        public BuildProvider Provider { get; }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public BuildProviderSettings Settings { get; }
    }
}
