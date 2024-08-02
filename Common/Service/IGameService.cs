using MagicOnion;
using Shared.Packets;

namespace Shared.Service
{
    /// <summary>
    /// Client -> ServerAPI 
    /// </summary>
    public interface IGameService : IService<IGameService>
    {
        ValueTask<ResLoginPacketResult> Login(string name);
    }
}
