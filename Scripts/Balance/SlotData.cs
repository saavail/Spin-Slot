using System;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Balance
{
    [Serializable]
    public struct SlotData
    {
        [SerializeField, ReadOnly]
        private SlotType _slotType;
        
        [SerializeField, OnValueChanged(nameof(ValidateType))]
        private int _slotIndex;
        
        [SerializeField]
        private SlotRewardData[] _rewardValues;

        public SlotRewardData[] RewardValues => _rewardValues;
        public int Length => _rewardValues.Length;
        public int SlotIndex => _slotIndex;
        public SlotType SlotType => _slotType;

        private void ValidateType()
        {
            _slotType = _slotIndex.ToSlotType();
        }
    }
}