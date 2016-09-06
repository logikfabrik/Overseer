// <copyright file="BuildProvidersViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="BuildProvidersViewModel" /> class.
    /// </summary>
    public class BuildProvidersViewModel : PropertyChangedBase
    {
        public BuildProvidersViewModel(IEnumerable<BuildProviderViewModel> buildProviderViewModels)
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
