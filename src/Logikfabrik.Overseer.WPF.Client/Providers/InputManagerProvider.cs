// <copyright file="InputManagerProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Providers
{
    using System.Windows.Input;
    using Ninject.Activation;

    /// <summary>
    /// The <see cref="InputManagerProvider" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class InputManagerProvider : Provider<InputManager>
    {
        /// <inheritdoc />
        protected override InputManager CreateInstance(IContext context)
        {
            return InputManager.Current;
        }
    }
}
