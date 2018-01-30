// <copyright file="MouseManager.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System.Windows.Input;
    using EnsureThat;

    /// <summary>
    /// The <see cref="MouseManager" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class MouseManager : IMouseManager
    {
        private readonly InputManager _inputManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseManager" /> class.
        /// </summary>
        /// <param name="inputManager">The input manager.</param>
        public MouseManager(InputManager inputManager)
        {
            Ensure.That(inputManager).IsNotNull();

            _inputManager = inputManager;
        }

        /// <inheritdoc />
        public event PreProcessInputEventHandler PreProcessInput
        {
            add
            {
                _inputManager.PreProcessInput += value;
            }

            remove
            {
                _inputManager.PreProcessInput -= value;
            }
        }
    }
}