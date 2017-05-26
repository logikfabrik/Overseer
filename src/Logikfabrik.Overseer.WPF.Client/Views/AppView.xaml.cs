// <copyright file="AppView.xaml.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Views
{
    using System;
    using System.Windows;

    /// <summary>
    /// The <see cref="AppView" /> class.
    /// </summary>
    public partial class AppView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppView" /> class.
        /// </summary>
        public AppView()
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }
    }
}