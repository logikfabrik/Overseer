// <copyright file="DisplaySetting.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System;
    using System.Windows;
    using Microsoft.Win32;

    /// <summary>
    /// The <see cref="DisplaySetting" /> class.
    /// </summary>
    public class DisplaySetting : IDisplaySetting
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisplaySetting" /> class.
        /// </summary>
        public DisplaySetting()
        {
            SystemEvents.DisplaySettingsChanged += (sender, args) =>
            {
                OnDisplaySettingsChanged(EventArgs.Empty);
            };
        }

        /// <inheritdoc/>
        public event EventHandler DisplaySettingsChanged;

        /// <inheritdoc/>
        public Rect WorkArea => SystemParameters.WorkArea;

        /// <summary>
        /// Raises the <see cref="DisplaySettingsChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnDisplaySettingsChanged(EventArgs e)
        {
            DisplaySettingsChanged?.Invoke(this, e);
        }
    }
}
