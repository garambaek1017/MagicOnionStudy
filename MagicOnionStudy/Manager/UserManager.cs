using System.Collections.Concurrent;

namespace MagicOnionServer
{
    public class UserManager : Singleton<UserManager>
    {
        public static int UserId;

        private readonly ConcurrentDictionary<Guid, ChatUser> PlayerByConnectionId = new();
        private readonly ConcurrentDictionary<long, ChatUser> PlayerByUserId = new();

        private UserManager()
        {
            UserId = 0;
        }

        private void IncreaseUserId()
        {
            Interlocked.Increment(ref UserId);
        }

        public long AddPlayer(Guid connectionId, string name)
        {
            if (PlayerByConnectionId.ContainsKey(connectionId) == false)
            {
                var newUserid = UserId;
                var newPlayer = new ChatUser(newUserid, connectionId, name);
                PlayerByUserId.TryAdd(newUserid, newPlayer);

                Logger.Log($"AddPlayer :: connectionId:{connectionId}, userId: {newPlayer.UserId}, name:{name}");

                IncreaseUserId();

                return newPlayer.UserId;
            }
            else
            {
                return PlayerByConnectionId[connectionId].UserId;
            }
        }
    }
}
