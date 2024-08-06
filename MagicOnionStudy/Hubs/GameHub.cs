using Cysharp.Runtime.Multicast;
using MagicOnion.Server.Hubs;
using Shared.Hubs;
using Shared.Packets;
using System.Numerics;

namespace MagicOnionServer.Hubs
{
    public partial class GameHub : StreamingHubBase<IChatub, IChatHubReceiver>, IChatub
    {
        private IGroup<IChatHubReceiver> _room;

        protected override ValueTask OnConnected()
        {
            Console.WriteLine($"[GameHub:OnConnected] ConnectionId:{ConnectionId} is connected.");
            return ValueTask.CompletedTask;
        }

        protected override ValueTask OnDisconnected()
        {
            Console.WriteLine($"[GameHub:OnConnected] ConnectionId:{ConnectionId} is disconnected.");
            return ValueTask.CompletedTask;
        }
    }
}
