using Shared.Packets;

namespace Shared.Hubs
{
    // server -> client definition
    public interface IChatHubReceiver
    {
        // todo : error Code를 enum으로 바꾸자 
        Task OnForceClose(ErrorCode errorCode);

        /// <summary>
        /// Server -> Client 
        /// </summary>
        /// <param name="message"></param>
        void OnSendReceiver(BroadCastPacket message);

    }

}
