using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class WinLineVisualiseButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private TextMeshProUGUI _text;

        private WinLinesLayer _winLinesLayer;
        private WinSlotsLayer _winSlotsLayer;
        private int _index;

        private void Start()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        public void Initialize(WinLinesLayer winLinesLayer, WinSlotsLayer winSlotsLayer, int index)
        {
            _winLinesLayer = winLinesLayer;
            _winSlotsLayer = winSlotsLayer;
            _index = index;

            _text.text = $"Line {_index + 1}";
        }
        
        private void OnButtonClick()
        {
            if (_winLinesLayer.IsLineActive(_index))
            {
                _winLinesLayer.HideLine(_index);
                _winSlotsLayer.HideLine(_index);
            }
            else
            {
                _winLinesLayer.ShowLine(_index);
                _winSlotsLayer.ShowLine(_index);
            }
        }
    }
}