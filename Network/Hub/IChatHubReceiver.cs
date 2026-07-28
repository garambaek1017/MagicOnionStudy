using Shared;

namespace Network.Hub
{
    // server -> client definition
    public interface IChatHubReceiver
    {
        // todo : error Code를 enum으로 바꾸자 
        void OnForceClose(ErrorCode errorCode);

        /// <summary>
        /// Server -> Client 
        /// </summary>
        /// <param name="message"></param>
        void OnSendReceiver(string message);
    }

}
