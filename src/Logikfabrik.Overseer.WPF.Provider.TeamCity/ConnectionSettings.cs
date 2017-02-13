// <copyright file="ConnectionSettings.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// The <see cref="ConnectionSettings" /> class.
    /// </summary>
    [XmlType("TeamCity")]
    public class ConnectionSettings : Settings.ConnectionSettings
    {
        /// <summary>
        /// Gets the provider type.
        /// </summary>
        /// <value>
        /// The provider type.
        /// </value>
        public override Type ProviderType { get; } = typeof(BuildProvider);

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the authentication type.
        /// </summary>
        /// <value>
        /// The authentication type.
        /// </value>
        public AuthenticationType AuthenticationType { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }
    }
}
