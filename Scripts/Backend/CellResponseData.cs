using System;
using Balance;
using UnityEngine;

namespace Backend
{
    [Serializable]
    public struct CellResponseData
    {
        [SerializeField]
        private SlotIndex _slotIndex;
        [SerializeField]
        private int _valueIndex;

        public SlotIndex SlotIndex => _slotIndex;
        public int ValueIndex => _valueIndex;

        public CellResponseData(int valueIndex, SlotIndex slotIndex)
        {
            _valueIndex = valueIndex;
            _slotIndex = slotIndex;
        }
    }
}