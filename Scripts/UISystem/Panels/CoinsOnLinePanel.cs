using TMPro;
using UnityEngine;

namespace UISystem
{
    public class CoinsOnLinePanel : MonoBehaviour,IAddingPanel
    {
        [SerializeField]
        private SettingsPanel _settingsPanel;
        [SerializeField]
        private TextMeshProUGUI _coinsOnLineText;

        private const int LastIndex = 4;
        private const int FirstIndex = 0;
        private readonly int[] _lines = new[] {1,2,3,4,5};
        private int _currentIndex;

        public int GetCoinsOnLineValue => _lines[_currentIndex];

        public void Initialize()
        {
            _currentIndex = FirstIndex;
            UpdateCoinsOnLines(_currentIndex);
        }

        public void DecreaseValue()
        {
            if (_currentIndex != FirstIndex)
            {
                _currentIndex--;
                UpdateCoinsOnLines(_currentIndex);
            }
        }

        public void IncreaseValue()
        {
            if (_currentIndex != LastIndex)
            {
                _currentIndex++;
                UpdateCoinsOnLines(_currentIndex);
            }
        }

        private void UpdateCoinsOnLines(int index)
        {
            _coinsOnLineText.text = _lines[_currentIndex].ToString();
            _settingsPanel.UpdateBetByCoins();
        }
    }
}