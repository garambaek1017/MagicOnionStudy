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
        public ClientState ClientState { get; set; }
        private IGameHub _hub { get; set; }
        public string Nickname { get; set; }
        private int _userId { get; set; }
        public List<string> Messages { get; set; } = new List<string>();

        private GameHubClient()
        {
            ClientState = ClientState.None;
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

        public async Task Login()
        {
            var response = await _hub.Login(Nickname);
            Logger.Log($"Login, code:: {response.Code}, userId:: {response.UserId}");

            if (response.Code == 0)
            {
                _userId = response.UserId;
            }
            else
            {
                Logger.Log("Login Fail !!");
            }
        }

        public async Task SendMessage(string message)
        {
            var response = await _hub.SendMessage(new ReqChatPacket()
            {
                UserId = _userId,
                Nickname = Nickname,
                Message = message   
            });

            if(response.Code != 0)
            {
                Logger.Log("SendMessage Fail !!");
            }
        }

        public async void DisposeAsync()
        {
            await _hub.DisposeAsync();
            Logger.Log("DisposeAsync..");
        }

    }
}
