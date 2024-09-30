using System.Threading.Tasks;
using Shared9.Packets;

namespace Shared9.Hubs
{
    public interface IGameHubReceiver
    {
        // todo : error Code를 enum으로 바꾸자 
        Task OnForceClose(ErrorCode errorCode);

        /// <summary>
        /// Server -> Client 
        /// </summary>
        /// <param name="message"></param>
        void OnSendReceiver(BroadCastPacket message);
        
        /// <summary>
        /// Server -> client Move Player
        /// </summary>
        /// <param name="packet"></param>
        void MovePlayer(BroadCastMovePlayerPacket packet);   
    }
}