using UnityEngine;

namespace Balance
{
    [CreateAssetMenu(menuName = "Scriptables/" + nameof(AutoSpinsData), fileName = nameof(AutoSpinsData))]
    public class AutoSpinsData : ScriptableObject
    {
        [SerializeField]
        private int[] _counters;

        [Header("Default Values")]
        [SerializeField]
        private int _defaultCounterIndex;
        [SerializeField]
        private bool _defaultStopIfWin;
        [SerializeField]
        private bool _defaultStopIfFreeSpins;
        
        public int[] Counters => _counters;
        public int DefaultCounterIndex => _defaultCounterIndex;
        public bool DefaultStopIfWin => _defaultStopIfWin;
        public bool DefaultStopIfFreeSpins => _defaultStopIfFreeSpins;
    }
}