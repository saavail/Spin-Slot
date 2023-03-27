using Backend;
using Balance;
using DependencyInjector;
using EntryPoint;
using Graphics;
using SlotMachineStuff;
using UISystem.Core;
using UnityEngine;
using Utilities.Pool;

namespace UISystem
{
    public class UICore : MonoBehaviour
    {
        [SerializeField]
        private SlotMachine _slotMachine;
        [SerializeField]
        private SlotsEnvironment _slotsEnvironment;
        [SerializeField]
        private WinLinesLayer _winLinesLayer;
        [SerializeField]
        private WinSlotsLayer _winSlotsLayer;
        [SerializeField]
        private SettingsPanel _panelSettings;
        [SerializeField]
        private Transform _popupsRoot;

        public void Initialize(IIconsService iconsService, IBalanceService balanceService, IBackendService backendService, IPoolService poolService)
        {
            var popupsSystem = new PopupsSystem(AllServices.Container.Single<IResourceLoader>(), _popupsRoot);
            AllServices.Container.RegisterSingle<IPopupSystemService>(popupsSystem);

            var spinBehaviorFactory = new SpinBehaviorFactory(_slotMachine, _winLinesLayer,
                _winSlotsLayer, _panelSettings, balanceService, backendService);
            
            AllServices.Container.RegisterSingle<ISpinBehaviorFactory>(spinBehaviorFactory);
            
            AllServices.Container.RegisterSingle<IClientController>(new ClientController(spinBehaviorFactory));

            _slotMachine.Initialize(iconsService, balanceService);
            _winSlotsLayer.Initialize(_slotMachine, poolService, balanceService, iconsService);
            _winLinesLayer.Initialize(poolService, balanceService);
            _slotsEnvironment.Initialize(_winLinesLayer, _winSlotsLayer, balanceService);
            _panelSettings.Initialize(backendService, popupsSystem);
        }
    }
}