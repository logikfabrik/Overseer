// <copyright file="IBuildNotificationViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.Windows;

    /// <summary>
    /// The <see cref="IBuildNotificationViewModel" /> interface.
    /// </summary>
    public interface IBuildNotificationViewModel
    {
        /// <summary>
        /// Gets the build.
        /// </summary>
        /// <value>
        /// The build.
        /// </value>
        IBuildViewModel Build { get; }

        /// <summary>
        /// Keeps this instance open.
        /// </summary>
        void KeepOpen();

        /// <summary>
        /// Starts closing this instance.
        /// </summary>
        void StartClosing();

        /// <summary>
        /// Closes this instance.
        /// </summary>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        void Close(RoutedEventArgs e);
    }
}
