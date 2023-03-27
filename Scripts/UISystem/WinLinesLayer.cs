using System.Collections.Generic;
using Balance;
using SlotMachineStuff;
using UnityEngine;
using Utilities.Pool;
using Utilities.PoolCustom;

namespace UISystem
{
    public class WinLinesLayer : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _rect;
        [SerializeField]
        private UILineRendererHolder _lineRendererPrefab;

        [Header("Data Source")]
        [SerializeField]
        private SlotMachine _slotMachine;
        [SerializeField]
        private SlotCell _slotCell;

        private IPoolService _poolService;
        private IBalanceService _balanceService;

        private readonly Dictionary<int, UILineRendererHolder> _activeLines = new();

        public void Initialize(IPoolService poolService, IBalanceService balanceService)
        {
            _poolService = poolService;
            _balanceService = balanceService;
        }
        
        public void ShowLine(int index)
        {
            if (_activeLines.ContainsKey(index))
                return;

            var line = _poolService.Get(_lineRendererPrefab, parent: _rect);
            _activeLines.Add(index, line);

            line.Rect.anchoredPosition = new Vector2(_rect.rect.width / -2f, _rect.rect.height / 2f);

            var slotIndexes = _balanceService.LinesData.Lines[index].CellIndexes;
            var slotRect = _slotCell.Rect.rect;

            List<Vector2> points = new()
            {
                new Vector2(0, (slotIndexes[0].Y + 0.5f) * slotRect.height * -1f),
            };

            foreach (var slotIndex in slotIndexes)
            {
                points.Add(new Vector2(
                    (slotIndex.X + 0.5f) * slotRect.width + slotIndex.X * _slotMachine.Spacing, 
                    (slotIndex.Y + 0.5f) * slotRect.height * -1f));
            }
            
            points.Add(new Vector2(
                (slotIndexes[^1].X + 1) * slotRect.width + slotIndexes[^1].X * _slotMachine.Spacing, 
                (slotIndexes[^1].Y + 0.5f) * slotRect.height * -1f));

            line.LineRenderer.Points = points.ToArray();
        }

        public bool IsLineActive(int index)
            => _activeLines.ContainsKey(index);

        public void HideLine(int index)
        {
            if (!_activeLines.TryGetValue(index, out var line))
                return;
            
            _poolService.Free(line, false);
            _activeLines.Remove(index);
        }

        public void HideAll()
        {
            foreach (var line in _activeLines.Values)
            {
                _poolService.Free(line, false);
            }
            
            _activeLines.Clear();
        }
    }
}