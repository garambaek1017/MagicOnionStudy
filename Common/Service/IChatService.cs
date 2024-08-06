using MagicOnion;
using Shared.Packets;

namespace Shared.Service
{
    /// <summary>
    /// Client -> ServerAPI 
    /// </summary>
    public interface IChatService : IService<IChatService>
    {
        ValueTask<ResLoginPacketResult> Login(string name);
    }
}
