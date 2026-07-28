using MagicOnionServer.Manager;
using Packets;
using Shared.Util;

namespace MagicOnionServer.Hubs
{
    public partial class ChatHub
    {  
        public async ValueTask<string> Login(string req)
        {
            var res = new ResLoginPacketResult();
            
            try
            {
                Logger.Log(req);
                
                var reqObj = req.ToObject<ReqLoginPacket>();
                // broadcast 하기 위해 그룹에 유저 추가 
                this._room = await this.Group.AddAsync(Constant.RoomName);

                var newUserId = UserManager.Instance.AddUser(ConnectionId, reqObj.Nickname);

                res = new ResLoginPacketResult
                {
                    UserId = newUserId,
                    Nickname = reqObj.Nickname,
                };
                
                BroadCast("Server", $"{reqObj.Nickname} has logged in..");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            } 
            finally
            {
                Logger.Log(res.ToLogString());
            }
            return res.ToJson();
        }
    }
}
