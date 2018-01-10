﻿// <copyright file="FinishWizardStepViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels.Wizard
{
    using Caliburn.Micro;
    using WPF.ViewModels;

    /// <summary>
    /// The <see cref="FinishWizardStepViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class FinishWizardStepViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FinishWizardStepViewModel" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public FinishWizardStepViewModel(IPlatformProvider platformProvider)
            : base(platformProvider)
        {
        }

        public void Finish()
        {
            (Parent as IClose)?.TryClose(true);
        }
    }
}
