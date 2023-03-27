using DependencyInjector;
using UISystem.AutoSpins;

namespace UISystem
{
    public interface ISpinBehaviorFactory : IService
    {
        NormalSpinBehavior CreateNormal();
        AutoSpinBehavior CreateAutoSpins(AutoSpinSettings autoSpinSettings);
        FreeSpinsBehavior CreateFreeSpins();
    }
}