// <copyright file="AppSettings.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System.Configuration;

    /// <summary>
    /// The <see cref="AppSettings" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class AppSettings : ApplicationSettingsBase, IAppSettings
    {
        /// <inheritdoc />
        [UserScopedSetting]
        public string CultureName
        {
            get { return (string)this["CultureName"]; }
            set { this["CultureName"] = value; }
        }

        /// <inheritdoc />
        [UserScopedSetting]
        public bool ShowNotifications
        {
            get { return (bool)this["ShowNotifications"]; }
            set { this["ShowNotifications"] = value; }
        }
    }
}