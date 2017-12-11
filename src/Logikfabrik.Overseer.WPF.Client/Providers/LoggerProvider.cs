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
    public class LoggerProvider : Provider<ILogger>
    {
        /// <summary>
        /// Creates an instance within the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The created instance.
        /// </returns>
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
