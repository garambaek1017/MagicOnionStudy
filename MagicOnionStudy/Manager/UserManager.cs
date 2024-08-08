using System.Collections.Concurrent;
using MagicOnionServer.User;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MagicOnionServer.Manager
{
    /// <summary>
    /// 유저 관리 매니저
    /// </summary>
    public class UserManager : Singleton<UserManager>
    {
        // 서버에서 발급하는 유저 id 
        private static int _userId = 0;
        // connectionId로 관리하는 dic
        private readonly ConcurrentDictionary<Guid, ChatUser> _userByConnectionId = new();
        // userId로 관리하는 dic 
        private readonly ConcurrentDictionary<long, ChatUser> _userByUserId = new();

        private UserManager()
        {

        }

        /// <summary>
        /// 유저 증가시 userId 증가처리 
        /// </summary>
        private void IncreaseUserId()
        {
            Interlocked.Increment(ref _userId);
        }

        /// <summary>
        ///  신규 유저 접속시 dictionary에 추가 
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public long AddUser(Guid connectionId, string name)
        {
            if (_userByConnectionId.TryGetValue(connectionId, out var value) != false)
            {
                return value.UserId;
            }

            var newUserid = _userId;
            var newPlayer = new ChatUser(newUserid, connectionId, name);

            _userByUserId.TryAdd(newUserid, newPlayer);
            _userByConnectionId.TryAdd(connectionId, newPlayer);

            Logger.Log($"AddPlayer :: connectionId:{connectionId}, userId: {newPlayer.UserId}, name:{name}");

            IncreaseUserId();

            return newPlayer.UserId;
        } 

        public long RemoveUser(Guid connectionId)
        {
            if (_userByConnectionId.TryGetValue(connectionId, out var value) == false)
            {
                return 0;
            }

            _userByUserId.TryRemove(value.UserId, out _);
            _userByConnectionId.TryRemove(connectionId, out _);

            Logger.Log($"RemovePlayer :: connectionId:{connectionId}, userId: {value.UserId}, name:{value.Name}");

            return value.UserId;
        }

        public bool CheckLogin(Guid connectionId)
        {
            return _userByConnectionId.ContainsKey(connectionId);
        }
    }
}
