// <copyright file="BuildProviderViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.ViewModels
{
    using System;
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="BuildProviderViewModel" /> class.
    /// </summary>
    public class BuildProviderViewModel : WPF.ViewModels.BuildProviderViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public BuildProviderViewModel(IEventAggregator eventAggregator)
            : base(eventAggregator)
        {
        }

        /// <summary>
        /// Gets the provider name.
        /// </summary>
        /// <value>
        /// The provider name.
        /// </value>
        public override string ProviderName { get; } = "TeamCity";

        /// <summary>
        /// Gets the type of the view model to add a connection.
        /// </summary>
        /// <value>
        /// The type of the view model to add a connection.
        /// </value>
        protected override Type AddConnectionViewModelType { get; } = typeof(AddConnectionViewModel);
    }
}
