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
    public abstract class BuildProviderSettings
    {
        private Guid _id;
        private string _name;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderSettings" /> class.
        /// </summary>
        protected BuildProviderSettings()
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
        /// Gets the build provider type.
        /// </summary>
        /// <value>
        /// The build provider type.
        /// </value>
        public abstract Type BuildProviderType { get; }
    }
}
