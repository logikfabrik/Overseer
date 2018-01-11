// <copyright file="BuildProviderViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.ViewModels
{
    using Caliburn.Micro;

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
        // ReSharper disable once InheritdocConsiderUsage
        public BuildProviderViewModel(IEventAggregator eventAggregator)
            : base(eventAggregator, "Visual Studio Team Services")
        {
        }
    }
}
