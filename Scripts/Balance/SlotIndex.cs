using System;
using UnityEngine;

namespace Balance
{
    [Serializable]
    public struct SlotIndex
    {
        [SerializeField]
        private int _x;
        [SerializeField]
        private int _y;

        public int X
        {
            get => _x;
            set => _x = value;
        }

        public int Y
        {
            get => _y;
            set => _y = value;
        }
    }
}