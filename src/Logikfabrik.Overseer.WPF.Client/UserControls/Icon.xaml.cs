// <copyright file="Icon.xaml.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.UserControls
{
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// The <see cref="Icon" /> class.
    /// </summary>
    public partial class Icon
    {
        /// <summary>
        /// The data property.
        /// </summary>
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(Geometry), typeof(Icon));

        /// <summary>
        /// Initializes a new instance of the <see cref="Icon" /> class.
        /// </summary>
        public Icon()
        {
            InitializeComponent();
        }

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
