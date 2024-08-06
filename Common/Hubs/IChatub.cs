using MagicOnion;
using Shared.Packets;

namespace Shared.Hubs
{
    /// <summary>
    /// Client -> Server 
    /// </summary>
    public interface IChatub : IStreamingHub<IChatub, IChatHubReceiver>
    {
        // Client, login to Server
        ValueTask<ResLoginPacketResult> Login(ReqLoginPacket pkt);

        // Client, SendMessage to Server
        ValueTask<ResChatPacketResult> SendMessage(ReqChatPacket chatPacket);
    }
}
