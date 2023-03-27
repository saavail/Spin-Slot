using DependencyInjector;

namespace UISystem.Core
{
    public interface IPopupSystemService : IService
    {
        Popup Show<TPopup>();
    }
}