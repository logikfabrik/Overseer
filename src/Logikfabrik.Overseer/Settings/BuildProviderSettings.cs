// <copyright file="BuildProviderSettings.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System;

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
        /// <exception cref="ArgumentException">Thrown if <paramref name="value" /> is <c>null</c> or white space.</exception>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Value cannot be null or white space.", nameof(value));
                }

                _name = value;
            }
        }

        /// <summary>
        /// Gets or sets the provider type name.
        /// </summary>
        /// <value>
        /// The provider type name.
        /// </value>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value" /> is <c>null</c> or white space.</exception>
        public string ProviderTypeName
        {
            get
            {
                return _providerTypeName;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Value cannot be null or white space.", nameof(value));
                }

                _providerTypeName = value;
            }
        }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value" /> is <c>null</c>.</exception>
        public Setting[] Settings
        {
            get
            {
                return _settings;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Value cannot be null.");
                }

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
