using MagicOnion.Server.Hubs;
using MagicOnionServer.Manager;
using Network.Hub;
using Packets;
using Shared;
using Shared.Util;

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
            
            var broadCastPacket = new BroadCastPacket()
            {
                Sender = name,
                BroadCastMessage = message,
            };
            
            _room.All.OnSendReceiver(broadCastPacket.ToJson());
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

                if(guid != Guid.Empty)
                {
                    this._room.Single(guid).OnForceClose(ErrorCode.Success);
                }
            }
        }
       
        
    }
}
