using MagicOnion;
using Shared.Packets;

namespace Shared.Hubs
{
    /// <summary>
    /// Client -> Server 
    /// </summary>
    public interface IChatHub : IStreamingHub<IChatHub, IChatHubReceiver>
    {
        // Client, login to Server
        ValueTask<ResLoginPacketResult> Login(ReqLoginPacket pkt);

        // Client, SendMessage to Server
        ValueTask<ResChatPacketResult> SendMessage(ReqChatPacket pkt);

        // Client, Logout to Server
        ValueTask<ResLogoutPacketResult> Logout(ReqLogoutPacket pkt);
    }
}
