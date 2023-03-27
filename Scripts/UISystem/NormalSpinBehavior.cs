using System;
using System.Threading;
using Backend;
using Balance;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SlotMachineStuff;

namespace UISystem
{
    public class NormalSpinBehavior : SpinBehavior
    {
        public NormalSpinBehavior(SlotMachine slotMachine, WinLinesLayer winLinesLayer, WinSlotsLayer winSlotsLayer,
            SettingsPanel settingsPanel, IBalanceService balanceService, IBackendService backendService)
            : base(slotMachine, winLinesLayer, winSlotsLayer, settingsPanel, balanceService, backendService) { }

        public override async UniTask Execute()
        {
            _tokenSource = new CancellationTokenSource();
            
            var data = _backendService.SendSpinRequest(default);

            _task = Execute(data);
            await _task;
        }

        public override async UniTask Execute(SpinResponseData data)
        {
            Prepare();
            
            await _slotMachine.SpinAsync(data.Drums, _tokenSource.Token);

            if (data.WinLines.Length > 1)
            {
                int index = 0;

                while (true)
                {
                    var winLine = data.WinLines[index];
                    
                    ShowWinLine(winLine);

                    await UniTask.Delay(TimeSpan.FromSeconds(WinLineVisualSwapDelay), cancellationToken: _tokenSource.Token);

                    HideWinLine(winLine);

                    index = (index + 1) % data.WinLines.Length;
                }
            }
            else if (data.WinLines.Length == 1)
            {
                ShowWinLine(data.WinLines[0]);
            }
        }

        public override void Cancel()
        {
            _tokenSource.Cancel();
        }
    }
}