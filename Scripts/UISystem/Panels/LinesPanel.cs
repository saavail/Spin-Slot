using TMPro;
using UnityEngine;

namespace UISystem
{
    public class LinesPanel : MonoBehaviour, IAddingPanel
    {
        [SerializeField]
        private SettingsPanel _settingsPanel;
        [SerializeField]
        private TextMeshProUGUI _linesText;

        private const int LastIndex = 8;
        private const int FirstIndex = 0;
        private readonly int[] _lines = new[] {1,2,3,4,5,6,7,8,9};
        private int _currentIndex;

        public int GetLinesValue => _lines[_currentIndex];

        public void Initialize()
        {
            _currentIndex = LastIndex;
            UpdateLines(_currentIndex);
        }

        public void DecreaseValue()
        {
            if (_currentIndex != FirstIndex)
            {
                _currentIndex--;
                UpdateLines(_currentIndex);
            }
        }

        public void IncreaseValue()
        {
            if (_currentIndex != LastIndex)
            {
                _currentIndex++;
                UpdateLines(_currentIndex);
            }
        }

        private void UpdateLines(int index)
        {
            _linesText.text = _lines[_currentIndex].ToString();
            _settingsPanel.UpdateBetByCoins();
        }
    }
}