using MagicOnion.Server.Hubs;
using MagicOnionServer.Manager;
using Shared;
using Shared.Hubs;

namespace MagicOnionServer.Hubs
{
    public partial class ChatHub : StreamingHubBase<IChatHub, IChatHubReceiver>, IChatHub
    {
        private IGroup<IChatHubReceiver> _room;

        protected override ValueTask OnConnected()
        {
            Logger.Log($"[ChatHub:OnConnected] ConnectionId:{ConnectionId} is connected.");
            return ValueTask.CompletedTask;
        }

        protected override ValueTask OnDisconnected()
        {
            Logger.Log($"[ChatHub:OnDisconnected] ConnectionId:{ConnectionId} is disconnected.");
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

        private void OnForceClose(long userId = 0)
        {
            if (userId == 0)
            {
                // 전체 킥 
            }
            else
            {
                var guid = UserManager.Instance.GetConnectionId(userId);

                if(guid != null)
                {
                    this._room.Single(guid).OnForceClose(ErrorCode.Success);
                }
                
            }
        }
    }
}
