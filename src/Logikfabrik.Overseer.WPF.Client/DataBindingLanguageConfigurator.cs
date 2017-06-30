// <copyright file="DataBindingLanguageConfigurator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Markup;

    /// <summary>
    /// The <see cref="DataBindingLanguageConfigurator" /> class.
    /// </summary>
    /// <remarks>
    /// Based on SO https://stackoverflow.com/a/520334, answered by loraderon, https://stackoverflow.com/users/22092/loraderon.
    /// </remarks>
    public static class DataBindingLanguageConfigurator
    {
        /// <summary>
        /// Configures data binding.
        /// </summary>
        public static void Configure()
        {
            var language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.Name);

            FrameworkContentElement.LanguageProperty.OverrideMetadata(typeof(TextElement), new FrameworkPropertyMetadata(language));
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(language));
        }
    }
}
