// <copyright file="LanguageConfigurator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System.Globalization;
    using System.Threading;
    using EnsureThat;

    /// <summary>
    /// The <see cref="LanguageConfigurator" /> class.
    /// </summary>
    public static class LanguageConfigurator
    {
        /// <summary>
        /// Configures language.
        /// </summary>
        /// <param name="appSettingsFactory">The app settings factory.</param>
        public static void Configure(IAppSettingsFactory appSettingsFactory)
        {
            Ensure.That(appSettingsFactory).IsNotNull();

            Configure(appSettingsFactory.Create());
        }

        /// <summary>
        /// Configures language.
        /// </summary>
        /// <param name="appSettings">The application settings.</param>
        public static void Configure(AppSettings appSettings)
        {
            Ensure.That(appSettings).IsNotNull();

            var culture = CultureInfo.GetCultureInfo(appSettings.CultureName);

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }
    }
}