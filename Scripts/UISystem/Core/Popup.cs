using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.Core
{
    public class Popup : MonoBehaviour
    {
        public const float StartAnimationScale = 0.1f;
        public const float AnimationDuration = 0.2f;
        
        [SerializeField]
        private Button _backgroundButton;
        [SerializeField]
        private Button _closeButton;
        [SerializeField]
        private RectTransform _contentRect;

        public bool IsTweening => DOTween.IsTweening(_backgroundButton) || DOTween.IsTweening(_contentRect);
        public bool IsOpen { get; private set; }
        
        protected virtual void Awake()
        {
            _backgroundButton.onClick.AddListener(Close);
            
            if (_closeButton != null)
                _closeButton.onClick.AddListener(Close);
        }

        public void Open()
        {
            if (IsTweening)
                return;

            IsOpen = true;
            gameObject.SetActive(true);
            
            _backgroundButton.image.DOFade(0.65f, AnimationDuration)
                .From(0f)
                .SetTarget(_backgroundButton)
                .SetEase(Ease.InQuad);

            _contentRect.DOScale(Vector3.one, AnimationDuration)
                .From(Vector3.one * StartAnimationScale)
                .SetEase(Ease.InQuad);
        }

        public void Close()
        {
            if (IsTweening)
                return;

            IsOpen = false;

            _backgroundButton.image.DOFade(0f, AnimationDuration)
                .SetTarget(_backgroundButton)
                .SetEase(Ease.OutQuad);

            _contentRect.DOScale(Vector3.one * StartAnimationScale, AnimationDuration)
                .OnComplete(() => gameObject.SetActive(false))
                .SetEase(Ease.OutQuad);
        }
    }
}