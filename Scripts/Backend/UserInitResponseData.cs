using System;
using UnityEngine;

namespace Backend
{
    [Serializable]
    public struct UserInitResponseData
    {
        [SerializeField]
        private int[] _coinPossibleValues;
        [SerializeField]
        private int _walletBalance;

        public int[] CoinPossibleValues => _coinPossibleValues;
        public int WalletBalance => _walletBalance;

        public UserInitResponseData(int[] coinPossibleValues, int walletBalance)
        {
            _coinPossibleValues = coinPossibleValues;
            _walletBalance = walletBalance;
        }
    }
}