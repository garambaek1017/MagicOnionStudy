using System.Threading.Tasks;
using Packets;
using Shared.Util;
using Uitility;
using Util;

public partial class HubClient
{
    public async Task SendMessageAsync(string userName, string message)
    {
        var req = new ReqChatPacket()
        {
            Nickname = userName,
            Message = message,
        };

        var res = await _hub.SendMessage(req.ToJson());
        MyLogger.Log($"[SendMessage Result]:{res}");
    }

    public async Task JoinAsync(string userName)
    {
        var req = new ReqLoginPacket()
        {
            Nickname = userName,
        };

        var res = await _hub.Login(req.ToJson());
        MyLogger.Log($"[Login Result]:{res}");
    }
}
