// <copyright file="ConnectionSettings.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor
{
    using System;
    using System.Xml.Serialization;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ConnectionSettings" /> class.
    /// </summary>
    [XmlType("AppVeyor")]

    // ReSharper disable once InheritdocConsiderUsage
    public class ConnectionSettings : Settings.ConnectionSettings
    {
        private string _token;

        /// <inheritdoc />
        public override Type ProviderType { get; } = typeof(BuildProvider);

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
