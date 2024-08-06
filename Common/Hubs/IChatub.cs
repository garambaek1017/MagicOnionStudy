using MagicOnion;
using Shared.Packets;

namespace Shared.Hubs
{
    
    /// <summary>
    /// Client -> ServerAPI(streaming)
    /// </summary>
    public interface IChatub : IStreamingHub<IChatub, IChatHubReceiver>
    {
        ValueTask<ResLoginPacketResult> Login(string name);

        ValueTask<ResChatPacketResult> SendMessage(ReqChatPacket chatPacket);
    }
}
