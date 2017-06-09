// <copyright file="IBuildNotificationViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
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
        /// Views this instance.
        /// </summary>
        void View();
    }
}
