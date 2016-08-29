// <copyright file="AddConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor.ViewModels
{
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="AddConnectionViewModel" /> class.
    /// </summary>
    /// <seealso cref="WPF.ViewModels.AddConnectionViewModel" />
    public class AddConnectionViewModel : WPF.ViewModels.AddConnectionViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddConnectionViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public AddConnectionViewModel(IEventAggregator eventAggregator)
            : base(eventAggregator)
        {
        }
    }
}
