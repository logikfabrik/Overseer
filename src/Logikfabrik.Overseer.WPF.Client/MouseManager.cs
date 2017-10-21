// <copyright file="MouseManager.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client
{
    using System.Windows.Input;
    using EnsureThat;

    /// <summary>
    /// The <see cref="MouseManager" /> class.
    /// </summary>
    public class MouseManager : IMouseManager
    {
        private readonly InputManager _inputManager;

        public MouseManager(InputManager inputManager)
        {
            Ensure.That(inputManager).IsNotNull();

            _inputManager = inputManager;
        }

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