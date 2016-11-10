namespace Logikfabrik.Overseer.WPF
{
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="ViewModel" /> class.
    /// </summary>
    public abstract class ViewModel : PropertyChangedBase
    {
        /// <summary>
        /// Gets the view name.
        /// </summary>
        /// <value>
        /// The view name.
        /// </value>
        public abstract string ViewName { get; }
    }
}
