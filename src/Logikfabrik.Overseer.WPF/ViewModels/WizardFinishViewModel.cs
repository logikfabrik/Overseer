// <copyright file="WizardFinishViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="WizardFinishViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class WizardFinishViewModel : IWizardViewModel, IChild<WizardViewModel>
    {
        /// <inheritdoc/>
        object IChild.Parent
        {
            get { return Parent; }
            set { Parent = value as WizardViewModel; }
        }

        /// <inheritdoc/>
        public WizardViewModel Parent { get; set; }

        public void Finish()
        {
            (Parent as IClose)?.TryClose(true);
        }
    }
}