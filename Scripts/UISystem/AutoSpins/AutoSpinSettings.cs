namespace UISystem.AutoSpins
{
    public struct AutoSpinSettings
    {
        public int SpinsCount { get; set; }
        public bool StopIfWin { get; set; }
        public bool StopIfFreeSpins { get; set; }

        public AutoSpinSettings(int spinsCount, bool stopIfWin, bool stopIfFreeSpins)
        {
            SpinsCount = spinsCount;
            StopIfWin = stopIfWin;
            StopIfFreeSpins = stopIfFreeSpins;
        }
    }
}