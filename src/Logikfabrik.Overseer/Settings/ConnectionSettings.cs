// <copyright file="ConnectionSettings.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
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
        private string[] _trackedProjects;
        private int _buildsPerProject;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettings" /> class.
        /// </summary>
        protected ConnectionSettings()
        {
            _id = Guid.NewGuid();
            _trackedProjects = new string[] { };
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
        /// Gets or sets the tracked projects.
        /// </summary>
        /// <value>
        /// The tracked projects.
        /// </value>
        public string[] TrackedProjects
        {
            get
            {
                return _trackedProjects;
            }

            set
            {
                Ensure.That(value).IsNotNull();

                _trackedProjects = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of builds per project.
        /// </summary>
        /// <value>
        /// The number of builds per project.
        /// </value>
        public int BuildsPerProject
        {
            get
            {
                return _buildsPerProject;
            }

            set
            {
                Ensure.That(value).IsGt(0);

                _buildsPerProject = value;
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