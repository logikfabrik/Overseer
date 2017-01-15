﻿// <copyright file="AppSettings.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System.Configuration;

    /// <summary>
    /// The <see cref="AppSettings" /> class.
    /// </summary>
    public class AppSettings : ApplicationSettingsBase
    {
        /// <summary>
        /// Gets or sets the proxy URL.
        /// </summary>
        /// <value>
        /// The proxy URL.
        /// </value>
        [UserScopedSettingAttribute]
        public string ProxyUrl
        {
            get { return (string)this["ProxyUrl"]; }
            set { this["ProxyUrl"] = value; }
        }

        /// <summary>
        /// Gets or sets the proxy username.
        /// </summary>
        /// <value>
        /// The proxy username.
        /// </value>
        [UserScopedSettingAttribute]
        public string ProxyUsername
        {
            get { return (string)this["ProxyUsername"]; }
            set { this["ProxyUsername"] = value; }
        }

        /// <summary>
        /// Gets or sets the proxy password.
        /// </summary>
        /// <value>
        /// The proxy password.
        /// </value>
        [UserScopedSettingAttribute]
        public string ProxyPassword
        {
            get { return (string)this["ProxyPassword"]; }
            set { this["ProxyPassword"] = value; }
        }
    }
}