using System.Collections.Generic;
using Balance;
using Graphics;
using SlotMachineStuff;
using UnityEngine;
using Utilities.Pool;

namespace UISystem
{
    public class WinSlotsLayer : MonoBehaviour
    {
        [SerializeField]
        private SlotCell _slotCellPrefab;

        private SlotMachine _slotMachine;
        private IPoolService _poolService;
        private IBalanceService _balanceService;
        private IIconsService _iconsService;

        private readonly Dictionary<SlotIndex, SlotCell> _activeSlots = new();

        public void Initialize(SlotMachine slotMachine, IPoolService poolService, IBalanceService balanceService, IIconsService iconsService)
        {
            _slotMachine = slotMachine;
            _poolService = poolService;
            _balanceService = balanceService;
            _iconsService = iconsService;
        }

        public void ShowLine(int lineIndex)
        {
            foreach (var slotIndex in _balanceService.LinesData.Lines[lineIndex].CellIndexes)
            {
                LightSlot(slotIndex);
            }
        }
        
        public void ShowLine(int lineIndex, int count)
        {
            foreach (var slotIndex in _balanceService.GetWinSlotIndexes(lineIndex, count))
            {
                LightSlot(slotIndex);
            }
        }

        public void HideLine(int lineIndex)
        {
            foreach (var slotIndex in _balanceService.LinesData.Lines[lineIndex].CellIndexes)
            {
                HideSlot(slotIndex);
            }
        }
        
        public void HideLine(int lineIndex, int count)
        {
            foreach (var slotIndex in _balanceService.GetWinSlotIndexes(lineIndex, count))
            {
                HideSlot(slotIndex);
            }
        }

        public void LightSlot(SlotIndex slotIndex)
        {
            if (_activeSlots.ContainsKey(slotIndex))
                return;
            
            var sourceSlot = _slotMachine.Drums[slotIndex.X].Slots[SlotMachine.VisibleSlots - slotIndex.Y - 1];

            var slot = _poolService.Get(_slotCellPrefab, out bool isNew, sourceSlot.transform.position, Quaternion.identity, parent: transform);
            _activeSlots.Add(slotIndex, slot);
            
            if (isNew)
                slot.Initialize(_iconsService);
            
            slot.Setup(sourceSlot.Index);
            slot.ShowAnimateWin();
        }

        public void HideSlot(SlotIndex slotIndex)
        {
            if (!_activeSlots.TryGetValue(slotIndex, out var slot))
                return;
            
            _poolService.Free(slot);
            _activeSlots.Remove(slotIndex);
        }

        public void HideAll()
        {
            foreach (var slot in _activeSlots.Values)
            {
                _poolService.Free(slot);
            }
            
            _activeSlots.Clear();
        }
    }
}