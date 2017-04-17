// <copyright file="BuildProvidersViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.Collections.Generic;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildProvidersViewModel" /> class.
    /// </summary>
    public class BuildProvidersViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProvidersViewModel" /> class.
        /// </summary>
        /// <param name="providers">The providers.</param>
        public BuildProvidersViewModel(IEnumerable<IBuildProviderViewModel> providers)
        {
            Ensure.That(providers).IsNotNull();

            Providers = providers;
            DisplayName = "Add connection";
        }

        /// <summary>
        /// Gets the providers.
        /// </summary>
        /// <value>
        /// The providers.
        /// </value>
        public IEnumerable<IBuildProviderViewModel> Providers { get; }
    }
}
