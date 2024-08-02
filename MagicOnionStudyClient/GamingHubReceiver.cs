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

        public void OnSendMessage(BroadCastPacket message)
        {
            Logger.Log($"{message.Sender}, {message.BroadCastMessage}");
        }
    }
}
