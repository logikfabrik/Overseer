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
    // ReSharper disable once InheritdocConsiderUsage
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

        /// <inheritdoc />
        public Type AppliesTo { get; }

        /// <inheritdoc />
        public IBuildProvider Create(ConnectionSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            return _resolutionRoot.Get<T2>(new ConstructorArgument(nameof(settings), settings, true));
        }
    }
}
