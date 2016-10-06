// <copyright file="BuildNotificationViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildNotificationViewModel" /> class.
    /// </summary>
    public class BuildNotificationViewModel : PropertyChangedBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildNotificationViewModel" /> class.
        /// </summary>
        /// <param name="build">The build.</param>
        public BuildNotificationViewModel(IBuild build)
        {
            Ensure.That(build).IsNotNull();

            BuildViewModel = new BuildViewModel(build);
        }

        /// <summary>
        /// Gets the build view model.
        /// </summary>
        /// <value>
        /// The build view model.
        /// </value>
        public BuildViewModel BuildViewModel { get; }
    }
}
