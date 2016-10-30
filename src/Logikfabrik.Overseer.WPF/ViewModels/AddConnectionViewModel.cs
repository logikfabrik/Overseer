// <copyright file="AddConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.ComponentModel;
    using System.Linq;
    using Caliburn.Micro;
    using EnsureThat;
    using FluentValidation;
    using Settings;

    /// <summary>
    /// The <see cref="AddConnectionViewModel" /> class.
    /// </summary>
    public abstract class AddConnectionViewModel : PropertyChangedBase, IDataErrorInfo
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IBuildProviderSettingsRepository _buildProviderSettingsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConnectionViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildProviderSettingsRepository">The build provider settings repository.</param>
        protected AddConnectionViewModel(IEventAggregator eventAggregator, IBuildProviderSettingsRepository buildProviderSettingsRepository)
        {
            Ensure.That(eventAggregator).IsNotNull();
            Ensure.That(buildProviderSettingsRepository).IsNotNull();

            _eventAggregator = eventAggregator;
            _buildProviderSettingsRepository = buildProviderSettingsRepository;
        }

        /// <summary>
        /// Gets or sets the connection name.
        /// </summary>
        /// <value>
        /// The connection name.
        /// </value>
        public string ConnectionName { get; set; }

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        public string Error => null;

        /// <summary>
        /// Gets the validator.
        /// </summary>
        /// <value>
        /// The validator.
        /// </value>
        protected abstract IValidator Validator { get; }

        /// <summary>
        /// Gets the error message for the property with the specified name.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <returns>The error message, if any.</returns>
        public string this[string name]
        {
            get
            {
                var result = Validator.Validate(this);

                if (result.IsValid)
                {
                    return null;
                }

                var error = result.Errors.FirstOrDefault(e => e.PropertyName == name);

                return error?.ErrorMessage;
            }
        }

        /// <summary>
        /// Add the connection.
        /// </summary>
        public void AddConnection()
        {
            if (!Validator.Validate(this).IsValid)
            {
                return;
            }

            _buildProviderSettingsRepository.AddBuildProviderSettings(GetBuildProviderSettings());

            ViewConnections();
        }

        /// <summary>
        /// View the connections.
        /// </summary>
        public void ViewConnections()
        {
            var message = new NavigationMessage(typeof(ConnectionsViewModel));

            _eventAggregator.PublishOnUIThread(message);
        }

        /// <summary>
        /// Gets the build provider settings.
        /// </summary>
        /// <returns>The build provider settings.</returns>
        protected abstract BuildProviderSettings GetBuildProviderSettings();
    }
}