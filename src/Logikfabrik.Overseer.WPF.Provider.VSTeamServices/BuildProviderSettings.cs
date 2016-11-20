// <copyright file="BuildProviderSettings.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices
{
    using System;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildProviderSettings" /> class.
    /// </summary>
    public class BuildProviderSettings : Settings.BuildProviderSettings
    {
        private string _token;
        private string _url;

        /// <summary>
        /// Gets the build provider type.
        /// </summary>
        /// <value>
        /// The build provider type.
        /// </value>
        public override Type BuildProviderType { get; } = typeof(BuildProvider);

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
