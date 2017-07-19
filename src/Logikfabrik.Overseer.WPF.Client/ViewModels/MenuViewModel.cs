// <copyright file="MenuViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using System;
    using System.Windows.Input;
    using Caliburn.Micro;
    using EnsureThat;
    using Navigation;
    using WPF.ViewModels;

    /// <summary>
    /// The <see cref="MenuViewModel" /> class.
    /// </summary>
    public class MenuViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;
        private bool _isExpanded;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public MenuViewModel(IEventAggregator eventAggregator)
        {
            Ensure.That(eventAggregator).IsNotNull();

            _eventAggregator = eventAggregator;

            // TODO: Move to better location.
            InputManager.Current.PreProcessInput += (sender, e) =>
            {
                var args = e.StagingItem.Input as MouseButtonEventArgs;

                if (args == null)
                {
                    return;
                }

                if (args.ChangedButton == MouseButton.Left && args.ButtonState == MouseButtonState.Released)
                {
                    // TODO: Toggle the menu hamburger in XAML.
                    Close();
                }
            };

        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is expanded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is expanded; otherwise, <c>false</c>.
        /// </value>
        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }

            set
            {
                _isExpanded = value;
                NotifyOfPropertyChange(() => IsExpanded);
            }
        }

        /// <summary>
        /// Toggles this instance.
        /// </summary>
        public void Toggle()
        {
            IsExpanded = !IsExpanded;
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            IsExpanded = false;
        }

        /// <summary>
        /// Goes to dashboard.
        /// </summary>
        public void GoToDashboard()
        {
            GoTo(typeof(DashboardViewModel));
        }

        /// <summary>
        /// Goes to connections.
        /// </summary>
        public void GoToConnections()
        {
            GoTo(typeof(ConnectionsViewModel));
        }

        /// <summary>
        /// Goes to add connection.
        /// </summary>
        public void GoToAddConnection()
        {
            GoTo(typeof(BuildProvidersViewModel));
        }

        /// <summary>
        /// Goes to settings.
        /// </summary>
        public void GoToSettings()
        {
            GoTo(typeof(EditSettingsViewModel));
        }

        /// <summary>
        /// Goes to about.
        /// </summary>
        public void GoToAbout()
        {
            GoTo(typeof(AboutViewModel));
        }

        private void GoTo(Type itemType)
        {
            var message = new NavigationMessage(itemType);

            _eventAggregator.PublishOnUIThread(message);

            Close();
        }
    }
}
