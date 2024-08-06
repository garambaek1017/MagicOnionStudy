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
        public readonly int MAX_LINE = 100;

        public ChatNetwork Network { get; set; }

        public ChatUser User { get; set; }

        public bool IsRunning { get; set; } = false;
        public bool IsChatting { get; set; } = false;

        public int CallNumber = 0;

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

        public void Clear()
        {
            Console.Clear();
        }

        public async Task ConnectAsync()
        {
            var address = "http://localhost:5000";
            Logger.Log($"MagicOnionStudyClient Start, Connect this Url ->> {address}");

            var channel = GrpcChannel.ForAddress(address);
            Logger.Log("Start Connection...");

            Network = new ChatNetwork();

            await Network.ConnectAsync(channel);
        }

        public void DisplayCommand()
        {
            Clear();
            //Console.WriteLine("MagicOnion Chatting Client::" + this.CallNumber++);
            DisplayUser();

            if (IsChatting == false)
            {
                Console.WriteLine("==========================");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Chat");
                Console.WriteLine("3. Exit");
                Console.WriteLine("==========================");
                Console.Write("[Input number] >>>> ");
            }
            else
            {
                Console.WriteLine("==========================");
                Console.WriteLine("1. Login");
                Console.WriteLine("3. Exit");
                Console.WriteLine("==========================");
            }
        }

        public void DisplayUser()
        {
            Console.WriteLine("==========================");
            if (User != null)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.WriteLine($" #### [{User.Nickname}] #### ");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine($" #### [Please Login or Connect] #### ");
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("==========================");
        }

        public async void Login()
        {
            Console.Write("[Enter Your Nickname] >>>> ");

            var nickname = Console.ReadLine();
            User = new ChatUser()
            {
                Nickname = nickname
            };

            if (Network == null)
            {
                await ConnectAsync();
            }

            await Network.Login(User.Nickname);
        }

        public async void SendChat()
        {
            var message = Console.ReadLine();
            await Network.SendMessage(message);
        }

        public async void Dispose()
        {
            await Network.DisposeAsync();
            Logger.Log("Connection State : Disconnected");
        }

        public void Running()
        {
            while (IsRunning)
            {
                DisplayCommand();

                var cmd = Console.ReadLine();
                if (cmd == "1" || cmd.ToLower() == "login")
                {
                    Login();
                }
                else if (cmd.ToLower() == "chat" || cmd == "2")
                {
                    IsChatting = true;
                    while (IsChatting)
                    {
                        SendChat();
                    }
                }
                else if (cmd.ToLower() == "exit" || cmd == "3")
                {
                    Dispose();

                    Logger.Log("Connection State : Disconnected");

                    IsRunning = false;
                }
            }
        }
    }
}
