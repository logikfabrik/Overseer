// <copyright file="AppViewErrorLocalizer.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Localization
{
    using System;

    /// <summary>
    /// The <see cref="AppViewErrorLocalizer" /> class.
    /// </summary>
    public static class AppViewErrorLocalizer
    {
        /// <summary>
        /// Gets a localized UI message.
        /// </summary>
        /// <param name="exception">An exception.</param>
        /// <returns>A localized UI message.</returns>
        public static string Localize(Exception exception)
        {
            return Properties.Resources.App_Error_Standard;
        }
    }
}
