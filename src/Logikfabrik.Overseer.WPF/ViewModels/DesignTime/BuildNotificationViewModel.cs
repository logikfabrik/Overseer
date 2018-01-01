// <copyright file="BuildNotificationViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.DesignTime
{
    using System;
    using System.Windows;

    /// <summary>
    /// The <see cref="BuildNotificationViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class BuildNotificationViewModel : IBuildNotificationViewModel
    {
        /// <inheritdoc />
        public IBuildViewModel Build { get; set; } = new BuildViewModel();

        /// <inheritdoc />
        public void KeepOpen()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void StartClosing()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Close(RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
