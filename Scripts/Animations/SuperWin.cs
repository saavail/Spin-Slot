using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Animations
{
    public class SuperWin : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _coinsEffect;
        [SerializeField] private ParticleSystem _shineEffect;
        [SerializeField] private GameObject _winType, _winCounter;
        private const float _delayForStop = 3.5f;

        [Header("Counter Up")]
        [SerializeField] private int _winingValue;
        [SerializeField] private int _countFPS;
        [SerializeField] private float _counterDuration;
        private int _initialCount = 1;
        private TextMeshProUGUI _counter;

        [Header("Background")] 
        [SerializeField] private Image _background;

        [Header("Scale")] 
        [SerializeField] private Vector3 _scaleEndValue;
        [SerializeField] private float _scaleDuration;
        
        [Header("Punch")]
        [SerializeField] private Vector3 _punchDirection;
        [SerializeField] private float _punchDuration;
        [SerializeField] private int _punchVibro;
        
        private void Awake()
        {
            _coinsEffect.Stop();
            _counter = _winCounter.GetComponent<TextMeshProUGUI>();
            _counter.text = _initialCount.ToString();
        }

        public void TestButton()
        {
            StartAnimation();
        }

        private void StartAnimation()
        {
            _winCounter.SetActive(true);
            _winType.SetActive(true);
            _background.gameObject.SetActive(true);
            _shineEffect.gameObject.SetActive(true);
            TextAnimation();
            StartCoroutine(CounterText(_winingValue));
        }
        
        private void TextAnimation()
        {
            _winType.transform.localScale = Vector3.zero;
            _winCounter.transform.localScale =Vector3.zero;
            _shineEffect.transform.localScale = Vector3.zero;
            
            var superWinSequence = DOTween.Sequence();

            superWinSequence.Append(_shineEffect.transform.DOScale(1f, 2f).SetEase(Ease.InOutQuint))
                .Join(ScaleAnimation(_winType).SetDelay(0.4f))
                .Join(ScaleAnimation(_winCounter).OnComplete(() =>
                {
                    PunchAnimation(_winType);
                    PunchAnimation(_winCounter);
                    var size = _shineEffect.sizeOverLifetime;
                    size.enabled = true;
                    _coinsEffect.gameObject.SetActive(true);
                    BackgroundAnimation();
                    superWinSequence.SetAutoKill(true);
                }));
        }

        private IEnumerator StopAnimation()
        {
            _coinsEffect.Stop();
            yield return new WaitForSeconds(_delayForStop);
            _background.DOKill();
            _winCounter.SetActive(false);
            _winType.SetActive(false);
            _shineEffect.gameObject.SetActive(false);
            _coinsEffect.gameObject.SetActive(false);
            _background.gameObject.SetActive(false);
        }
        
        private void BackgroundAnimation()
        {
            _background.fillAmount = 0f;
            _background.DOFade(0.35f, 0.6f).SetLoops(-1, LoopType.Yoyo);
        }

        private IEnumerator CounterText(int newValue)
        {
            WaitForSeconds wait = new WaitForSeconds(1f / _countFPS);
            int previousValue = _initialCount;
            // if we need add coins by step
            // int stepAmount = Mathf.CeilToInt((newValue - previousValue) / (_countFPS * _counterDuration));

            while (previousValue < newValue)
            {
                previousValue += 1;
                if (previousValue > newValue)
                {
                    previousValue = newValue;
                }
                
                _counter.text = previousValue.ToString();
                yield return wait;
            }

            StartCoroutine(StopAnimation());
        }
        
        private Tween ScaleAnimation(GameObject animationObject)
        {
            return animationObject.transform.DOScale(_scaleEndValue, _scaleDuration).SetEase(Ease.InExpo);
        }

        private Tween PunchAnimation(GameObject animationObject)
        {
            return animationObject.transform.DOPunchScale(_punchDirection, _punchDuration, _punchVibro);
        }
        
    }
}