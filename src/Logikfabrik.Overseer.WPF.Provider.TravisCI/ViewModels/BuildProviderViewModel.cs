// <copyright file="BuildProviderViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI.ViewModels
{
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="BuildProviderViewModel" /> class.
    /// </summary>
    public class BuildProviderViewModel : WPF.ViewModels.BuildProviderViewModel<ConnectionSettings, ConnectionSettingsViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public BuildProviderViewModel(IEventAggregator eventAggregator)
            : base(eventAggregator, "Travis CI")
        {
        }
    }
}
