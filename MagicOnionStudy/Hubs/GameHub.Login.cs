using Shared.Packets;

namespace MagicOnionServer.Hubs
{
    public partial class GameHub
    {
        public async ValueTask<ResLoginPacketResult> Login(string name)
        {
            // 그룹에 유저 추가 
            this._room = await this.Group.AddAsync(Constant.RoomName);
            var newUserId = PlayerManager.Instance.AddPlayer(ConnectionId, name);

            this._room.All.OnSendReceiver(new BroadCastPacket()
            {
                Sender = name,
                BroadCastMessage = "Newbie is Connected"
            });

            // todo : 로그인 처리 
            var res = new ResLoginPacketResult
            {
                UserId = newUserId,
                Nickname = name
            };

            return res;
        }

    }
}
