using Grpc.Net.Client;
using MagicOnion.Client;
using MagicOnion.Serialization.MemoryPack;
using Shared.Hubs;
using Shared.Packets;
using Shared.Util;

namespace MagicOnionStudyClient
{
    public class GameHubClient : Singleton<GameHubClient>
    {
        private IGameHub _hub { get; set; }

        private string Nickname { get; set; }

        private GameHubClient()
        {

        }

        public async Task ConnectAsync(GrpcChannel channel)
        {
             _hub = await StreamingHubClient.ConnectAsync<IGameHub, IGameHubReceiver>(channel, new GamingHubReceiver(),
                serializerProvider: MemoryPackMagicOnionSerializerProvider.Instance);

            Logger.Log($"ConnectAsync, Connection is Success");
        }

        public void SetNickname(string nickname)
        {
            Nickname = nickname;
            Logger.Log($"SetNickname, Nickname : {Nickname}");
        }

        public async void Login()
        {
            var response = await _hub.Login(Nickname);
            Logger.Log($"Login, code:: {response.Code}, userId:: {response.UserId}");
        }

        public async void DisposeAsync()
        {
            await _hub.DisposeAsync();
            Logger.Log("DisposeAsync..");
        }

    }
}
