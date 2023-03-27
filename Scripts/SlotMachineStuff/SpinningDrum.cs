using System;
using System.Collections;
using System.Linq;
using Balance;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Graphics;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace SlotMachineStuff
{
    public class SpinningDrum : MonoBehaviour
    {
        [SerializeField]
        private SlotCell[] _slots;

        [Header("Settings")]
        [SerializeField]
        private int _direction = -1;
        [SerializeField]
        private float _speed = 4000;
        [SerializeField]
        private float _startSpinDuration = 0.7f;
        [SerializeField]
        private float _completeDuration = 0.5f;

        [Header("Animations")]
        [SerializeField]
        private bool _startSpinCustomCurve;
        [SerializeField, HideIf(nameof(IsUsingCustomStartSpinCurve))]
        private Ease _startSpinEase = Ease.InQuad;
        [SerializeField, HideIf(nameof(IsNotUsingStartCustomSpinCurve))]
        public AnimationCurve _startSpinCurve;
        
        [SerializeField]
        private bool _completeSpinCustomCurve;
        [SerializeField, HideIf(nameof(IsUsingCustomCompleteSpinCurve))]
        private Ease _completeEase = Ease.OutBack;
        [SerializeField, HideIf(nameof(IsNotUsingCustomCompleteSpinCurve))]
        public AnimationCurve _completeSpinCurve;
        
        private IIconsService _iconService;
        private IBalanceService _balanceService;

        private RectTransform _myRect;

        private Coroutine _startSpinRoutine;
        private Coroutine _spinRoutine;
        private Coroutine _completeSpinRoutine;

        private int[] _lastIndexes;
        private int[] _newIndexes;
        private int _reSetupIndex;
        private float _justSpinDuration;

        public SlotCell[] Slots => _slots;
        public RectTransform Rect => _myRect ??= transform as RectTransform;
        private float HalfHeight => Rect.rect.height / 2f;
        public bool IsSpinning => _startSpinRoutine != null || _completeSpinRoutine != null || _spinRoutine != null;
        
        private bool IsUsingCustomStartSpinCurve => _startSpinCustomCurve;
        private bool IsNotUsingStartCustomSpinCurve => !_startSpinCustomCurve;
        private bool IsUsingCustomCompleteSpinCurve => _completeSpinCustomCurve;
        private bool IsNotUsingCustomCompleteSpinCurve => !_completeSpinCustomCurve;
        
        public void Initialize(IIconsService iconService, IBalanceService balanceService)
        {
            _iconService = iconService;
            _balanceService = balanceService;

            _lastIndexes = _slots.Select(i => Random.Range(0, balanceService.SlotsData.Slots.Length)).ToArray();

            for (var i = 0; i < _slots.Length; i++)
            {
                _slots[i].Initialize(iconService);
                _slots[i].Setup(_lastIndexes[i]);
            }
        }

        // public void Spin(int[] indexes, float justSpinDuration)
        // {
        //     _newIndexes = indexes;
        //     _justSpinDuration = justSpinDuration;
        //     
        //     this.StopCoroutine(ref _startSpinRoutine);
        //     this.StopCoroutine(ref _completeSpinRoutine);
        //
        //     _startSpinRoutine = StartCoroutine(StartSpinRoutine());
        // }
        
        public async UniTask SpinAsync(int[] indexes, float justSpinDuration)
        {
            _newIndexes = indexes.Reverse().ToArray();
            _justSpinDuration = justSpinDuration;
            
            this.StopCoroutine(ref _startSpinRoutine);
            this.StopCoroutine(ref _completeSpinRoutine);

            await StartSpinRoutine().ToUniTask(this);
            await SpinRoutine().ToUniTask(this);
            await CompleteSpinRoutine().ToUniTask(this);
        }

        public void FastCompleteSpin()
        {
            if (_spinRoutine != null)
                return;
            
            this.StopCoroutine(ref _spinRoutine);
            _completeSpinRoutine = StartCoroutine(CompleteSpinRoutine());
        }

        private void CompleteSpin()
        {
            _lastIndexes = _newIndexes;
        }

        private IEnumerator StartSpinRoutine()
        {
            float moveDistance = _startSpinDuration * _speed;
            
            var moveSubRoutine = AnimationMoveSubRoutine(moveDistance, _startSpinDuration, 
                IsUsingCustomStartSpinCurve, false, _startSpinCurve, _startSpinEase);
            
            foreach (var waiter in moveSubRoutine) 
                yield return waiter;
            
            //_spinRoutine = StartCoroutine(SpinRoutine());
            _startSpinRoutine = null;
        }
        
        private IEnumerator SpinRoutine()
        {
            float moveDistance = _justSpinDuration * _speed;
            
            var moveSubRoutine = AnimationMoveSubRoutine(moveDistance, _justSpinDuration, 
                false, false, null, Ease.Linear);
            
            foreach (var waiter in moveSubRoutine) 
                yield return waiter;
            
            //_completeSpinRoutine = StartCoroutine(CompleteSpinRoutine());
            _spinRoutine = null;
        }

        private IEnumerator CompleteSpinRoutine()
        {
            float upSlotY = _slots[^1].Rect.anchoredPosition.y;
            float moveDistance = upSlotY + HalfHeight - _slots[0].HeightHalf;

            _slots[^1].Setup(_newIndexes[0]);
            _reSetupIndex = 1;
            
            var moveSubRoutine = AnimationMoveSubRoutine(moveDistance, _completeDuration, 
                IsUsingCustomCompleteSpinCurve, true, _completeSpinCurve, _completeEase);
            
            foreach (var waiter in moveSubRoutine) 
                yield return waiter;

            _completeSpinRoutine = null;
            SlotsTakeYourPlaces();
            CompleteSpin();
        }

        private IEnumerable AnimationMoveSubRoutine(float moveDistance, float duration, bool useCustomCurve, bool canReSetup,
            AnimationCurve curve, Ease ease)
        {
            float timePassed = 0f;
            float lastPos = 0f;

            while (timePassed < duration)
            {
                yield return null;

                timePassed += Time.deltaTime;

                float newPos = !useCustomCurve
                    ? DOVirtual.EasedValue(0, moveDistance, timePassed / duration, ease)
                    : moveDistance - curve.Evaluate(timePassed / duration) * moveDistance;

                float delta = newPos - lastPos;
                lastPos = newPos;

                foreach (SlotCell slot in _slots)
                    slot.MoveVertical(delta * _direction);

                UpdateSlotsByPosition(canReSetup);
            }
        }

        private void SlotsTakeYourPlaces()
        {
            float slotHeight = _slots[0].Rect.rect.height;
            float posY = -HalfHeight + slotHeight / 2f;

            foreach (var slot in _slots)
            {
                slot.SetPosition(posY);
                posY += slotHeight;
            }
        }

        private void UpdateSlotsByPosition(bool canReSetup)
        {
            if (_direction < 0)
            {
                if (!_slots[0].IsVisibleFromRect(Rect))
                {
                    MoveFirstToEnd();
                    SlotCell slot = FirstToLastElement();
                    
                    if (canReSetup)
                    {
                        slot.Setup(_newIndexes[_reSetupIndex]);
                        _reSetupIndex = ++_reSetupIndex % _newIndexes.Length;
                    }
                }
            }
        }

        private SlotCell FirstToLastElement()
        {
            var temp = _slots[0];

            for (int i = 1; i < _slots.Length; i++) 
                _slots[i - 1] = _slots[i];
            
            _slots[^1] = temp;
            return temp;
        }

        private void MoveFirstToEnd()
        {
            var lastCellRect = _slots[^1].Rect;
            var cellRect = _slots[0].Rect;

            cellRect.anchoredPosition = new Vector2(cellRect.anchoredPosition.x, lastCellRect.anchoredPosition.y + lastCellRect.rect.height);
        }
    }
}