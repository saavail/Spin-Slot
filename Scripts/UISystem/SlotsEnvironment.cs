using System;
using Balance;
using UnityEngine;

namespace UISystem
{
    public class SlotsEnvironment : MonoBehaviour
    {
        [SerializeField]
        private WinLineVisualiseButton[] _visualiseWinLineButtons;

        public void Initialize(WinLinesLayer winLinesLayer, WinSlotsLayer winSlotsLayer, IBalanceService balanceService)
        {
            for (var i = 0; i < _visualiseWinLineButtons.Length && i < balanceService.LinesData.Lines.Length; i++)
            {
                _visualiseWinLineButtons[i].Initialize(winLinesLayer, winSlotsLayer, i);
            }
        }
    }
}