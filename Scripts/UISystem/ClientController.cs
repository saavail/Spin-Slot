using System;
using Cysharp.Threading.Tasks;
using UISystem.AutoSpins;

namespace UISystem
{
    public class ClientController : IClientController, IDisposable
    {
        private readonly ISpinBehaviorFactory _spinBehaviorFactory;

        private SpinBehavior _lastBehavior;

        public ClientController(ISpinBehaviorFactory spinBehaviorFactory)
        {
            _spinBehaviorFactory = spinBehaviorFactory;
            
            SettingsPanel.OnSpinClick += SettingsPanel_OnSpinClick;
            AutoSpinPopup.OnAutoSpinClick += SettingsPanel_OnAutoSpinClick;
        }

        private void SettingsPanel_OnAutoSpinClick(AutoSpinSettings settings)
        {
            _lastBehavior?.Cancel();
            _lastBehavior = _spinBehaviorFactory.CreateAutoSpins(settings);
            _lastBehavior.Execute().Forget();
        }

        private void SettingsPanel_OnSpinClick()
        {
            _lastBehavior?.Cancel();
            _lastBehavior = _spinBehaviorFactory.CreateNormal();
            _lastBehavior.Execute().Forget();
        }

        public void Dispose()
        {
            SettingsPanel.OnSpinClick -= SettingsPanel_OnSpinClick;
            AutoSpinPopup.OnAutoSpinClick -= SettingsPanel_OnAutoSpinClick;
        }
    }
}