// <copyright file="BuildProviderFactory{T1,T2}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using EnsureThat;
    using Ninject;
    using Ninject.Parameters;
    using Ninject.Syntax;
    using Settings;

    /// <summary>
    /// The <see cref="BuildProviderFactory{T1,T2}" /> class.
    /// </summary>
    /// <typeparam name="T1">The <see cref="ConnectionSettings" /> type.</typeparam>
    /// <typeparam name="T2">The <see cref="BuildProvider{T1}" /> type.</typeparam>
    public class BuildProviderFactory<T1, T2> : IBuildProviderFactory
        where T1 : ConnectionSettings
        where T2 : BuildProvider<T1>
    {
        private readonly IResolutionRoot _resolutionRoot;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderFactory{T1,T2}" /> class.
        /// </summary>
        /// <param name="resolutionRoot">The resolution root.</param>
        public BuildProviderFactory(IResolutionRoot resolutionRoot)
        {
            Ensure.That(resolutionRoot).IsNotNull();

            AppliesTo = typeof(T2);
            _resolutionRoot = resolutionRoot;
        }

        /// <summary>
        /// Gets the type this factory applies to.
        /// </summary>
        /// <value>
        /// The type this factory applies to.
        /// </value>
        public Type AppliesTo { get; }

        /// <summary>
        /// Creates a <see cref="IBuildProvider" />.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// A <see cref="IBuildProvider" />.
        /// </returns>
        public IBuildProvider Create(ConnectionSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            return _resolutionRoot.Get<T2>(new ConstructorArgument(nameof(settings), settings, true));
        }
    }
}
