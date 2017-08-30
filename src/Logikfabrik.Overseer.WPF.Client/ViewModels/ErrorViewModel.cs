// <copyright file="ErrorViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using System.Windows;
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="MenuViewModel" /> class.
    /// </summary>
    public class ErrorViewModel : PropertyChangedBase
    {
        private string _message;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorViewModel" /> class.
        /// </summary>
        /// <param name="application">The application.</param>
        public ErrorViewModel(Application application)
        {
            Ensure.That(application).IsNotNull();

            application.DispatcherUnhandledException += (sender, e) =>
            {
                Message = e.Exception?.Message;

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
    }
}
