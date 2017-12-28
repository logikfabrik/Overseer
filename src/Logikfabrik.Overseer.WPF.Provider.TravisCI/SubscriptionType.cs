// <copyright file="SubscriptionType.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TravisCI
{
    /// <summary>
    /// The <see cref="SubscriptionType" /> enumeration.
    /// </summary>
    public enum SubscriptionType
    {
        /// <summary>
        /// Subscription for Travis CI for Open Source.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        TravisCIForOpenSource = 0,

        /// <summary>
        /// Subscription for Travis Pro.
        /// </summary>
        TravisPro = 1,

        /// <summary>
        /// Subscription for Travis Enterprise.
        /// </summary>
        TravisEnterprise = 2
    }
}
