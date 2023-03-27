using System;
using Backend;
using SlotMachineStuff;
using TMPro;
using UISystem.AutoSpins;
using UISystem.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class SettingsPanel : MonoBehaviour
    {
        [SerializeField]
        private SlotMachine _slotMachine;
        [SerializeField]
        private CoinsValuePanel _coinsValuePanel;
        [SerializeField]
        private CoinsOnLinePanel _coinsOnLinePanel;
        [SerializeField]
        private LinesPanel _linesPanel;

        [SerializeField]
        private Button _spinButton;
        [SerializeField]
        private Button _autoSpinButton;

        [SerializeField]
        private TextMeshProUGUI _balanceByCoinText;
        [SerializeField]
        private TextMeshProUGUI _betByCoinText;
        [SerializeField]
        private TextMeshProUGUI _balanceByCurrrencyText;
        [SerializeField]
        private TextMeshProUGUI _betByCurrencyText;
        
        private IBackendService _backendService;
        private IPopupSystemService _popupSystem;
        private int _balanceInCoins;
        private int _betByCoins;
        private float _balanceInCurrency;
        private float _betByCurrency;
        private int _balanceInPlayerCurrency;

        public static event Action OnSpinClick;

        public void Initialize(IBackendService backendService, IPopupSystemService popupSystem)
        {
            _backendService = backendService;
            _popupSystem = popupSystem;
            
            var responseUserInit = backendService.SendInitRequest();

            _balanceInPlayerCurrency = responseUserInit.WalletBalance;
            _balanceInCurrency = (float) _balanceInPlayerCurrency / 100;
            _coinsValuePanel.Initialize(responseUserInit);
            _coinsOnLinePanel.Initialize();
            _linesPanel.Initialize();
            UpdateBalanceByCurrency();
        }

        private void Start()
        {
            _spinButton.onClick.AddListener(OnSpinButtonClick);
            _autoSpinButton.onClick.AddListener(OnAutoSpinButtonClick);
        }

        public SpinRequestData GenerateSpinRequestData()
        {
            return new SpinRequestData(
                _coinsOnLinePanel.GetCoinsOnLineValue,
                _linesPanel.GetLinesValue,
                _coinsValuePanel.GetCoinValueInPlayerCurrency);
        }

        public void UpdateBetByCoins()
        {
            _betByCoins = _coinsOnLinePanel.GetCoinsOnLineValue * _linesPanel.GetLinesValue;
            _betByCoinText.text = _betByCoins.ToString();
            UpdateBetByCurrency();
        }

        private void UpdateBetByCurrency()
        {
            _betByCurrency = _betByCoins * _coinsValuePanel.GetCoinValueInCurrency;
            _betByCurrencyText.text = "Bet: " + _betByCurrency;
        }

        public void UpdateBalanceByCoin()
        {
            _balanceInCoins = _balanceInPlayerCurrency / _coinsValuePanel.GetCoinValueInPlayerCurrency;
            _balanceByCoinText.text = _balanceInCoins.ToString();
            UpdateBetByCurrency();
        }

        private void UpdateBalanceByCurrency()
        {
            _balanceByCurrrencyText.text = "Balance: " + _balanceInCurrency;
        }

        private void OnAutoSpinButtonClick()
        {
            _popupSystem.Show<AutoSpinPopup>();
        }

        private void OnSpinButtonClick()
        {
            OnSpinClick?.Invoke();
        }
    }
}