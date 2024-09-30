using Network.Hubs;
using Packets;
using Shared;
using Shared.Util;

namespace MagicOnionStudyClient
{
    /// <summary>
    /// Receiver From Server 
    /// </summary>
    public class ChatHubReceiver : IChatHubReceiver
    {
        public async void OnForceClose(ErrorCode errorCode)
        {
            await ChatClient.Instance.Logout();
        }

        public void OnSendReceiver(string pkt)
        {
            var packet = pkt.ToObject<BroadCastPacket>();
            Console.WriteLine($"[>>>] Sender:{packet.Sender}, Message:{packet.BroadCastMessage}");
        }
    }
}
