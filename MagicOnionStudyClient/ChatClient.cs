using Grpc.Net.Client;
using Shared.Util;

namespace MagicOnionStudyClient
{
    public class ChatClient : Singleton<ChatClient>
    {
        private ChatClient()
        {

        }

        private Network Network { get; set; }
        public bool IsRunning { get; set; } = false;
        public string Nickname { get; set; }

        public async Task ConnectAsync()
        {
            var address = "http://localhost:5000";
            Logger.Log($"Connect this Url ->> {address}");

            var channel = GrpcChannel.ForAddress(address);
            Logger.Log("Start Connection...");

            Network = new();

            await Network.ConnectAsync(channel);
        }

        public async Task Login()
        {
            Console.Write("[Enter Your Nickname] >>>> ");

            var nickname = Console.ReadLine();

            Nickname = nickname;
            await Network.Login(Nickname);
        }

        public async Task SendChat()
        {
            while (IsRunning)
            {
                var message = Console.ReadLine();

                if (message == "exit")
                {
                    await Network.DisposeAsync();
                }
                else
                {
                    await Network.SendMessage(message);
                }
            }
        }
    }
}