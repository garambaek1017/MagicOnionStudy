using MagicOnion;
using Shared.Packets;

namespace Shared.Hubs
{
    
    /// <summary>
    /// Client -> ServerAPI(streaming)
    /// </summary>
    public interface IGameHub : IStreamingHub<IGameHub, IGameHubReceiver>
    {
        ValueTask<ResLoginPacketResult> Login(string name);
    }
}
