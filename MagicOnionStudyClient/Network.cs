using Grpc.Net.Client;
using MagicOnion.Client;
using MagicOnion.Serialization.MemoryPack;
using Network.Hubs;
using Packets;
using Shared.Util;

namespace MagicOnionStudyClient
{
    public class Network
    {
        private IChatHub ChatHub { get; set; }

        public Network()
        {

        }

        public async Task ConnectAsync(GrpcChannel channel)
        {
            ChatHub = await StreamingHubClient.ConnectAsync<IChatHub, IChatHubReceiver>(channel, new ChatHubReceiver(),
                serializerProvider: MemoryPackMagicOnionSerializerProvider.Instance);

            Logger.Log($"ConnectAsync, Connection is Success");
        }

        public async Task Login(string nickname)
        {
            var req = new ReqLoginPacket()
            {
                Nickname = nickname
            };

            var res = await ChatHub.Login(req.ToJson());
            Logger.Log($"Login, code:: {res}");
        }

        public async Task SendMessage(string message)
        {
            var req = new ReqChatPacket()
            {
                Nickname = ChatClient.Instance.Nickname,
                Message = message
            };

            var res = await ChatHub.SendMessage(req.ToJson());
            Logger.Log($"SendMessage, code:: {res} ");
        }

        public async Task DisposeAsync()
        {
            await ChatHub.DisposeAsync();
            Logger.Log("DisposeAsync..");
        }

        public async Task Logout()
        {
            var req = new ReqLogoutPacket()
            {
                Nickname = ChatClient.Instance.Nickname,
            };

            var res = await ChatHub.Logout(req.ToJson());
            Logger.Log($"Logout, code:: {res}");

            var resLogoutPacketResult = res.ToObject<ResLogoutPacketResult>();
            if(resLogoutPacketResult.Code == Shared.ErrorCode.Success)
            {
                await DisposeAsync();
            }
        }
    }
}
