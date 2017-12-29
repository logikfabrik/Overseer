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
        private string _url;

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
    }
}
