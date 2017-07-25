// <copyright file="InputManagerProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Providers
{
    using System.Windows.Input;
    using Ninject.Activation;

    /// <summary>
    /// The <see cref="InputManagerProvider" /> class.
    /// </summary>
    public class InputManagerProvider : Provider<InputManager>
    {
        /// <summary>
        /// Creates an instance within the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The created instance.
        /// </returns>
        protected override InputManager CreateInstance(IContext context)
        {
            return InputManager.Current;
        }
    }
}
