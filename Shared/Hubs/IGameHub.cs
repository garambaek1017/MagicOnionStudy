using System.Threading.Tasks;
using MagicOnion;
using Shared9.Packets;

namespace Shared9.Hubs
{
    public interface IGameHub : IStreamingHub<IGameHub, IGameHubReceiver>
    {
        // Client, login to Server
        ValueTask<ResLoginPacketResult> Login();

        // Client, Logout to Server
        ValueTask<ResLogoutPacketResult> Logout(ReqLogoutPacket pkt);

        // Clilent, Move Player to Server
        ValueTask<ResMovePlayerPacketResult> MovePlayer(ReqMovePlayerPacket pkt);
    }
}