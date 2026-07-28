using Shared;

namespace Network.Hub
{
    // server -> client definition
    public interface IChatHubReceiver
    {
        void OnForceClose(ErrorCode errorCode);

        /// <summary>
        /// Server -> Client 
        /// </summary>
        /// <param name="message"></param>
        void OnSendReceiver(string message);
    }

}
