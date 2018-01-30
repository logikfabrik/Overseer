// <copyright file="Wizard.xaml.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Views.Views.AddConnection
{
    using ViewModels;

    /// <summary>
    /// The <see cref="Wizard" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public partial class Wizard
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Wizard" /> class.
        /// </summary>
        // ReSharper disable once InheritdocConsiderUsage
        public Wizard()
        {
            InitializeComponent();

            Loaded += (sender, args) =>
            {
                var viewModel = DataContext as IContextAware;

                if (viewModel == null)
                {
                    return;
                }

                viewModel.Context = GetType().Name;
            };
        }
    }
}
