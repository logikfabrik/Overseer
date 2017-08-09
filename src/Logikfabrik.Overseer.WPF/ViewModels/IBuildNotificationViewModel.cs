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
        /// Gets name.
        /// </summary>
        /// <value>
        /// The build name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        string Message { get; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        BuildStatus? Status { get; }

        /// <summary>
        /// Opens the notification in the browser.
        /// </summary>
        void ViewInBrowser();

        /// <summary>
        /// Keeps the notification open.
        /// </summary>
        void KeepOpen();

        /// <summary>
        /// Starts closing the notification.
        /// </summary>
        void StartClosing();

        /// <summary>
        /// Closes the notification.
        /// </summary>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        void Close(RoutedEventArgs e);
    }
}
