using Grpc.Net.Client;
using MagicOnion.Client;
using MagicOnion.Serialization.MemoryPack;
using Shared.Hubs;
using Shared.Packets;
using Shared.Util;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace MagicOnionStudyClient
{
    public class ChatClient : Singleton<ChatClient>
    {
        public Network Network { get; set; }
        public bool IsRunning { get; set; } = false;
        public string Nickname { get; set; }

        private ChatClient()
        {

        }

        public List<BroadCastPacket> BroadCastPackets { get; set; } = new List<BroadCastPacket>();

        public void WriteMessage()
        {
            foreach (var broadCastPacket in BroadCastPackets)
            {
                Console.WriteLine($">>>> [{broadCastPacket.Sender}]:{broadCastPacket.BroadCastMessage}");
            }
        }

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

        public async Task Dispose()
        {
            await Network.DisposeAsync();
            Logger.Log("Connection State : Disconnected");
        }
    }
}
