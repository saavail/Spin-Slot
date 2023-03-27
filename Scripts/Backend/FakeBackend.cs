using EntryPoint;
using Utilities;
using LogType = EntryPoint.LogType;

namespace Backend
{
    public class FakeBackend : AsyncInitializableAndLoad<FakeBackendData>, IBackendService
    {
        private readonly IGameLoggerService _logger;

        protected override string LoadPath => nameof(FakeBackendData);

        public FakeBackend(IResourceLoader resourceLoader, IGameLoggerService logger)
            : base(resourceLoader)
        {
            _logger = logger;
        }
        
        public void Initialize()
        {
            _logger.Log(LogType.Production, $"{GetType().Name} initialized");
        }

        public UserInitResponseData SendInitRequest()
        {
            return Data.InitResponse;
        }

        public SpinResponseData SendSpinRequest(SpinRequestData request)
        {
            return Data.SpinResponses.RandomElement();
        }

        public string SendFreeSpinRequest()
        {
            return default;
        }

        public string SendReconnectRequest()
        {
            return default;
        }
    }
}