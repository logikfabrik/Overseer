// <copyright file="IViewNotificationViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.Windows;

    /// <summary>
    /// The <see cref="IViewNotificationViewModel" /> interface.
    /// </summary>
    public interface IViewNotificationViewModel
    {
        /// <summary>
        /// Gets the build.
        /// </summary>
        /// <value>
        /// The build.
        /// </value>
        IViewBuildViewModel Build { get; }

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
