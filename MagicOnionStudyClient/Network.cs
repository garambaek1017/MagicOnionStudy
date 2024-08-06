using Grpc.Net.Client;
using MagicOnion.Client;
using MagicOnion.Serialization.MemoryPack;
using Shared.Hubs;
using Shared.Packets;
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
            var reqPkt = new ReqLoginPacket()
            {
                Nickname = nickname
            };

            var response = await ChatHub.Login(reqPkt);
            Logger.Log($"Login, code:: {response.Code}, userId:: {response.UserId}");
        }

        public async Task SendMessage(string message)
        {
            var response = await ChatHub.SendMessage(new ReqChatPacket()
            {
                Nickname = ChatClient.Instance.Nickname,
                Message = message
            });

            if (response.Code != 0)
            {
                Logger.Log("SendMessage Fail !!");
            }
        }

        public async Task DisposeAsync()
        {
            await ChatHub.DisposeAsync();
            Logger.Log("DisposeAsync..");
        }
    }
}
