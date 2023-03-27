using Backend;
using Balance;
using SlotMachineStuff;
using UISystem.AutoSpins;

namespace UISystem
{
    public class SpinBehaviorFactory : ISpinBehaviorFactory
    {
        private readonly SlotMachine _slotMachine;
        private readonly WinLinesLayer _winLinesLayer;
        private readonly WinSlotsLayer _winSlotsLayer;
        private readonly SettingsPanel _settingsPanel;
        private readonly IBalanceService _balanceService;
        private readonly IBackendService _backendService;

        public SpinBehaviorFactory(SlotMachine slotMachine, WinLinesLayer winLinesLayer, WinSlotsLayer winSlotsLayer, 
            SettingsPanel settingsPanel, IBalanceService balanceService, IBackendService backendService)
        {
            _slotMachine = slotMachine;
            _winLinesLayer = winLinesLayer;
            _winSlotsLayer = winSlotsLayer;
            _settingsPanel = settingsPanel;
            _balanceService = balanceService;
            _backendService = backendService;
        }
        
        public NormalSpinBehavior CreateNormal()
        {
            return new NormalSpinBehavior(_slotMachine, _winLinesLayer, _winSlotsLayer, _settingsPanel, _balanceService, 
                _backendService);
        }

        public AutoSpinBehavior CreateAutoSpins(AutoSpinSettings autoSpinSettings)
        {
            return new AutoSpinBehavior(_slotMachine, _winLinesLayer, _winSlotsLayer, _settingsPanel, _balanceService, _backendService, 
                this, autoSpinSettings);
        }

        public FreeSpinsBehavior CreateFreeSpins()
        {
            return new FreeSpinsBehavior(_slotMachine, _winLinesLayer, _winSlotsLayer, _settingsPanel, _balanceService, 
                _backendService, this);
        }
    }
}