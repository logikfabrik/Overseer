﻿// <copyright file="ConnectionSettings.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.Codeship
{
    using System;
    using System.Xml.Serialization;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ConnectionSettings" /> class.
    /// </summary>
    [XmlType("Codeship")]

    // ReSharper disable once InheritdocConsiderUsage
    public class ConnectionSettings : Settings.ConnectionSettings
    {
        private string _username;
        private string _password;

        /// <inheritdoc />
        public override Type ProviderType { get; } = typeof(BuildProvider);

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                Ensure.That(value).IsNotNullOrWhiteSpace();

                _username = value;
            }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                Ensure.That(value).IsNotNullOrWhiteSpace();

                _password = value;
            }
        }
    }
}
