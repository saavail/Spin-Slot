using System.Threading;
using Backend;
using Balance;
using Cysharp.Threading.Tasks;
using SlotMachineStuff;

namespace UISystem
{
    public abstract class SpinBehavior
    {
        protected const float WinLineVisualSwapDelay = 2f;
        
        protected readonly SlotMachine _slotMachine;
        protected readonly WinLinesLayer _winLinesLayer;
        protected readonly WinSlotsLayer _winSlotsLayer;
        protected readonly SettingsPanel _settingsPanel;
        private readonly IBalanceService _balanceService;
        protected readonly IBackendService _backendService;

        protected CancellationTokenSource _tokenSource;
        protected UniTask _task;

        public bool IsWorking { get; private set; }
        
        public SpinBehavior(SlotMachine slotMachine, WinLinesLayer winLinesLayer, WinSlotsLayer winSlotsLayer, 
            SettingsPanel settingsPanel, IBalanceService balanceService, IBackendService backendService)
        {
            _slotMachine = slotMachine;
            _winLinesLayer = winLinesLayer;
            _winSlotsLayer = winSlotsLayer;
            _settingsPanel = settingsPanel;
            _balanceService = balanceService;
            _backendService = backendService;
        }
        
        public abstract UniTask Execute(SpinResponseData data);
        public abstract UniTask Execute();
        public abstract void Cancel();

        protected bool CanCancel() 
            => _task.Status != UniTaskStatus.Pending;

        protected void Prepare()
        {
            _winLinesLayer.HideAll();
            _winSlotsLayer.HideAll();
        }

        protected void ShowWinLine(WinLineResponseData data)
        {
            _winLinesLayer.ShowLine(data.Index);
            _winSlotsLayer.ShowLine(data.Index, data.SlotsCount);
        }
        
        protected void HideWinLine(WinLineResponseData data)
        {
            _winLinesLayer.HideLine(data.Index);
            _winSlotsLayer.HideLine(data.Index, data.SlotsCount);
        }
    }
}