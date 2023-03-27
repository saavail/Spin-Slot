using System;
using System.Linq;
using Balance;
using Extensions;
using UnityEngine;

namespace Backend
{
    [Serializable]
    public struct SpinResponseData
    {
        public const int DefaultDrumsCount = 5;

        [SerializeField]
        private int _coinsForLine;
        [SerializeField]
        private int _linesCount;
        [SerializeField]
        private int _coinValue;
        [SerializeField]
        private DrumResponseData[] _drums;
        [SerializeField]
        private WinLineResponseData[] _winLines;
        [SerializeField]
        private int _sumMultipliers;
        [SerializeField]
        private int _sumWinValue;
        [SerializeField]
        private int _walletBalance;

        public int CoinsForLine => _coinsForLine;
        public int LinesCount => _linesCount;
        public int CoinValue => _coinValue;
        public DrumResponseData[] Drums => _drums;
        public WinLineResponseData[] WinLines => _winLines;
        public int SumMultipliers => _sumMultipliers;
        public int SumWinValue => _sumWinValue;
        public int WalletBalance => _walletBalance;
        
        public SpinResponseData(int coinsForLine, int linesCount, int coinValue, DrumResponseData[] drums, WinLineResponseData[] winLines, 
            int sumMultipliers, int sumWinValue, int walletBalance)
        {
            _coinsForLine = coinsForLine;
            _linesCount = linesCount;
            _coinValue = coinValue;
            _winLines = winLines;
            _sumMultipliers = sumMultipliers;
            _sumWinValue = sumWinValue;
            _walletBalance = walletBalance;
            _drums = drums;
        }
        
        public SpinResponseData(int coinsForLine, int linesCount, int coinValue, int[][] drums, WinLineResponseData[] winLines, 
            int sumMultipliers, int sumWinValue, int walletBalance)
        {
            _coinsForLine = coinsForLine;
            _linesCount = linesCount;
            _coinValue = coinValue;
            _winLines = winLines;
            _sumMultipliers = sumMultipliers;
            _sumWinValue = sumWinValue;
            _walletBalance = walletBalance;

            _drums = drums.Select(i => new DrumResponseData(i)).ToArray();
        }

        public bool IsFreeSpins() 
            => CountScatters() > 2;

        public bool Have2Scatters()
            => CountScatters() == 2;

        public bool IsWinnable() 
            => WinLines.Length > 0;

        public bool IsEmpty()
            => WinLines.Length == 0;

        public bool IsBigWin() 
            => false;

        public int CountScatters() 
            => _drums.SelectMany(i => i.Indexes).Count(i => i == SlotType.Scatter.ToSlotIndex());
    }
}