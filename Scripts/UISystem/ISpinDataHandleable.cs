using Backend;
using Cysharp.Threading.Tasks;

namespace UISystem
{
    public interface ISpinDataHandleable
    {
        UniTask HandleSpinData(SpinResponseData data);
        void CompleteSpin(SpinResponseData data);
    }
}