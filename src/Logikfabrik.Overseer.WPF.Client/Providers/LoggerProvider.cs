// <copyright file="LoggerProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Providers
{
    using Ninject.Activation;
    using Serilog;

    /// <summary>
    /// The <see cref="LoggerProvider" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class LoggerProvider : Provider<ILogger>
    {
        /// <inheritdoc />
        protected override ILogger CreateInstance(IContext context)
        {
            return new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .WriteTo.Console()
                .WriteTo.RollingFile("log-{Date}.txt")
                .CreateLogger();
        }
    }
}
