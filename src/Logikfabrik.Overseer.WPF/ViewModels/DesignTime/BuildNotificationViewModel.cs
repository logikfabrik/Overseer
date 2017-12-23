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
        /// <inheritdoc />
        public IBuildViewModel Build { get; set; } = new BuildViewModel();

        /// <inheritdoc />
        public void KeepOpen()
        {
        }

        /// <inheritdoc />
        public void StartClosing()
        {
        }

        /// <inheritdoc />
        public void Close(RoutedEventArgs e)
        {
        }
    }
}
