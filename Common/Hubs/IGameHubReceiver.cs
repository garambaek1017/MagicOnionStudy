using Shared.Packets;

namespace Shared.Hubs
{
    // server -> client definition
    public interface IGameHubReceiver
    {
        // todo : error Code를 enum으로 바꾸자 
        void OnForceClose(int errorCode);

        void OnSendReeiver(BroadCastPacket message);
    }
}
