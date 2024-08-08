using Shared;
using Shared.Hubs;
using Shared.Packets;

namespace MagicOnionStudyClient
{
    /// <summary>
    /// Receiver From Server 
    /// </summary>
    public class ChatHubReceiver : IChatHubReceiver
    {
        public async Task OnForceClose(ErrorCode errorCode)
        {
            await ChatClient.Instance.Logout();
        }

        public void OnSendReceiver(BroadCastPacket packet)
        {
            Console.WriteLine($"[>>>] Sender:{packet.Sender}, Message:{packet.BroadCastMessage}");
        }
    }
}
