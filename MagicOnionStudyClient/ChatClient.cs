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

            Network = new Network();

            await Network.ConnectAsync(channel);
        }

        public async Task Login()
        {
            Console.Write("[Enter Your Nickname] >>>> ");

            var nickname = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nickname))
            {
                Logger.Log("Nickname is empty. Please restart client and enter nickname.");
                IsRunning = false;
                return;
            }

            Nickname = nickname;
            await Network.Login(Nickname);
        }

        public async Task SendChat()
        {
            while (IsRunning)
            {
                var message = Console.ReadLine();
                if (message is null)
                {
                    IsRunning = false;
                    break;
                }

                if(message is "logout" or "exit")
                {
                    await Logout();
                    IsRunning = false;
                }
                else
                {
                    await Network.SendMessage(message);
                }
            }
        }
        public async Task Logout()
        {
            await Network.Logout();
        }
    }
}
