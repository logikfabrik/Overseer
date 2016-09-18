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
        /// The icon data property.
        /// </summary>
        public static readonly DependencyProperty IconDataProperty = DependencyProperty.Register("IconData", typeof(Geometry), typeof(IconButton));

        /// <summary>
        /// The icon width property.
        /// </summary>
        public static readonly DependencyProperty IconWidthProperty = DependencyProperty.Register("IconWidth", typeof(double), typeof(IconButton), new UIPropertyMetadata((double)10));

        /// <summary>
        /// The icon height property.
        /// </summary>
        public static readonly DependencyProperty IconHeightProperty = DependencyProperty.Register("IconHeight", typeof(double), typeof(IconButton), new UIPropertyMetadata((double)10));

        /// <summary>
        /// Gets or sets the icon data.
        /// </summary>
        /// <value>
        /// The icon data.
        /// </value>
        public Geometry IconData
        {
            get
            {
                return (Geometry)GetValue(IconDataProperty);
            }

            set
            {
                SetValue(IconDataProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the icon width.
        /// </summary>
        /// <value>
        /// The icon width.
        /// </value>
        public double IconWidth
        {
            get
            {
                return (double)GetValue(IconWidthProperty);
            }

            set
            {
                SetValue(IconWidthProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the icon height.
        /// </summary>
        /// <value>
        /// The icon height.
        /// </value>
        public double IconHeight
        {
            get
            {
                return (double)GetValue(IconHeightProperty);
            }

            set
            {
                SetValue(IconHeightProperty, value);
            }
        }
    }
}
