// <copyright file="ConnectionSettingsSignature.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System.Collections.Generic;
    using System.Reflection;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ConnectionSettingsSignature" /> class.
    /// </summary>
    public class ConnectionSettingsSignature
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsSignature" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public ConnectionSettingsSignature(ConnectionSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            Signature = GetSignature(settings) ?? 0;
        }

        /// <summary>
        /// Gets the signature.
        /// </summary>
        /// <value>
        /// The signature.
        /// </value>
        public int Signature { get; }

        private static int? GetSignature(ConnectionSettings settings)
        {
            var hash = 17;

            unchecked
            {
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (var property in GetProperties(settings))
                {
                    hash += 23 + (property.GetValue(settings)?.GetHashCode() ?? 0);
                }
            }

            return hash;
        }

        private static IEnumerable<PropertyInfo> GetProperties(ConnectionSettings settings)
        {
            return settings.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
    }
}
