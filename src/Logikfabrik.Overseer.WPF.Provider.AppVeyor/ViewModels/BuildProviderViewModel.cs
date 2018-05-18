// <copyright file="BuildProviderViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.ViewModels
{
    using Caliburn.Micro;
    using JetBrains.Annotations;
    using Navigation;

    /// <summary>
    /// The <see cref="BuildProviderViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class BuildProviderViewModel : WPF.ViewModels.BuildProviderViewModel<ConnectionSettings, EditConnectionSettingsViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="navigationMessageFactory">The navigation message factory.</param>
        [UsedImplicitly]

        // ReSharper disable once InheritdocConsiderUsage
        public BuildProviderViewModel(IEventAggregator eventAggregator, INavigationMessageFactory<WPF.ViewModels.AddConnectionViewModel<ConnectionSettings, EditConnectionSettingsViewModel>> navigationMessageFactory)
            : base(eventAggregator, navigationMessageFactory, "AppVeyor")
        {
        }
    }
}
