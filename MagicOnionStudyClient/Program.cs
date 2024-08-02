// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using MagicOnion.Client;
using MagicOnion.Serialization.MemoryPack;
using MagicOnionStudyClient;
using Shared.Hubs;
using Shared.Util;

var address = "http://localhost:5000";
Logger.Log($"MagicOnionStudyClient Start, Connect this Url ->> {address}");

var channel = GrpcChannel.ForAddress(address);
Logger.Log("Start Connection...");

// 커넥트 
var hub = await StreamingHubClient.ConnectAsync<IGameHub, IGameHubReceiver>(channel, new GamingHubReceiver(),
    serializerProvider: MemoryPackMagicOnionSerializerProvider.Instance);
Logger.Log($"ConnectionState : {channel.State}");

// 닉네임 받아서 로그인
Console.Write("Enter Your Nickname: ");
var nickname = Console.ReadLine();

var response = await hub.Login(nickname);
Logger.Log($"Your UserId: {response.UserId}, nickname :{response.Nickname}");

Logger.Log("If you input 'exit', the connection will be terminated.");
while (true)
{
    var cmd = Console.ReadLine();

    if (cmd == "exit")
    {
        await hub.DisposeAsync();
        Logger.Log("DisposeAsync");
        Logger.Log($"Connection State : {channel.State}");
    }
}
