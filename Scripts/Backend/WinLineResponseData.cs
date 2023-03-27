using System;
using UnityEngine;

namespace Backend
{
    [Serializable]
    public struct WinLineResponseData
    {
        [SerializeField]
        private int _index;
        [SerializeField]
        private int _slotIndex;
        [SerializeField]
        private int _slotsCount;
        [SerializeField]
        private int _coins;

        public int Index => _index;
        public int SlotIndex => _slotIndex;
        public int SlotsCount => _slotsCount;
        public int Coins => _coins;
        
        public WinLineResponseData(int index, int slotIndex, int slotsCount, int coins)
        {
            _index = index;
            _slotIndex = slotIndex;
            _slotsCount = slotsCount;
            _coins = coins;
        }
    }
}