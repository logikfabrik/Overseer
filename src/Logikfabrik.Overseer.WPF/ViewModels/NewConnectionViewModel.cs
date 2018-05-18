// <copyright file="NewConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.Collections.Generic;
    using Caliburn.Micro;
    using EnsureThat;
    using JetBrains.Annotations;

    /// <summary>
    /// The <see cref="NewConnectionViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class NewConnectionViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewConnectionViewModel" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        /// <param name="buildProviderViewModels">The buiild provider view models.</param>
        [UsedImplicitly]

        // ReSharper disable once InheritdocConsiderUsage
        public NewConnectionViewModel(IPlatformProvider platformProvider, IEnumerable<IBuildProviderViewModel> buildProviderViewModels)
            : base(platformProvider)
        {
            Ensure.That(buildProviderViewModels).IsNotNull();

            Providers = buildProviderViewModels;
            DisplayName = Properties.Resources.NewConnection_View;
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
