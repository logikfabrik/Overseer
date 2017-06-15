// <copyright file="EnumToItemsSourceMarkupExtension.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.MarkupExtensions
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Markup;
    using EnsureThat;

    /// <summary>
    /// The <see cref="EnumToItemsSourceMarkupExtension" /> class.
    /// </summary>
    public class EnumToItemsSourceMarkupExtension : MarkupExtension
    {
        private readonly Type _enumType;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumToItemsSourceMarkupExtension" /> class.
        /// </summary>
        /// <param name="enumType">The type.</param>
        public EnumToItemsSourceMarkupExtension(Type enumType)
        {
            Ensure.That(enumType).IsNotNull();
            Ensure.That(() => enumType.IsEnum, nameof(enumType)).IsTrue();

            _enumType = enumType;
        }

        /// <summary>
        /// Returns an object that is provided as the value of the target property for this markup extension.
        /// </summary>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
        /// <returns>
        /// The object value to set on the property where the extension is applied.
        /// </returns>
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
