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
Console.Write(">>>> Enter Your Nickname: ");
var nickname = Console.ReadLine();
GameHubClient.Instance.SetNickname(nickname);

while (true)
{
    if (GameHubClient.Instance.ClientState == ClientState.None)
    {
        ShowMenu();

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
        else if (cmd.ToLower() == "Chat" || cmd == "3")
        {
            GameHubClient.Instance.ClientState = ClientState.Chat;
        }
    }
    else
    {
       
    }
}

void ShowMenu()
{

    Console.Clear();

    Console.WriteLine("==========================");

    if (!string.IsNullOrEmpty(GameHubClient.Instance.Nickname))
    {
        Console.WriteLine($" #### [{GameHubClient.Instance.Nickname}] #### ");
        Console.WriteLine("==========================");
    }
    Console.WriteLine("1. Login");
    Console.WriteLine("2. Exit");
    Console.WriteLine("3. Chat");
    Console.WriteLine("==========================");
}

static void ShowChat()
{
    Console.Write("Input Message >>> ");
    var message = Console.ReadLine();

    GameHubClient.Instance.SendMessage(message);

    ShowMenu();

    foreach (var msg in GameHubClient.Instance.Messages)
    {
        Console.WriteLine($">>>> {msg}");
    }
}
