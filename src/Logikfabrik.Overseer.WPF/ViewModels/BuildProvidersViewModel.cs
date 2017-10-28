// <copyright file="BuildProvidersViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.Collections.Generic;
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildProvidersViewModel" /> class.
    /// </summary>
    public class BuildProvidersViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProvidersViewModel" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        /// <param name="providers">The providers.</param>
        public BuildProvidersViewModel(IPlatformProvider platformProvider, IEnumerable<IBuildProviderViewModel> providers)
            : base(platformProvider)
        {
            Ensure.That(providers).IsNotNull();

            Providers = providers;
            DisplayName = Properties.Resources.BuildProviders_View;
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
