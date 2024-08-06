using Shared;

namespace MagicOnionServer.Hubs
{
    public partial class ChatHub
    {
        public ValueTask<ResChatPacketResult> SendMessage(ReqChatPacket req)
        {
            var res = new ResChatPacketResult();

            try
            {
                Logger.Log($"{Extension.ToString(req)}");

                BroadCast(req.Nickname, req.Message);

                res.Code = ErrorCode.Success;
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

            return ValueTask.FromResult(res);
        }

    }
}
