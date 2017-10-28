// <copyright file="BuildNotificationConfigurator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System.Linq;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildNotificationConfigurator" /> class.
    /// </summary>
    public static class BuildNotificationConfigurator
    {
        /// <summary>
        /// Configures build notification.
        /// </summary>
        /// <param name="buildTracker">The build tracker.</param>
        /// <param name="buildNotificationManager">The build notification manager.</param>
        public static void Configure(IBuildTracker buildTracker, IBuildNotificationManager buildNotificationManager)
        {
            Ensure.That(buildTracker).IsNotNull();
            Ensure.That(buildNotificationManager).IsNotNull();

            buildTracker.ProjectProgressChanged += (sender, e) =>
            {
                if (!e.Builds.Any())
                {
                    return;
                }

                foreach (var build in e.Builds)
                {
                    buildNotificationManager.ShowNotification(e.Project, build);
                }
            };
        }
    }
}
