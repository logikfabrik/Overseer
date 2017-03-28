// <copyright file="ConnectionViewModelFactory{T1,T2}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using System;
    using EnsureThat;
    using Ninject;
    using Ninject.Parameters;
    using Ninject.Syntax;
    using Settings;

    /// <summary>
    /// The <see cref="ConnectionViewModelFactory{T1,T2}" /> class.
    /// </summary>
    /// <typeparam name="T1">The <see cref="ConnectionSettings" /> type.</typeparam>
    /// <typeparam name="T2">The <see cref="ConnectionViewModel{T}" /> type.</typeparam>
    public class ConnectionViewModelFactory<T1, T2> : IConnectionViewModelFactory
        where T1 : ConnectionSettings
        where T2 : ConnectionViewModel<T1>
    {
        private readonly IResolutionRoot _resolutionRoot;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionViewModelFactory{T1,T2} "/> class.
        /// </summary>
        /// <param name="resolutionRoot">The resolution root.</param>
        public ConnectionViewModelFactory(IResolutionRoot resolutionRoot)
        {
            Ensure.That(resolutionRoot).IsNotNull();

            AppliesTo = typeof(T1);
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
        /// Creates a view model.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// A view model.
        /// </returns>
        public T2 Create(ConnectionSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            return _resolutionRoot.Get<T2>(new ConstructorArgument(nameof(settings), settings));
        }

        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// A view model.
        /// </returns>
        IConnectionViewModel IConnectionViewModelFactory.Create(ConnectionSettings settings)
        {
            return Create(settings);
        }
    }
}
