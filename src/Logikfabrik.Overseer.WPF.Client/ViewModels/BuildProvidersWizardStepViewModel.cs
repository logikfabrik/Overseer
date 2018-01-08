// <copyright file="BuildProvidersWizardStepViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using System.Collections.Generic;
    using EnsureThat;
    using WPF.ViewModels;

    /// <summary>
    /// The <see cref="BuildProvidersWizardStepViewModel" /> class.
    /// </summary>
    public class BuildProvidersWizardStepViewModel : IWizardStepViewModel
    {
        public BuildProvidersWizardStepViewModel(IEnumerable<IBuildProviderViewModel> providers)
        {
            Ensure.That(providers).IsNotNull();

            Providers = providers;
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
