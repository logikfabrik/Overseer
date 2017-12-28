// <copyright file="ConnectionSettings.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI
{
    using System;
    using System.Xml.Serialization;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ConnectionSettings" /> class.
    /// </summary>
    [XmlType("TravisCI")]
    public class ConnectionSettings : Settings.ConnectionSettings
    {
        private string _gitHubToken;
        private string _enterpriseUrl;

        /// <inheritdoc/>
        public override Type ProviderType { get; } = typeof(BuildProvider);

        /// <summary>
        /// Gets or sets the GitHub token.
        /// </summary>
        /// <value>
        /// The GitHub token.
        /// </value>
        public string GitHubToken
        {
            get
            {
                return _gitHubToken;
            }

            set
            {
                Ensure.That(value).IsNotNullOrWhiteSpace();

                _gitHubToken = value;
            }
        }

        /// <summary>
        /// Gets or sets the subscription type.
        /// </summary>
        /// <value>
        /// The subscription type.
        /// </value>
        public SubscriptionType SubscriptionType { get; set; }

        /// <summary>
        /// Gets or sets the enterprise URL.
        /// </summary>
        /// <value>
        /// The enterprise URL.
        /// </value>
        public string EnterpriseUrl
        {
            get
            {
                return _enterpriseUrl;
            }

            set
            {
                Ensure.That(value).IsNotNullOrWhiteSpace();

                _enterpriseUrl = value;
            }
        }
    }
}
