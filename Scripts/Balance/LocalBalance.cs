using System.Linq;
using Cysharp.Threading.Tasks;
using EntryPoint;
using UnityEngine;

namespace Balance
{
    public class LocalBalance : IBalanceService, IAsyncInitializable
    {
        private const string LinesDataPath = "Data/LinesData";
        private const string SlotsDataPath = "Data/SlotsData";
        private const string AutoSpinsDataPath = "Data/AutoSpinsData";

        private readonly IResourceLoader _resourceLoader;

        public LinesData LinesData { get; private set; }
        public SlotsData SlotsData { get; private set; }
        public AutoSpinsData AutoSpinsData { get; private set; }

        public LocalBalance(IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
        }

        public async UniTask InitializeAsync()
        {
            var linesLoad = _resourceLoader.LoadAsync(LinesDataPath);
            var slotsLoad = _resourceLoader.LoadAsync(SlotsDataPath);
            var autoSpinsLoad = _resourceLoader.LoadAsync(AutoSpinsDataPath);

            (Object linesData, Object slotsData, Object autoSpinsData) = await (linesLoad, slotsLoad, autoSpinsLoad);
            
            LinesData = linesData as LinesData;
            SlotsData = slotsData as SlotsData;
            AutoSpinsData = autoSpinsData as AutoSpinsData;
        }

        public SlotIndex[] GetWinSlotIndexes(int lineIndex, int count) 
            => LinesData.Lines[lineIndex].CellIndexes.Take(count).ToArray();
    }
}