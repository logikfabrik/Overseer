// <copyright file="FavoritesSerializer.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Favorites
{
    using System.IO;
    using System.Xml.Serialization;
    using EnsureThat;

    /// <summary>
    /// The <see cref="FavoritesSerializer" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class FavoritesSerializer : IFavoritesSerializer
    {
        /// <inheritdoc />
        public Favorite[] Deserialize(string favorites)
        {
            Ensure.That(favorites).IsNotNullOrWhiteSpace();

            using (var reader = new StringReader(favorites))
            {
                var serializer = new XmlSerializer(typeof(Favorite[]));

                return (Favorite[])serializer.Deserialize(reader);
            }
        }

        /// <inheritdoc />
        public string Serialize(Favorite[] favorites)
        {
            Ensure.That(favorites).IsNotNull();

            using (var writer = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(Favorite[]));

                serializer.Serialize(writer, favorites);

                return writer.ToString();
            }
        }
    }
}
