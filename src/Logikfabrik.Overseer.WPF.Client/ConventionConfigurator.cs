// <copyright file="ConventionConfigurator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client
{
    using System.Windows.Controls;
    using Caliburn.Micro;
    using Extensions;

    public static class ConventionConfigurator
    {
        public static void Configure()
        {
            ConventionManager.AddElementConvention<PasswordBox>(
                PasswordBoxExtensions.BoundPasswordProperty,
                nameof(PasswordBox.Password),
                nameof(PasswordBox.PasswordChanged));
        }
    }
}
