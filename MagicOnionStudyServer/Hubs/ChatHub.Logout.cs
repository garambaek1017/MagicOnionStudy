using MagicOnionServer.Manager;
using Packets;
using Shared;
using Shared.Util;

namespace MagicOnionServer.Hubs
{
    public partial class ChatHub
    {
        public async ValueTask<string> Logout(string pkt)
        {
            var res = new ResLogoutPacketResult();
            
            var req = pkt.ToObject<ReqLogoutPacket>();
            try
            {
                // 그룹에서 보내면 지는 방에서 제거라서 자기는 브로드 캐스트를 받을수없지만
                // 남한테 쏠수있음 
                await this._room.RemoveAsync(this.Context);
                UserManager.Instance.RemoveUser(this.Context.ContextId);

                BroadCast("Server", $"{req.Nickname},{Context.ContextId} has logged out..");
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
            return res.ToJson();
        }
    }
}
