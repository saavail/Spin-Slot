using DependencyInjector;

namespace Backend
{
    public interface IBackendService : IService
    {
        void Initialize();
        UserInitResponseData SendInitRequest();
        SpinResponseData SendSpinRequest(SpinRequestData request);
        string SendFreeSpinRequest();
        string SendReconnectRequest();
    }
}