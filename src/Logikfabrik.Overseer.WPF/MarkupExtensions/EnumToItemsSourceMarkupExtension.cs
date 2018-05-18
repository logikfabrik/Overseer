// <copyright file="EnumToItemsSourceMarkupExtension.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.MarkupExtensions
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Markup;
    using EnsureThat;
    using JetBrains.Annotations;

    /// <summary>
    /// The <see cref="EnumToItemsSourceMarkupExtension" /> class.
    /// </summary>
    /// <remarks>
    /// Based on SO https://stackoverflow.com/a/20919656, answered by Rohit Vats, https://stackoverflow.com/users/632337/rohit-vats.
    /// </remarks>
    // ReSharper disable once InheritdocConsiderUsage
    public class EnumToItemsSourceMarkupExtension : MarkupExtension
    {
        private readonly Type _enumType;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumToItemsSourceMarkupExtension" /> class.
        /// </summary>
        /// <param name="enumType">The type.</param>
        [UsedImplicitly]

        // ReSharper disable once InheritdocConsiderUsage
        public EnumToItemsSourceMarkupExtension(Type enumType)
        {
            Ensure.That(enumType).IsNotNull();
            Ensure.That(() => enumType.IsEnum, nameof(enumType)).IsTrue();

            _enumType = enumType;
        }

        /// <inheritdoc />
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            Func<object, string> getDescription = value =>
            {
                var field = _enumType.GetField(value.ToString());
                var attribute = field.GetCustomAttribute<DescriptionAttribute>();

                return string.IsNullOrWhiteSpace(attribute?.Description) ? value.ToString() : attribute.Description;
            };

            return Enum.GetValues(_enumType).Cast<object>().Select(value => new { Value = value, DisplayName = getDescription(value) }).ToArray();
        }
    }
}
