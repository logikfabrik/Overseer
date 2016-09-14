// <copyright file="BuildProviderSettings.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildProvider" /> class.
    /// </summary>
    public class BuildProviderSettings
    {
        private string _name;
        private string _providerTypeName;
        private Setting[] _settings;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                Ensure.That(value).IsNotNullOrWhiteSpace();

                _name = value;
            }
        }

        /// <summary>
        /// Gets or sets the provider type name.
        /// </summary>
        /// <value>
        /// The provider type name.
        /// </value>
        public string ProviderTypeName
        {
            get
            {
                return _providerTypeName;
            }

            set
            {
                Ensure.That(value).IsNotNullOrWhiteSpace();

                _providerTypeName = value;
            }
        }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public Setting[] Settings
        {
            get
            {
                return _settings;
            }

            set
            {
                Ensure.That(value).IsNotNull();

                _settings = value;
            }
        }

        /// <summary>
        /// Gets the provider type.
        /// </summary>
        /// <returns>The provider type.</returns>
        public Type GetProviderType()
        {
            return string.IsNullOrWhiteSpace(_providerTypeName)
                ? null
                : Type.GetType(_providerTypeName, false);
        }
    }
}
