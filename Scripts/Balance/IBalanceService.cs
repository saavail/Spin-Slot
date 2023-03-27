using DependencyInjector;

namespace Balance
{
    public interface IBalanceService : IService
    {
        LinesData LinesData { get; }
        SlotsData SlotsData { get; }
        AutoSpinsData AutoSpinsData { get; }

        SlotIndex[] GetWinSlotIndexes(int lineIndex, int count);
    }
}