// <copyright file="ConnectionSettings.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.CircleCI
{
    using System;
    using System.Xml.Serialization;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ConnectionSettings" /> class.
    /// </summary>
    [XmlType("CircleCI")]

    // ReSharper disable once InheritdocConsiderUsage
    public class ConnectionSettings : Settings.ConnectionSettings
    {
        private string _version;
        private string _token;

        /// <inheritdoc />
        public override Type ProviderType { get; } = typeof(BuildProvider);

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
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token
        {
            get
            {
                return _token;
            }

            set
            {
                Ensure.That(value).IsNotNullOrWhiteSpace();

                _token = value;
            }
        }
    }
}
