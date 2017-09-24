// <copyright file="BuildNotificationViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.DesignTime
{
    using System.Windows;

    /// <summary>
    /// The <see cref="BuildNotificationViewModel" /> class.
    /// </summary>
    public class BuildNotificationViewModel : IBuildNotificationViewModel
    {
        /// <summary>
        /// Gets or sets the build.
        /// </summary>
        /// <value>
        /// The build.
        /// </value>
        public IBuildViewModel Build { get; set; }

        /// <summary>
        /// Opens the notification in the browser.
        /// </summary>
        public void ViewInBrowser()
        {
            // Method intentionally left empty.
        }

        /// <summary>
        /// Keeps the notification open.
        /// </summary>
        public void KeepOpen()
        {
            // Method intentionally left empty.
        }

        /// <summary>
        /// Starts closing the notification.
        /// </summary>
        public void StartClosing()
        {
            // Method intentionally left empty.
        }

        /// <summary>
        /// Closes the notification.
        /// </summary>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void Close(RoutedEventArgs e)
        {
            // Method intentionally left empty.
        }
    }
}
