// <copyright file="BuildNotificationViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Threading;
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildNotificationViewModel" /> class.
    /// </summary>
    public class BuildNotificationViewModel : ViewAware, IBuildNotificationViewModel
    {
        private readonly Uri _webUrl;
        private DispatcherTimer _dispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildNotificationViewModel" /> class.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="build">The build.</param>
        public BuildNotificationViewModel(IProject project, IBuild build)
        {
            Ensure.That(project).IsNotNull();
            Ensure.That(build).IsNotNull();

            Name = BuildMessageUtility.GetBuildName(project, build);
            Message = BuildMessageUtility.GetBuildStatusMessage(build.Status, new Dictionary<string, string> { { "requested by", build.RequestedBy } });
            Status = build.Status;
            _webUrl = build.WebUrl;

            Close();
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public BuildStatus? Status { get; }

        /// <summary>
        /// 
        /// </summary>
        public void View()
        {
            if (_webUrl == null)
            {
                return;
            }

            Process.Start(new ProcessStartInfo(_webUrl.AbsoluteUri));
        }

        /// <summary>
        /// Keeps the notification open.
        /// </summary>
        public void KeepOpen()
        {
            _dispatcher?.Stop();
        }

        /// <summary>
        /// Closes the notification.
        /// </summary>
        public void Close()
        {
            const int showForSeconds = 10;

            _dispatcher?.Stop();

            _dispatcher = new DispatcherTimer(TimeSpan.FromSeconds(showForSeconds), DispatcherPriority.Normal, (sender, args) => { TryClose(); }, Application.Current.Dispatcher);

            _dispatcher.Start();
        }

        private void TryClose()
        {
            var popup = GetView() as Popup;

            if (popup == null)
            {
                return;
            }

            popup.IsOpen = false;
        }
    }
}