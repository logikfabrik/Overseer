// <copyright file="ConnectionSettings.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity
{
    using System;
    using System.Xml.Serialization;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ConnectionSettings" /> class.
    /// </summary>
    [XmlType("TeamCity")]

    // ReSharper disable once InheritdocConsiderUsage
    public class ConnectionSettings : Settings.ConnectionSettings
    {
        private string _url;
        private string _version;

        /// <inheritdoc />
        public override Type ProviderType { get; } = typeof(BuildProvider);

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url
        {
            get
            {
                return _url;
            }

            set
            {
                Ensure.That(value).IsNotNullOrWhiteSpace();

                _url = value;
            }
        }

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
        public string Version
        {
            get
            {
                return _version;
            }

            set
            {
                Ensure.That(value).IsNotNullOrWhiteSpace();

                _version = value;
            }
        }

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
