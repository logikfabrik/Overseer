// <copyright file="ConnectionSettingsRepositoryProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Providers.Settings
{
    using System.Security.Cryptography;
    using EnsureThat;
    using Ninject.Activation;
    using Overseer.Logging;
    using Overseer.Settings;

    /// <summary>
    /// The <see cref="ConnectionSettingsRepositoryProvider" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ConnectionSettingsRepositoryProvider : Provider<IConnectionSettingsRepository>
    {
        private readonly ILogService _logService;
        private readonly IConnectionSettingsStore _settingsStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsRepositoryProvider"/> class.
        /// </summary>
        /// <param name="logService">The log service.</param>
        /// <param name="settingsStore">The settings store.</param>
        public ConnectionSettingsRepositoryProvider(ILogService logService, IConnectionSettingsStore settingsStore)
        {
            Ensure.That(logService).IsNotNull();
            Ensure.That(settingsStore).IsNotNull();

            _logService = logService;
            _settingsStore = settingsStore;
        }

        /// <inheritdoc />
        protected override IConnectionSettingsRepository CreateInstance(IContext context)
        {
            try
            {
                return new ConnectionSettingsRepository(_settingsStore);
            }
            catch (CryptographicException ex)
            {
                _logService.Log(GetType(), new LogEntry(LogEntryType.Error, "An error occurred while decrypting the connection settings. It's possible the passphrase used by the program doesn't match that of the file.", ex));

                throw;
            }
        }
    }
}
