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
        
        protected override async ValueTask OnDisconnected()
        {
            Logger.Log($"[ChatHub:OnDisconnected] ConnectionId:{ConnectionId} is disconnected.");

            UserManager.Instance.RemoveUser(Context.ContextId);

            if (_room is not null)
            {
                try
                {
                    await _room.RemoveAsync(Context);
                }
                catch (Exception ex)
                {
                    Logger.Log($"Remove group failed on disconnected: {ex.Message}");
                }
            }
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
