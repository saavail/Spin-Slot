using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Backend
{
    [Serializable]
    public struct DrumResponseData
    {
        public const int DefaultCount = 3;
        
        [SerializeField]
        private int[] _indexes;

        public int[] Indexes => _indexes;

        public DrumResponseData(int[] indexes)
        {
            _indexes = indexes;
        }
    }
}