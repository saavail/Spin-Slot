using UnityEngine;

namespace Balance
{
    [CreateAssetMenu(menuName = "Scriptables/" + nameof(SlotsData), fileName = nameof(SlotsData))]
    public class SlotsData : ScriptableObject
    {
        [SerializeField]
        private SlotData[] _slots;
        
        public SlotData[] Slots => _slots;
    }
}