using System.Threading.Tasks;
using MagicOnion;

namespace Network.Hub
{
    /// <summary>
    /// Client -> Server 
    /// </summary>
    public interface IChatHub : IStreamingHub<IChatHub, IChatHubReceiver>
    {
        // Client, login to Server
        ValueTask<string> Login(string pkt);

        // Client, SendMessage to Server
        ValueTask<string> SendMessage(string pkt);

        // Client, Logout to Server
        ValueTask<string> Logout(string pkt);
    }
}
