using System;
using Backend;
using Balance;
using Cysharp.Threading.Tasks;
using SlotMachineStuff;
using UISystem.AutoSpins;

namespace UISystem
{
    public class AutoSpinBehavior : SpinBehavior
    {
        private readonly ISpinBehaviorFactory _spinBehaviorFactory;
        private readonly AutoSpinSettings _autoSpinSettings;

        public AutoSpinBehavior(SlotMachine slotMachine, WinLinesLayer winLinesLayer, WinSlotsLayer winSlotsLayer,
            SettingsPanel settingsPanel, IBalanceService balanceService, IBackendService backendService, 
            ISpinBehaviorFactory spinBehaviorFactory, AutoSpinSettings autoSpinSettings)
            : base(slotMachine, winLinesLayer, winSlotsLayer, settingsPanel, balanceService, backendService)
        {
            _spinBehaviorFactory = spinBehaviorFactory;
            _autoSpinSettings = autoSpinSettings;
        }

        public override UniTask Execute(SpinResponseData data) 
            => UniTask.CompletedTask;

        public override async UniTask Execute()
        {
            for (int i = 0; i < _autoSpinSettings.SpinsCount; i++)
            {
                var data = _backendService.SendSpinRequest(default);

                if (data.IsFreeSpins())
                {
                    await _spinBehaviorFactory.CreateFreeSpins().Execute(data);
                    
                    if (_autoSpinSettings.StopIfFreeSpins)
                        return;
                }
                else
                {
                    await _spinBehaviorFactory.CreateNormal().Execute(data);
                    
                    if (_autoSpinSettings.StopIfWin && data.IsWinnable())
                        return;
                }
            }
        }

        public override void Cancel()
        {
            
        }
    }
}