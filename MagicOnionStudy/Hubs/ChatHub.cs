using MagicOnion.Server.Hubs;
using Shared.Hubs;

namespace MagicOnionServer.Hubs
{
    public partial class ChatHub : StreamingHubBase<IChatub, IChatHubReceiver>, IChatub
    {
        private IGroup<IChatHubReceiver> _room;

        protected override ValueTask OnConnected()
        {
            Console.WriteLine($"[ChatHub:OnConnected] ConnectionId:{ConnectionId} is connected.");
            return ValueTask.CompletedTask;
        }

        protected override ValueTask OnDisconnected()
        {
            Console.WriteLine($"[ChatHub:OnConnected] ConnectionId:{ConnectionId} is disconnected.");
            return ValueTask.CompletedTask;
        }

        private void BroadCast(string name, string message)
        {
            this._room.All.OnSendReceiver(new BroadCastPacket()
            {
                Sender = name,
                BroadCastMessage = message, 
            });
        }
    }
}
