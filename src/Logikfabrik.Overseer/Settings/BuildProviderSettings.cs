﻿// <copyright file="BuildProviderSettings.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System;
    using System.Linq;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildProvider" /> class.
    /// </summary>
    public class BuildProviderSettings
    {
        private Guid _id;
        private string _name;
        private string _buildProviderTypeName;
        private Setting[] _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderSettings" /> class.
        /// </summary>
        public BuildProviderSettings()
        {
            _id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id
        {
            get
            {
                return _id;
            }

            set
            {
                Ensure.That(value).IsNotEmpty();

                _id = value;
            }
        }

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
        public string BuildProviderTypeName
        {
            get
            {
                return _buildProviderTypeName;
            }

            set
            {
                Ensure.That(value).IsNotNullOrWhiteSpace();

                _buildProviderTypeName = value;
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
        /// Gets the build provider type.
        /// </summary>
        /// <returns>The build provider type.</returns>
        public Type GetBuildProviderType()
        {
            return string.IsNullOrWhiteSpace(_buildProviderTypeName)
                ? null
                : Type.GetType(_buildProviderTypeName, false);
        }

        /// <summary>
        /// Gets the setting with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The setting.</returns>
        public string GetSetting(string name)
        {
            Ensure.That(name).IsNotNullOrWhiteSpace();

            return _settings.First(setting => setting.Name == name).Value;
        }
    }
}
