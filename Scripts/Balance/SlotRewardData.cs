using System;
using UnityEngine;

namespace Balance
{
    [Serializable]
    public struct SlotRewardData
    {
        [SerializeField]
        private int _count;
        
        [SerializeField]
        private int _value;
        
        public int Count => _count;
        public int Value => _value;
    }
}