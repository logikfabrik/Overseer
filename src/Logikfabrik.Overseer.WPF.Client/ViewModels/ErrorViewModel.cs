// <copyright file="ErrorViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;
    using Localization;

    /// <summary>
    /// The <see cref="MenuViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ErrorViewModel : PropertyChangedBase
    {
        private string _message;
        private bool _isExpanded;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorViewModel" /> class.
        /// </summary>
        /// <param name="application">The application.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public ErrorViewModel(IApp application)
        {
            Ensure.That(application).IsNotNull();

            application.DispatcherUnhandledException += (sender, e) =>
            {
                Message = AppViewErrorLocalizer.Localize(e.Exception);
                IsExpanded = true;

                e.Handled = true;
            };
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
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
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            IsExpanded = false;
        }
    }
}
