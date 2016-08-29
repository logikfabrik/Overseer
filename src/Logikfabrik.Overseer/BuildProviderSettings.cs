// <copyright file="BuildProviderSettings.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="BuildProvider" /> class.
    /// </summary>
    public class BuildProviderSettings
    {
        private string _name;
        private Type _providerType;

        public BuildProviderSettings(string name, Type providerType)
        {
            Name = name;
            ProviderType = providerType;
            Settings = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        /// <exception cref="ArgumentException">Thrown if name is <c>null</c> or white space.</exception>
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
                    throw new ArgumentException("Name cannot be null or white space.", nameof(value));
                }

                _name = value;
            }
        }

        public Type ProviderType
        {
            get
            {
                return _providerType;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Provider type cannot be null.");
                }
            }
        }

        public IDictionary<string, string> Settings { get; private set; }
    }
}
