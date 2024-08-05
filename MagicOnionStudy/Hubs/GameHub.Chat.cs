using Shared.Packets;

namespace MagicOnionServer.Hubs
{
    public partial class GameHub
    {
        public async ValueTask<ResChatPacketResult> SendMessage(ReqChatPacket packet)
        {
            this._room.All.OnSendReceiver(new BroadCastPacket()
            {
                Sender = packet.Nickname,
                BroadCastMessage = packet.Message
            });

            // todo : 로그인 처리 
            var res = new ResChatPacketResult()
            {
                Code = 0,
            };

            return res;
        }

    }
}
