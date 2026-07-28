using MagicOnionServer.Manager;
using Packets;
using Shared;
using Shared.Util;

namespace MagicOnionServer.Hubs
{
    public partial class ChatHub
    {
        public ValueTask<string> SendMessage(string pkt)
        {
            var res = new ResChatPacketResult();
            
            try
            {
                var req = pkt.ToObject<ReqChatPacket>();
                    
                if (UserManager.Instance.CheckLogin(Context.ContextId) == true)
                {
                    BroadCast(req.Nickname, req.Message);
                    res.Code = ErrorCode.Success;
                }
                else
                {
                    res.Code = ErrorCode.Fail;
                    res.Message = "You are not login";
                }
            }
            catch (Exception ex)
            {
                res.Code = ErrorCode.Fail;
                Logger.Log(ex.Message);
            }
            finally
            {
                Logger.Log(res.ToLogString());
            }

            return ValueTask.FromResult(res.ToJson());
        }
    }
}