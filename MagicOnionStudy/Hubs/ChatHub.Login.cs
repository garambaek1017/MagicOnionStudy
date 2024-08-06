using Shared;

namespace MagicOnionServer.Hubs
{
    public partial class ChatHub
    {
        public async ValueTask<ResLoginPacketResult> Login(ReqLoginPacket req)
        {
            var res = new ResLoginPacketResult();

            try
            {
                Logger.Log(Extension.ToString(req));

                // broadcast 하기 위해 그룹에 유저 추가 
                this._room = await this.Group.AddAsync(Constant.RoomName);

                var newUserId = UserManager.Instance.AddPlayer(ConnectionId, req.Nickname);

                res = new ResLoginPacketResult
                {
                    UserId = newUserId,
                    Nickname = req.Nickname,
                };

                BroadCast("Server", $"{req.Nickname} has logged in..");
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
