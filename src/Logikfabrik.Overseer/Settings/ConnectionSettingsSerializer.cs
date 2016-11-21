// <copyright file="ConnectionSettingsSerializer.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System;
    using System.IO;
    using System.Xml.Serialization;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ConnectionSettingsSerializer" /> class.
    /// </summary>
    public class ConnectionSettingsSerializer : IConnectionSettingsSerializer
    {
        private readonly Type[] _supportedTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsSerializer" /> class.
        /// </summary>
        /// <param name="supportedTypes">The supported types.</param>
        public ConnectionSettingsSerializer(Type[] supportedTypes)
        {
            Ensure.That(supportedTypes).IsNotNull();

            _supportedTypes = supportedTypes;
        }

        /// <summary>
        /// Deserializes the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// The deserialized settings.
        /// </returns>
        public ConnectionSettings[] Deserialize(string settings)
        {
            Ensure.That(settings).IsNotNullOrWhiteSpace();

            using (var reader = new StringReader(settings))
            {
                var serializer = new XmlSerializer(typeof(ConnectionSettings[]), _supportedTypes);

                return (ConnectionSettings[])serializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// Serializes the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// The serialized settings.
        /// </returns>
        public string Serialize(ConnectionSettings[] settings)
        {
            Ensure.That(settings).IsNotNull();

            using (var writer = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(ConnectionSettings[]), _supportedTypes);

                serializer.Serialize(writer, settings);

                return writer.ToString();
            }
        }
    }
}
