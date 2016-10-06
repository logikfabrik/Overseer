// <copyright file="INotificationManager.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="INotificationManager" /> interface.
    /// </summary>
    public interface INotificationManager
    {
        /// <summary>
        /// Shows the notification.
        /// </summary>
        /// <typeparam name="T">The view model type.</typeparam>
        /// <param name="viewModel">The view model.</param>
        void ShowNotification<T>(T viewModel)
            where T : PropertyChangedBase;
    }
}