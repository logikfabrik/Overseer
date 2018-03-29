// <copyright file="BuildTrackerSettings.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System.Configuration;

    /// <summary>
    /// The <see cref="BuildTrackerSettings" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class BuildTrackerSettings : ApplicationSettingsBase, IBuildTrackerSettings
    {
        /// <inheritdoc />
        [UserScopedSetting]
        public int Interval
        {
            get { return (int)this["Interval"]; }
            set { this["Interval"] = value; }
        }

        /// <inheritdoc />
        public int Expiration => Interval;
    }
}
