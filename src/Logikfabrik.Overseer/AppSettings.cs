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
    public class AppSettings : ApplicationSettingsBase
    {
        /// <summary>
        /// Gets or sets the interval in seconds.
        /// </summary>
        /// <value>
        /// The interval in seconds.
        /// </value>
        [UserScopedSetting]
        public int Interval
        {
            get { return (int)this["Interval"]; }
            set { this["Interval"] = value; }
        }

        /// <summary>
        /// Gets the expiration.
        /// </summary>
        /// <value>
        /// The expiration.
        /// </value>
        public int Expiration => Interval;

        /// <summary>
        /// Gets or sets the culture name.
        /// </summary>
        /// <value>
        /// The culture name.
        /// </value>
        [UserScopedSetting]
        public string CultureName
        {
            get { return (string)this["CultureName"]; }
            set { this["CultureName"] = value; }
        }
    }
}