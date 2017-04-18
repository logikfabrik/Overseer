namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;

    public interface IViewModel : IViewAware, IClose, IHaveDisplayName, IChild
    {
    }
}