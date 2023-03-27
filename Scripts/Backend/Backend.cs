namespace Backend
{
    public class Backend : IBackendService
    {
        public void Initialize()
        {
        }

        public UserInitResponseData SendInitRequest()
        {
            throw new System.NotImplementedException();
        }

        public SpinResponseData SendSpinRequest(SpinRequestData request)
        {
            throw new System.NotImplementedException();
        }

        public string SendFreeSpinRequest()
        {
            throw new System.NotImplementedException();
        }

        public string SendReconnectRequest()
        {
            throw new System.NotImplementedException();
        }
    }
}