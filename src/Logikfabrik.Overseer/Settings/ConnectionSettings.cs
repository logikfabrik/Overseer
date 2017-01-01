// <copyright file="ConnectionSettings.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ConnectionSettings" /> class.
    /// </summary>
    public abstract class ConnectionSettings : IEquatable<ConnectionSettings>
    {
        private readonly Lazy<IEnumerable<PropertyInfo>> _properties;
        private Guid _id;
        private string _name;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettings" /> class.
        /// </summary>
        protected ConnectionSettings()
        {
            _id = Guid.NewGuid();
            _properties = new Lazy<IEnumerable<PropertyInfo>>(GetProperties);
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

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as ConnectionSettings);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            var values = _properties.Value.Select(property => property.GetValue(this, null)).ToArray();

            return GetHashCode(values);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <c>true</c> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(ConnectionSettings other)
        {
            var type = GetType();

            if (other == null || other.GetType() != type)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var property in _properties.Value)
            {
                var thisValue = property.GetValue(this, null);
                var otherValue = property.GetValue(other, null);

                if (thisValue != otherValue && (thisValue == null || !thisValue.Equals(otherValue)))
                {
                    return false;
                }
            }

            return true;
        }

        private static int GetHashCode(params object[] args)
        {
            if (args == null)
            {
                return 0;
            }

            var hash = 42;

            unchecked
            {
                foreach (var arg in args)
                {
                    if (ReferenceEquals(arg, null))
                    {
                        continue;
                    }

                    if (arg.GetType().IsArray)
                    {
                        hash = ((IEnumerable)arg).Cast<object>().Aggregate(hash, (source, accumulate) => (source * 37) + GetHashCode(accumulate));
                    }
                    else
                    {
                        hash = (hash * 37) + arg.GetHashCode();
                    }
                }
            }

            return hash;
        }

        private IEnumerable<PropertyInfo> GetProperties()
        {
            return GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
    }
}