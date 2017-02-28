// <copyright file="ConnectionSettings.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ConnectionSettings" /> class.
    /// </summary>
    public abstract class ConnectionSettings
    {
        private Guid _id;
        private string _name;
        private string[] _projectsToMonitor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettings" /> class.
        /// </summary>
        protected ConnectionSettings()
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
        /// Gets or sets the projects to monitor.
        /// </summary>
        /// <value>
        /// The projects to monitor.
        /// </value>
        public string[] ProjectsToMonitor
        {
            get
            {
                return _projectsToMonitor;
            }

            set
            {
                Ensure.That(value).IsNotNull();

                _projectsToMonitor = value;
            }
        }

        /// <summary>
        /// Gets the provider type.
        /// </summary>
        /// <value>
        /// The provider type.
        /// </value>
        public abstract Type ProviderType { get; }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A clone of this instance.
        /// </returns>
        public virtual ConnectionSettings Clone()
        {
            return (ConnectionSettings)MemberwiseClone();
        }
    }
}