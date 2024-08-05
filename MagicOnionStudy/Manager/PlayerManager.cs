using System.Collections.Concurrent;
using Shared.Util;

namespace MagicOnionServer
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public static int UserId;

        private readonly ConcurrentDictionary<Guid, Player> PlayerByConnectionId = new ConcurrentDictionary<Guid, Player>();
        private readonly ConcurrentDictionary<long, Player> PlayerByUserId = new ConcurrentDictionary<long, Player>();

        private PlayerManager()
        {
            UserId = 0;
        }

        public void IncreaseUserId()
        {
            Interlocked.Increment(ref UserId);
        }

        public int AddPlayer(Guid connectionId, string name)
        {
            IncreaseUserId();

            var player = new Player(0, connectionId, name);

            PlayerByConnectionId.TryAdd(connectionId, player);
            PlayerByUserId.TryAdd(UserId, player);

            Logger.Log($"AddPlayer :: connectionId:{connectionId}, userId: {UserId}, name:{name}");

            return UserId;
        }

    }
}
