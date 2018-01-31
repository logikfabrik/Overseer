// <copyright file="PasswordBoxExtensions.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Extensions
{
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// The <see cref="PasswordBoxExtensions" /> class.
    /// </summary>
    /// <remarks>
    /// Based on SO https://stackoverflow.com/a/31079674, answered by FMM, https://stackoverflow.com/users/170407/fmm.
    /// </remarks>
    public static class PasswordBoxExtensions
    {
        /// <summary>
        /// A dependency property.
        /// </summary>
        public static readonly DependencyProperty BoundPasswordProperty = DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PasswordBoxExtensions), new FrameworkPropertyMetadata(string.Empty, OnBoundPasswordChanged));

        /// <summary>
        /// Gets the bound password.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The bound password.</returns>
        public static string GetBoundPassword(DependencyObject obj)
        {
            var passwordBox = obj as PasswordBox;

            // ReSharper disable once InvertIf
            if (passwordBox != null)
            {
                passwordBox.PasswordChanged -= PasswordChanged;
                passwordBox.PasswordChanged += PasswordChanged;
            }

            return (string)obj.GetValue(BoundPasswordProperty);
        }

        /// <summary>
        /// Sets the bound password.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetBoundPassword(DependencyObject obj, string value)
        {
            if (value == GetBoundPassword(obj))
            {
                return;
            }

            obj.SetValue(BoundPasswordProperty, value);
        }

        private static void OnBoundPasswordChanged(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            var passwordBox = sender as PasswordBox;

            if (passwordBox == null)
            {
                return;
            }

            passwordBox.Password = GetBoundPassword(passwordBox);
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;

            if (passwordBox == null)
            {
                return;
            }

            SetBoundPassword(passwordBox, passwordBox.Password);

            var method = passwordBox.GetType().GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic);

            method?.Invoke(passwordBox, new object[] { passwordBox.Password.Length, 0 });
        }
    }
}