using System;
using Balance;
using DependencyInjector;
using TMPro;
using UISystem.Core;
using UISystem.SimpleElements;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.AutoSpins
{
    public class AutoSpinPopup : Popup
    {
        [SerializeField]
        private Button _activeAutoSpinsButton;
        
        [Header("Text")]
        [SerializeField]
        private TextMeshProUGUI _headerText;
        [SerializeField]
        private TextMeshProUGUI _countersLabelText;
        [SerializeField]
        private TextMeshProUGUI _togglesLabelText;
        [SerializeField]
        private TextMeshProUGUI _winToggleText;
        [SerializeField]
        private TextMeshProUGUI _freeSpinsToggleText;
        [SerializeField]
        private TextMeshProUGUI _cancelButtonText;
        [SerializeField]
        private TextMeshProUGUI _activateButtonText;
        
        [Header("Controllers")]
        [SerializeField]
        private AutoSpinCounter[] _spinCounters;
        [SerializeField]
        private ToggleButton _stopIfWinButton;
        [SerializeField]
        private ToggleButton _stopIfFreeSpinsButton;

        public static event Action<AutoSpinSettings> OnAutoSpinClick;

        private AutoSpinSettings _autoSpinSettings;
        private AutoSpinCounter _prevAutoSpinCounter;

        protected override void Awake()
        {
            base.Awake();
            
            _activeAutoSpinsButton.onClick.AddListener(OnActiveAutoSpinsClick);

            _stopIfWinButton.ClickAction = OnStopIfWinClick;
            _stopIfFreeSpinsButton.ClickAction = OnStopIfFreeSpinsClick;

            var data = AllServices.Container.Single<IBalanceService>().AutoSpinsData;

            for (int i = 0; i < data.Counters.Length && i < _spinCounters.Length; i++)
            {
                _spinCounters[i].Initialize(data.Counters[i], i == data.DefaultCounterIndex);

                if (i == data.DefaultCounterIndex) 
                    _prevAutoSpinCounter = _spinCounters[i];
            }

            _stopIfWinButton.ForceSet(data.DefaultStopIfWin);
            _stopIfFreeSpinsButton.ForceSet(data.DefaultStopIfFreeSpins);

            _autoSpinSettings.SpinsCount = data.Counters[data.DefaultCounterIndex];
            _autoSpinSettings.StopIfWin = data.DefaultStopIfWin;
            _autoSpinSettings.StopIfFreeSpins = data.DefaultStopIfFreeSpins;

            Localize();
        }

        private void Localize()
        {
            _headerText.text = Strings.AutoSpinsHeader;
            _countersLabelText.text = Strings.AutoSpinsCountersLabel;
            _togglesLabelText.text = Strings.AutoSpinsTogglesLabel;
            _winToggleText.text = Strings.AutoSpinsWinToggle;
            _freeSpinsToggleText.text = Strings.AutoSpinsFreeSpinsToggle;
            _cancelButtonText.text = Strings.Cancel;
            _activateButtonText.text = Strings.Start;
        }

        private void OnEnable()
        {
            AutoSpinCounter.OnCounterClick += AutoSpinCounter_OnCounterClick;
        }

        private void OnDisable()
        {
            AutoSpinCounter.OnCounterClick -= AutoSpinCounter_OnCounterClick;
        }

        private void OnActiveAutoSpinsClick()
        {
            OnAutoSpinClick?.Invoke(_autoSpinSettings);
        }

        private void OnStopIfWinClick(bool isOn)
        {
            _autoSpinSettings.StopIfWin = isOn;
        }
        
        private void OnStopIfFreeSpinsClick(bool isOn)
        {
            _autoSpinSettings.StopIfFreeSpins = isOn;
        }

        private void AutoSpinCounter_OnCounterClick(AutoSpinCounter counter)
        {
            _prevAutoSpinCounter.DeActivate();
            
            _prevAutoSpinCounter = counter;
            counter.Activate();

            _autoSpinSettings.SpinsCount = counter.Counter;
        }
    }
}