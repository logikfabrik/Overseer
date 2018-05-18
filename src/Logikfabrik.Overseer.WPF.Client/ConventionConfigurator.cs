// <copyright file="ConventionConfigurator.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client
{
    using System.Windows.Controls;
    using Caliburn.Micro;
    using Extensions;

    /// <summary>
    /// The <see cref="ConventionConfigurator" /> class.
    /// </summary>
    public static class ConventionConfigurator
    {
        /// <summary>
        /// Configures conventions.
        /// </summary>
        public static void Configure()
        {
            ConventionManager.AddElementConvention<PasswordBox>(
                PasswordBoxExtensions.BoundPasswordProperty,
                nameof(PasswordBox.Password),
                nameof(PasswordBox.PasswordChanged));
        }
    }
}
