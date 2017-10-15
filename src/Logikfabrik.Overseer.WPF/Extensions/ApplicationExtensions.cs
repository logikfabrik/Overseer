// <copyright file="ApplicationExtensions.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Extensions
{
    using System.Diagnostics;
    using System.Windows;

    /// <summary>
    /// The <see cref="ApplicationExtensions" /> class.
    /// </summary>
    public static class ApplicationExtensions
    {
        /// <summary>
        /// Restarts the specified application.
        /// </summary>
        /// <param name="application">The application.</param>
        public static void Restart(this Application application)
        {
            Process.Start(Application.ResourceAssembly.Location);

            application.Shutdown();
        }
    }
}
