// <copyright file="HyperlinkExtensions.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Extensions
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Navigation;

    /// <summary>
    /// The <see cref="HyperlinkExtensions" /> class.
    /// </summary>
    public static class HyperlinkExtensions
    {
        /// <summary>
        /// The is external property.
        /// </summary>
        public static readonly DependencyProperty IsExternalProperty = DependencyProperty.RegisterAttached("IsExternal", typeof(bool), typeof(HyperlinkExtensions), new UIPropertyMetadata(false, OnIsExternalChanged));

        /// <summary>
        /// Gets whether the <see cref="Hyperlink" /> is external.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns><c>true</c> if external; otherwise <c>false</c>.</returns>
        public static bool GetIsExternal(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsExternalProperty);
        }

        /// <summary>
        /// Sets whether the <see cref="Hyperlink" /> is external.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetIsExternal(DependencyObject obj, bool value)
        {
            obj.SetValue(IsExternalProperty, value);
        }

        private static void OnIsExternalChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var isExternal = args.NewValue as bool?;

            if (!isExternal.HasValue || !isExternal.Value)
            {
                return;
            }

            var hyperlink = sender as Hyperlink;

            if (hyperlink == null)
            {
                return;
            }

            WeakEventManager<Hyperlink, RequestNavigateEventArgs>.AddHandler(hyperlink, nameof(hyperlink.RequestNavigate), HyperlinkRequestNavigate);
        }

        private static void HyperlinkRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));

            e.Handled = true;
        }
    }
}