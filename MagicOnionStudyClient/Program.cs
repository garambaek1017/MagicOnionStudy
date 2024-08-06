// See https://aka.ms/new-console-template for more information
using MagicOnionStudyClient;

ChatClient.Instance.IsRunning = true;

// connect
await ChatClient.Instance.ConnectAsync();

// login
await ChatClient.Instance.Login();

// SendChat
Console.WriteLine("Start chatting. Feel free to write anything, if you want to exit, type 'exit' ");
Console.WriteLine("---------------------------------------------------");

await ChatClient.Instance.SendChat();