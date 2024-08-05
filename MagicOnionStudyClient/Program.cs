// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using MagicOnionStudyClient;
using Shared.Util;

var address = "http://localhost:5000";
Logger.Log($"MagicOnionStudyClient Start, Connect this Url ->> {address}");

var channel = GrpcChannel.ForAddress(address);
Logger.Log("Start Connection...");

// 커넥트 
Logger.Log($"ConnectionState : {channel.State}");
await GameHubClient.Instance.ConnectAsync(channel);

// 닉네임 받아서 로그인
Logger.Log(">>>> Enter Your Nickname: ");
var nickname = Console.ReadLine();
GameHubClient.Instance.SetNickname(nickname);

// 3초후 
Task.Delay(3000);

ShowMenu();

while (true)
{
    var cmd = Console.ReadLine();

    if (cmd == "1" || cmd.ToLower() == "login")
    {
        GameHubClient.Instance.Login();
    }
    else if (cmd.ToLower() == "exit" || cmd == "2")
    {
        GameHubClient.Instance.DisposeAsync();
        Logger.Log($"Connection State : {channel.State}");
    }
}


void ShowMenu()
{
    Console.Clear();

    Logger.Log("==========================");
    Logger.Log("1. Login");
    Logger.Log("2. Exit");
    Logger.Log("==========================");
}
