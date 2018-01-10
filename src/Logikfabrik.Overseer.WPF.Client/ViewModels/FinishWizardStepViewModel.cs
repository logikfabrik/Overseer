namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using Caliburn.Micro;
    using WPF.ViewModels;

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
