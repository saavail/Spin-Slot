using System;
using UnityEngine;

namespace Balance
{
    [Serializable]
    public struct LineData
    {
        [SerializeField]
        private int _index;
        
        [SerializeField]
        private SlotIndex[] _cellIndexes;

        public SlotIndex[] CellIndexes => _cellIndexes;
        public int Index => _index;
    }
}