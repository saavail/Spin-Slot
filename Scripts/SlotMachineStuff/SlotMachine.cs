using System.Linq;
using System.Threading;
using Backend;
using Balance;
using Cysharp.Threading.Tasks;
using Graphics;
using UnityEngine;
using UnityEngine.UI;

namespace SlotMachineStuff
{
    public class SlotMachine : MonoBehaviour
    {
        public const int VisibleSlots = 3;
        
        [SerializeField]
        private SpinningDrum[] _drums;
        [SerializeField]
        private HorizontalLayoutGroup _drumsGroup;

        [Header("Settings")]
        [SerializeField]
        private float _justSpinDuration = 1f;

        public float Spacing => _drumsGroup.spacing;
        public SpinningDrum[] Drums => _drums;

        public void Initialize(IIconsService iconsService, IBalanceService balanceService)
        {
            foreach (var drum in _drums) 
                drum.Initialize(iconsService, balanceService);
        }

        // public void Spin(DrumResponseData[] drumsData)
        // {
        //     if (_drums.Any(i => i.IsSpinning))
        //     {
        //         FastCompleteSpin();
        //         return;
        //     }
        //     
        //     float justSpinDuration = _justSpinDuration;
        //
        //     for (var i = 0; i < _drums.Length; i++)
        //     {
        //         _drums[i].Spin(drumsData[i].Indexes, justSpinDuration);
        //         justSpinDuration += _justSpinDuration;
        //     }
        // }
        
        public async UniTask SpinAsync(DrumResponseData[] drumsData, CancellationToken tokenSourceToken)
        {
            if (_drums.Any(i => i.IsSpinning))
            {
                FastCompleteSpin();
                return;
            }
            
            float justSpinDuration = _justSpinDuration;

            for (var i = 0; i < _drums.Length - 1; i++)
            {
                _drums[i].SpinAsync(drumsData[i].Indexes, justSpinDuration).Forget();
                justSpinDuration += _justSpinDuration;
            }

            await _drums[^1].SpinAsync(drumsData[^1].Indexes, justSpinDuration);
        }

        public void FastCompleteSpin()
        {
            foreach (var drum in _drums) 
                drum.FastCompleteSpin();
        }
    }
}