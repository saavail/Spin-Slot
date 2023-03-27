using System.Collections.Generic;
using System.Linq;
using Backend;
using TMPro;
using UnityEngine;

namespace UISystem
{
    public class CoinsValuePanel : MonoBehaviour, IAddingPanel
    {
        [SerializeField]
        private SettingsPanel _settingsPanel;
        [SerializeField]
        private TextMeshProUGUI _coinsOnLineText;

        private List<int> _coinsValueAvailable = new List<int>();
        private const int FirstIndex = 0;
        private int _lastIndex;
        private int _currentIndex;

        public int GetCoinValueInPlayerCurrency => _coinsValueAvailable[_currentIndex];
        public float GetCoinValueInCurrency => (float)_coinsValueAvailable[_currentIndex] / 100;

        public void Initialize(UserInitResponseData responseData)
        {
            _coinsValueAvailable = responseData.CoinPossibleValues.ToList();
            _lastIndex = _coinsValueAvailable.Count - 1;
            _currentIndex = FirstIndex;
            UpdateCoinsValue(_currentIndex);
        }

        public void DecreaseValue()
        {
            if (_currentIndex != FirstIndex)
            {
                _currentIndex--;
                UpdateCoinsValue(_currentIndex);
            }
        }

        public void IncreaseValue()
        {
            if (_currentIndex != _lastIndex)
            {
                _currentIndex++;
                UpdateCoinsValue(_currentIndex);
            }
        }

        private void UpdateCoinsValue(int index)
        {
            _coinsOnLineText.text = ((float)_coinsValueAvailable[index] / 100).ToString();
            _settingsPanel.UpdateBalanceByCoin();
        }
    }
}