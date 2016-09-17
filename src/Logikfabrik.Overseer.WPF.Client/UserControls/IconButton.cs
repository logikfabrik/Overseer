// <copyright file="IconButton.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.UserControls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// The <see cref="IconButton" /> class.
    /// </summary>
    public class IconButton : Button
    {
        /// <summary>
        /// The data property.
        /// </summary>
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(Geometry), typeof(IconButton));

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public Geometry Data
        {
            get
            {
                return (Geometry)GetValue(DataProperty);
            }

            set
            {
                SetValue(DataProperty, value);
            }
        }
    }
}
