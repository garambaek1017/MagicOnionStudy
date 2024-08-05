using Shared.Hubs;
using Shared.Packets;
using Shared.Util;

namespace MagicOnionStudyClient
{
    /// <summary>
    /// Receiver From Server 
    /// </summary>
    public class GamingHubReceiver : IGameHubReceiver
    {
        public void OnForceClose(int errorCode)
        {
            throw new NotImplementedException();
        }

        public void OnSendReceiver(BroadCastPacket message)
        {
            GameHubClient.Instance.Messages.Add($"{message.Sender}, {message.BroadCastMessage}");
        }
    }
}
