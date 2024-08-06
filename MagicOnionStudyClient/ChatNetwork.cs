using Grpc.Net.Client;
using MagicOnion.Client;
using MagicOnion.Serialization.MemoryPack;
using Shared.Hubs;
using Shared.Packets;
using Shared.Util;
using System.Security.Cryptography.X509Certificates;

namespace MagicOnionStudyClient
{
    public class ChatNetwork
    {
        public IChatub ChatHub { get; set; }

        public ChatNetwork()
        {

        }

        public async Task ConnectAsync(GrpcChannel channel)
        {
            ChatHub = await StreamingHubClient.ConnectAsync<IChatub, IChatHubReceiver>(channel, new ChatHubReceiver(),
                serializerProvider: MemoryPackMagicOnionSerializerProvider.Instance);

            Logger.Log($"ConnectAsync, Connection is Success");
        }

        public async Task Login(string nickname)
        {
            var response = await ChatHub.Login(nickname);
            Logger.Log($"Login, code:: {response.Code}, userId:: {response.UserId}");

            if (response.Code == 0)
            {
                ChatClient.Instance.User.UserId = response.UserId;
            }
            else
            {
                Logger.Log("Login Fail !!");
            }
        }

        public async Task SendMessage(string message)
        {
            var response = await ChatHub.SendMessage(new ReqChatPacket()
            {
                UserId = ChatClient.Instance.User.UserId,
                Nickname = ChatClient.Instance.User.Nickname,
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
