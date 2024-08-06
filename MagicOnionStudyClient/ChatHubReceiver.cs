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
            ChatClient.Instance.BroadCastPackets.Add(packet);

            ChatClient.Instance.DisplayCommand();
            ChatClient.Instance.WriteMessage();
        }
    }
}
