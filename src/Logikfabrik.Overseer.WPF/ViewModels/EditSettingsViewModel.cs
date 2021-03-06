﻿// <copyright file="EditSettingsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Caliburn.Micro;
    using EnsureThat;
    using Extensions;
    using JetBrains.Annotations;
    using Localization;
    using Validators;

    /// <summary>
    /// The <see cref="EditSettingsViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class EditSettingsViewModel : ViewModel, IDataErrorInfo
    {
        private readonly IApp _application;
        private readonly EditSettingsViewModelValidator _validator;
        private readonly IAppSettings _applicationSettings;
        private readonly IBuildTrackerSettings _buildTrackerSettings;
        private int _interval;
        private string _cultureName;
        private bool _showNotificationsForInProgressBuilds;
        private bool _showNotificationsForFailedBuilds;
        private bool _showNotificationsForSucceededBuilds;
        private bool _showNotificationsForStoppedBuilds;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditSettingsViewModel" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        /// <param name="application">The application.</param>
        /// <param name="applicationSettingsFactory">The application settings factory.</param>
        /// <param name="buildTrackerSettingsFactory">The build tracker settings factory.</param>
        [UsedImplicitly]

        // ReSharper disable once InheritdocConsiderUsage
        public EditSettingsViewModel(IPlatformProvider platformProvider, IApp application, IAppSettingsFactory applicationSettingsFactory, IBuildTrackerSettingsFactory buildTrackerSettingsFactory)
            : base(platformProvider)
        {
            Ensure.That(application).IsNotNull();
            Ensure.That(applicationSettingsFactory).IsNotNull();
            Ensure.That(buildTrackerSettingsFactory).IsNotNull();

            _application = application;
            _validator = new EditSettingsViewModelValidator();

            _applicationSettings = applicationSettingsFactory.Create();

            _cultureName = _applicationSettings.CultureName;
            _showNotificationsForInProgressBuilds = _applicationSettings.ShowNotificationsForInProgressBuilds;
            _showNotificationsForFailedBuilds = _applicationSettings.ShowNotificationsForFailedBuilds;
            _showNotificationsForSucceededBuilds = _applicationSettings.ShowNotificationsForSucceededBuilds;
            _showNotificationsForStoppedBuilds = _applicationSettings.ShowNotificationsForStoppedBuilds;

            CultureNames = SupportedCultures.CultureNames.Select(cultureName => new Tuple<string, string>(SupportedCulturesLocalizer.Localize(cultureName), cultureName));

            _buildTrackerSettings = buildTrackerSettingsFactory.Create();

            _interval = _buildTrackerSettings.Interval;

            DisplayName = Properties.Resources.EditSettings_View;
        }

        /// <summary>
        /// Gets or sets the interval in seconds.
        /// </summary>
        /// <value>
        /// The interval in seconds.
        /// </value>
        public int Interval
        {
            get
            {
                return _interval;
            }

            set
            {
                _interval = value;
                NotifyOfPropertyChange(() => Interval);
                NotifyOfPropertyChange(() => IsValid);
            }
        }

        /// <summary>
        /// Gets or sets the culture name.
        /// </summary>
        /// <value>
        /// The culture name.
        /// </value>
        public string CultureName
        {
            get
            {
                return _cultureName;
            }

            set
            {
                _cultureName = value;
                NotifyOfPropertyChange(() => CultureName);
                NotifyOfPropertyChange(() => IsValid);
            }
        }

        /// <summary>
        /// Gets the culture names.
        /// </summary>
        /// <value>
        /// The culture names.
        /// </value>
        public IEnumerable<Tuple<string, string>> CultureNames { get; }

        public bool ShowNotificationsForInProgressBuilds
        {
            get
            {
                return _showNotificationsForInProgressBuilds;
            }

            set
            {
                _showNotificationsForInProgressBuilds = value;
                NotifyOfPropertyChange(() => ShowNotificationsForInProgressBuilds);
            }
        }

        public bool ShowNotificationsForFailedBuilds
        {
            get
            {
                return _showNotificationsForFailedBuilds;
            }

            set
            {
                _showNotificationsForFailedBuilds = value;
                NotifyOfPropertyChange(() => ShowNotificationsForFailedBuilds);
            }
        }

        public bool ShowNotificationsForSucceededBuilds
        {
            get
            {
                return _showNotificationsForSucceededBuilds;
            }

            set
            {
                _showNotificationsForSucceededBuilds = value;
                NotifyOfPropertyChange(() => ShowNotificationsForSucceededBuilds);
            }
        }

        public bool ShowNotificationsForStoppedBuilds
        {
            get
            {
                return _showNotificationsForStoppedBuilds;
            }

            set
            {
                _showNotificationsForStoppedBuilds = value;
                NotifyOfPropertyChange(() => ShowNotificationsForStoppedBuilds);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid => _validator.Validate(this).IsValid;

        /// <inheritdoc />
        public string Error => null;

        /// <inheritdoc />
        public string this[string name]
        {
            get
            {
                var result = _validator.Validate(this);

                if (result.IsValid)
                {
                    return null;
                }

                var error = result.Errors.FirstOrDefault(e => e.PropertyName == name);

                return error?.ErrorMessage;
            }
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void Save()
        {
            if (!IsValid)
            {
                return;
            }

            var restart = _applicationSettings.CultureName != CultureName;

            _applicationSettings.CultureName = _cultureName;
            _applicationSettings.ShowNotificationsForInProgressBuilds = _showNotificationsForInProgressBuilds;
            _applicationSettings.ShowNotificationsForFailedBuilds = _showNotificationsForFailedBuilds;
            _applicationSettings.ShowNotificationsForSucceededBuilds = _showNotificationsForSucceededBuilds;
            _applicationSettings.ShowNotificationsForStoppedBuilds = _showNotificationsForStoppedBuilds;

            _applicationSettings.Save();

            _buildTrackerSettings.Interval = _interval;

            _buildTrackerSettings.Save();

            if (restart)
            {
                _application.Restart();
            }
        }
    }
}