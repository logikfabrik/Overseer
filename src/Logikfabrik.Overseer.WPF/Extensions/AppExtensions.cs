// <copyright file="AppExtensions.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Extensions
{
    using System.Diagnostics;
    using System.Windows;

    /// <summary>
    /// The <see cref="AppExtensions" /> class.
    /// </summary>
    public static class AppExtensions
    {
        /// <summary>
        /// Restarts the specified application.
        /// </summary>
        /// <param name="application">The application.</param>
        public static void Restart(this IApp application)
        {
            Process.Start(Application.ResourceAssembly.Location);

            application.Shutdown();
        }
    }
}
