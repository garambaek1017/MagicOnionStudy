using Shared.Hubs;
using Shared.Packets;
using System.Security.Cryptography.X509Certificates;

namespace MagicOnionStudyClient
{
    /// <summary>
    /// Receiver From Server 
    /// </summary>
    public class ChatHubReceiver : IChatHubReceiver
    {
        public void OnForceClose(int errorCode)
        {
            throw new NotImplementedException();
        }

        public void OnSendReceiver(BroadCastPacket packet)
        {
            if (packet.Sender == ChatClient.Instance.Nickname)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
            }
            Console.WriteLine($"[>>>] Sender:{packet.Sender}, Message:{packet.BroadCastMessage}");
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
