using System;
using Backend;
using Balance;
using Cysharp.Threading.Tasks;
using SlotMachineStuff;

namespace UISystem
{
    public class FreeSpinsBehavior : SpinBehavior
    {
        private readonly ISpinBehaviorFactory _spinBehaviorFactory;

        public FreeSpinsBehavior(SlotMachine slotMachine, WinLinesLayer winLinesLayer, WinSlotsLayer winSlotsLayer,
            SettingsPanel settingsPanel, IBalanceService balanceService, 
            IBackendService backendService, ISpinBehaviorFactory spinBehaviorFactory)
            : base(slotMachine, winLinesLayer, winSlotsLayer, settingsPanel, balanceService, backendService)
        {
            _spinBehaviorFactory = spinBehaviorFactory;
        }

        public override UniTask Execute() 
            => UniTask.CompletedTask;

        public override async UniTask Execute(SpinResponseData data)
        {
            await _spinBehaviorFactory.CreateNormal().Execute(data);
        }

        public override void Cancel()
        {
            
        }
    }
}