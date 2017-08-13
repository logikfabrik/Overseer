// <copyright file="ProgressControl.xaml.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Views.UserControls
{
    using System.Windows;

    /// <summary>
    /// The <see cref="ProgressControl" /> class.
    /// </summary>
#pragma warning disable S110 // Inheritance tree of classes should not be too deep
    public partial class ProgressControl
#pragma warning restore S110 // Inheritance tree of classes should not be too deep
    {
        /// <summary>
        /// The is errored dependency property.
        /// </summary>
        public static readonly DependencyProperty IsErroredProperty = DependencyProperty.Register(nameof(IsErrored), typeof(bool), typeof(ProgressControl));

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressControl" /> class.
        /// </summary>
        public ProgressControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is errored.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is errored; otherwise, <c>false</c>.
        /// </value>
        public bool IsErrored
        {
            get
            {
                return (bool)GetValue(IsErroredProperty);
            }

            set
            {
                SetValue(IsErroredProperty, value);
            }
        }
    }
}
