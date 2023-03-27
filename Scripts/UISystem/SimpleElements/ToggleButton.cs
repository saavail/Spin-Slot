using System;
using DG.Tweening;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.SimpleElements
{
    public class ToggleButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private RectTransform _buttonRect;
        [SerializeField]
        private RectTransform _activeBackRect;

        private float _defaultOffX;
        private bool _isOn;

        public float DefaultOffX
        {
            get
            {
                if (_defaultOffX == 0)
                    _defaultOffX = _buttonRect.anchoredPosition.x;

                return _defaultOffX;
            }
        }

        public Action<bool> ClickAction { get; set; }

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
        }

        public void Set(bool isOn, float duration = 0.4f)
        {
            _isOn = isOn;
            
            _buttonRect.DOKill();
            _activeBackRect.DOKill();

            if (isOn)
            {
                _buttonRect.DOAnchorPosX(Mathf.Abs(_defaultOffX), duration)
                    .SetEase(Ease.InOutSine);
                
                _activeBackRect.DOAnchorMax(Vector2.one, duration)
                    .SetEase(Ease.InOutSine);
            }
            else
            {
                _buttonRect.DOAnchorPosX(_defaultOffX, duration)
                    .SetEase(Ease.InOutSine);
                
                _activeBackRect.DOAnchorMax(new Vector2(0.25f, 1f), duration)
                    .SetEase(Ease.InOutSine);
            }
        }

        public void ForceSet(bool isOn)
        {
            _isOn = isOn;
            
            _buttonRect.DOKill();
            _activeBackRect.DOKill();
            
            if (isOn)
            {
                _buttonRect.anchoredPosition = _buttonRect.anchoredPosition.ChangeX(Mathf.Abs(DefaultOffX));
                _activeBackRect.anchorMax = Vector2.one;
            }
            else
            {
                _buttonRect.anchoredPosition = _buttonRect.anchoredPosition.ChangeX(DefaultOffX);
                _activeBackRect.anchorMax = new Vector2(0.25f, 1f);
            }
        }

        private void OnClick()
        {
            Set(!_isOn);
            ClickAction?.Invoke(_isOn);
            
            //AudioPlayer.Play2D(_isOn ? SoundType.SoundOn : SoundType.SoundOff);
        }
    }
}