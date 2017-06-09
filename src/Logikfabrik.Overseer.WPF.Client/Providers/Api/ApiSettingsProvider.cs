// <copyright file="ApiSettingsProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Providers.Api
{
    using EnsureThat;
    using Ninject.Activation;
    using Overseer.Api;

    public class ApiSettingsProvider : Provider<ApiSettings>
    {
        private readonly IAppSettingsFactory _appSettingsFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiSettingsProvider" /> class.
        /// </summary>
        /// <param name="appSettingsFactory">The app settings factory.</param>
        public ApiSettingsProvider(IAppSettingsFactory appSettingsFactory)
        {
            Ensure.That(appSettingsFactory).IsNotNull();

            _appSettingsFactory = appSettingsFactory;
        }

        /// <summary>
        /// Creates an instance within the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The created instance.
        /// </returns>
        protected override ApiSettings CreateInstance(IContext context)
        {
            var appSettings = _appSettingsFactory.Create();

            return new ApiSettings
            {
                ProxyUrl = appSettings.ProxyUrl,
                ProxyUsername = appSettings.ProxyUsername,
                ProxyPassword = appSettings.ProxyPassword
            };
        }
    }
}
