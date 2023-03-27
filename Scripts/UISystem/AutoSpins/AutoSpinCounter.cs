using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.AutoSpins
{
    public class AutoSpinCounter : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _counterText;
        [SerializeField]
        private Button _mainButton;

        [Header("Settings")]
        [SerializeField]
        private Sprite _notActiveSprite;
        [SerializeField]
        private Sprite _activeSprite;
        [SerializeField]
        private float _activeScale = 1.2f;

        private RectTransform _rect;

        public static event Action<AutoSpinCounter> OnCounterClick; 

        public int Counter { get; private set; }

        public void Initialize(int counter, bool isActive)
        {
            Counter = counter;
            
            _rect = (RectTransform) transform;

            if (isActive)
            {
                _rect.localScale = Vector3.one * _activeScale;
                _mainButton.image.sprite = _activeSprite;
            }

            _counterText.text = counter.ToString();
        }

        private void Awake()
        {
            _mainButton.onClick.AddListener(OnMainButtonClick);
        }

        public void Activate()
        {
            _rect.localScale = Vector3.one * _activeScale;
            _mainButton.image.sprite = _activeSprite;
        }

        public void DeActivate()
        {
            _rect.localScale = Vector3.one;
            _mainButton.image.sprite = _notActiveSprite;
        }

        private void OnMainButtonClick()
        {
            OnCounterClick?.Invoke(this);
        }
    }
}