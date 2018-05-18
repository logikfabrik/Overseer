// <copyright file="Favorite.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Favorites
{
    using System;
    using EnsureThat;

    /// <summary>
    /// The <see cref="Favorite" /> class.
    /// </summary>
    public class Favorite
    {
        private Guid _settingsId;
        private string _projectId;

        /// <summary>
        /// Gets or sets the settings identifier.
        /// </summary>
        /// <value>
        /// The settings identifier.
        /// </value>
        public Guid SettingsId
        {
            get
            {
                return _settingsId;
            }

            set
            {
                Ensure.That(value).IsNotEmpty();

                _settingsId = value;
            }
        }

        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        /// <value>
        /// The project identifier.
        /// </value>
        public string ProjectId
        {
            get
            {
                return _projectId;
            }

            set
            {
                Ensure.That(value).IsNotNullOrWhiteSpace();

                _projectId = value;
            }
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A clone of this instance.
        /// </returns>
        public virtual Favorite Clone()
        {
            return (Favorite)MemberwiseClone();
        }
    }
}
