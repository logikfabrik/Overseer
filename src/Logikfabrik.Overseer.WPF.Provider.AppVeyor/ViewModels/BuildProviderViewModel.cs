// <copyright file="BuildProviderViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.ViewModels
{
    using System;
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="BuildProviderViewModel" /> class.
    /// </summary>
    /// <seealso cref="WPF.ViewModels.BuildProviderViewModel" />
    public class BuildProviderViewModel : WPF.ViewModels.BuildProviderViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public BuildProviderViewModel(IEventAggregator eventAggregator)
            : base(eventAggregator, new BuildProvider())
        {
            AddConnectionViewModel = typeof(AddConnectionViewModel);
        }

        /// <summary>
        /// Gets the type of the view model to add connection.
        /// </summary>
        /// <value>
        /// The type of the view model to add connection.
        /// </value>
        protected override Type AddConnectionViewModel { get; }
    }
}
