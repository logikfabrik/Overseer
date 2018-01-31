// <copyright file="ScrollToTopBehavior.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Behaviors
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interactivity;

    /// <summary>
    /// The <see cref="ScrollToTopBehavior" /> class.
    /// </summary>
    public class ScrollToTopBehavior : Behavior<ScrollViewer>
    {
        /// <inheritdoc />
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        /// <inheritdoc/>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (AssociatedObject == null)
            {
                return;
            }

            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            var contentControl = AssociatedObject.Content as ContentControl;

            if (contentControl == null)
            {
                return;
            }

            var descriptor = DependencyPropertyDescriptor.FromProperty(Caliburn.Micro.View.ModelProperty, typeof(ContentControl));

            descriptor?.AddValueChanged(contentControl, OnModelChanged);
        }

        private void OnModelChanged(object sender, EventArgs e)
        {
            AssociatedObject.ScrollToTop();
        }
    }
}