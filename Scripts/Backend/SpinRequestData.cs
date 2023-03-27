namespace Backend
{
    public struct SpinRequestData : IRequestFormatter
    {
        public int CoinsForLine { get; set; }
        public int LinesCount { get; }
        public int CoinValue { get; }
        
        public SpinRequestData(int coinsForLine, int linesCount, int coinValue)
        {
            CoinsForLine = coinsForLine;
            LinesCount = linesCount;
            CoinValue = coinValue;
        }

        public string ToRequestFormat()
        {
            return default;
        }
    }
}