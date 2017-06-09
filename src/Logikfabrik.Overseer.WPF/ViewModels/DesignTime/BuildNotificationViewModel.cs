// <copyright file="BuildNotificationViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.DesignTime
{
    /// <summary>
    /// The <see cref="BuildNotificationViewModel" /> class.
    /// </summary>
    public class BuildNotificationViewModel : IBuildNotificationViewModel
    {
        /// <summary>
        /// Gets name.
        /// </summary>
        /// <value>
        /// The build name.
        /// </value>
        public string Name { get; } = "My build 1.0.0.0 (master)";

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; } = "My message";

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public BuildStatus? Status { get; } = BuildStatus.Succeeded;

        /// <summary>
        /// Views this instance.
        /// </summary>
        public void View()
        {
            // Method intentionally left empty.
        }
    }
}
