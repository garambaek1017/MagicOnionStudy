using Shared;

namespace MagicOnionServer.Hubs
{
    public partial class ChatHub
    {
        public async ValueTask<ResLogoutPacketResult> Logout(ReqLogoutPacket req)
        {
            var res = new ResLogoutPacketResult();

            try
            {
                Logger.Log(Extension.ToString(req));
                BroadCast("Server", $"{req.Nickname} has logged out..");
            }
            catch (Exception ex)
            {
                res.Code = ErrorCode.Fail;
                Logger.Log(ex.Message);
            }
            finally
            {
                Logger.Log(Extension.ToString(res));
            }
            return res;
        }
    }
}
